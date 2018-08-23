using System;
using System.Windows.Forms;
using DigitRecognizer.Presentation.Views.Interfaces;

namespace DigitRecognizer.Presentation.Views.Implementations
{
    public partial class BenchmarkView : UserControl, IBenchmarkView
    {
        #region Init

        public BenchmarkView()
        {
            InitializeComponent();

            InitializeView();
        }

        private void InitializeView()
        {
            btnRunBenchmark.Click += OnBtnRunBenchmarkClick;

            btnCancelBenchmark.Click += OnBtnCancelBenchmarkClick;

            ResetView();
        }

        #endregion

        #region Event handlers

        public event EventHandler RunBenchmark;

        public event EventHandler CancelBenchmark;

        #endregion

        #region Properties

        public bool IsBenchmarkRunning
        {
            get => btnCancelBenchmark.Enabled;

            set
            {
                if (btnCancelBenchmark.Enabled != value)
                {
                    if (btnCancelBenchmark.InvokeRequired)
                    {
                        btnCancelBenchmark.Invoke((Action) (() => { btnCancelBenchmark.Enabled = value; }));
                    }
                    else
                    {
                        btnCancelBenchmark.Enabled = value;
                    }
                }
            }
        }

        #endregion

        #region Methods

        public void PerformProgressStep()
        {
            if (!IsBenchmarkRunning)
            {
                return;
            }

            if (pBarProgress.InvokeRequired)
            {
                pBarProgress.Invoke((Action)(() =>
                {
                    pBarProgress.PerformStep();
                }));
            }
            else
            {
                pBarProgress.PerformStep();
            }
        }

        public void SetAccuracy(int accuracy)
        {
            if (!IsBenchmarkRunning)
            {
                return;
            }

            if (pBarAccuracy.InvokeRequired)
            {
                pBarAccuracy.Invoke((Action)(() =>
                {
                    pBarAccuracy.Value = accuracy;
                }));
            }
            else
            {
                pBarAccuracy.Value = accuracy;
            }

            string message = $@"Accuracy: {accuracy / 10000.0:P2} ({accuracy}/10000)";

            if (lblAccuracy.InvokeRequired)
            {
                lblAccuracy.Invoke((Action)(() =>
                {
                    lblAccuracy.Text = message;
                }));
            }
            else
            {
                lblAccuracy.Text = message;
            }
        }

        private void OnBtnRunBenchmarkClick(object sender, EventArgs e)
        {
            ResetView();
            
            RunBenchmark?.Invoke(this, EventArgs.Empty);
        }

        private void OnBtnCancelBenchmarkClick(object sender, EventArgs e)
        {
            ResetView();

            CancelBenchmark?.Invoke(this, EventArgs.Empty);
        }

        private void ResetView()
        {
            pBarAccuracy.Value = 0;

            pBarProgress.Value = 0;

            lblAccuracy.Text = @"Accuracy";
        }

        #endregion
    }
}
