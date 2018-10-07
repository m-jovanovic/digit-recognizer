using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using DigitRecognizer.Core.Data;
using DigitRecognizer.Core.Extensions;
using DigitRecognizer.Core.Utilities;
using DigitRecognizer.MachineLearning.Infrastructure.Models;
using DigitRecognizer.MachineLearning.Providers;
using DigitRecognizer.Presentation.Models;
using DigitRecognizer.Presentation.Services;
using DigitRecognizer.Presentation.Views.Interfaces;

namespace DigitRecognizer.Presentation.Presenters
{
    public class BenchmarkPresenter
    {
        #region Fields

        private readonly IBenchmarkView _benchmarkView;
        private readonly IMessageService _messageService;
        private readonly ILoggingService _loggingService;
        private CancellationTokenSource _cancellationTokenSource;

        #endregion

        #region Ctor

        public BenchmarkPresenter(IBenchmarkView benchmarkView, IMessageService messageService, ILoggingService loggingService)
        {
            _messageService = messageService;
            _loggingService = loggingService;
            _benchmarkView = benchmarkView;

            _benchmarkView.RunBenchmark += OnRunBenchmark;

            _benchmarkView.CancelBenchmark += OnCancelBenchmark;

            _benchmarkView.IsBenchmarkRunning = false;
        }

        #endregion

        #region Methods

        private async void OnRunBenchmark(object sender, EventArgs e)
        {
            IPredictionModel predictionModel = Global.PredictionModel;

            if (predictionModel == null)
            {
                _messageService.ShowMessage("The prediction model must be loaded first.", "Prediction model", icon: MessageBoxIcon.Information);

                return;
            }

            _benchmarkView.ResetView();

            _cancellationTokenSource = new CancellationTokenSource();

            CancellationToken token = _cancellationTokenSource.Token;

            token.Register(() => { _benchmarkView.IsBenchmarkRunning = false; });

            try
            {
                await Task.Run(() => {
                    RunBenchmark(predictionModel);
                }, token);
            }
            catch (Exception exception)
            {
                _loggingService.Log(exception);

                _messageService.ShowMessage("An error ocurred while running the benchmark. Please try again.", "Benchmark error", icon: MessageBoxIcon.Information);
            }
        }

        private void RunBenchmark(IPredictionModel model)
        {
            _loggingService.Log("Running benchmark has started");

            _benchmarkView.IsBenchmarkRunning = true;

            var provider = new BatchDataProvider(DirectoryHelper.TestLabelsPath, DirectoryHelper.TestImagesPath, 100);

            var acc = 0;

            for (var i = 0; i < 100; i++)
            {
                if (!_benchmarkView.IsBenchmarkRunning)
                {
                    break;
                }

                MnistImageBatch data = provider.GetData();

                int[] predictions = data.Pixels.Select(model.Predict).Select(x=> x.ArgMax()).ToArray();

                acc += data.Labels.Where((lbl, pred) => lbl == predictions[pred]).Count();

                _benchmarkView.PerformProgressStep();

                _benchmarkView.DrawGrid(new ImageGridModel(data, predictions));
            }

            _benchmarkView.SetAccuracy(acc);

            _benchmarkView.IsBenchmarkRunning = false;

            _loggingService.Log("Running benchmark has completed");
        }

        private void OnCancelBenchmark(object sender, EventArgs e)
        {
            _loggingService.Log("Running benchmark was canceled");

            _cancellationTokenSource.Cancel();
        }
        
        #endregion
    }
}
