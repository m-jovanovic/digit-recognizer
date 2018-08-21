using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DigitRecognizer.Core.Data;
using DigitRecognizer.Core.Extensions;
using DigitRecognizer.Core.Utilities;
using DigitRecognizer.MachineLearning.Infrastructure.Models;
using DigitRecognizer.MachineLearning.Providers;
using DigitRecognizer.Presentation.Views.Interfaces;

namespace DigitRecognizer.Presentation.Presenters
{
    public class MainFormPresenter
    {
        private readonly IBenchmarkView _benchmarkView;

        public MainFormPresenter(IMainFormView mainFormView)
        {
            _benchmarkView = mainFormView.BenchmarkView;

            _benchmarkView.RunBenchmark += OnRunBenchmark;
        }

        private void OnRunBenchmark(object sender, EventArgs e)
        {
            var dlg = new OpenFileDialog
            {
                Multiselect = true
            };

            dlg.ShowDialog();

            ClusterPredictionModel model = ClusterPredictionModel.FromFiles(dlg.FileNames);

            Task.Run(() =>
            {
                var provider = new BatchDataProvider(DirectoryHelper.TestLabelsPath, DirectoryHelper.TestImagesPath, 100);

                var acc = 0;

                for (var i = 0; i < 100; i++)
                {
                    MnistImageBatch data = provider.GetData();

                    List<double[]> predictions = data.Pixels.Select(model.Predict).ToList();

                    acc += data.Labels.Where((t, j) => t == predictions[j].ArgMax()).Count();

                    _benchmarkView.PerformProgressStep();
                }

                _benchmarkView.SetAccuracy(acc);
            });
        }
    }
}
