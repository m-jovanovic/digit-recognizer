using System;
using System.Windows.Forms;
using DigitRecognizer.Presentation.Components;
using DigitRecognizer.Presentation.Data;
using DigitRecognizer.Presentation.Views.Interfaces;

namespace DigitRecognizer.Presentation.Views.Implementations
{
    public partial class BenchmarkView : UserControl, IBenchmarkView
    {
        private readonly ImageGrid _imageGrid;

        #region Ctor

        public BenchmarkView()
        {
            InitializeComponent();

            Padding = new Padding(5);

            _imageGrid = new ImageGrid() { Dock = DockStyle.Fill, Padding = new Padding(20)};

            panelGridContainer.Controls.Add(_imageGrid);

            InitializeView();
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

            string text = $@"{pBarProgress.Value}%";
            
            if (lblProgressVal.InvokeRequired)
            {
                lblProgressVal.Invoke((Action) (() =>
                {
                    lblProgressVal.Text = text;
                }));
            }
            else
            {
                lblProgressVal.Text = text;
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

            string text1 = $@"Accuracy: {accuracy}/10000 images correcty classified";

            if (lblAccuracy.InvokeRequired)
            {
                lblAccuracy.Invoke((Action)(() =>
                {
                    lblAccuracy.Text = text1;
                }));
            }
            else
            {
                lblAccuracy.Text = text1;
            }

            string text2 = $"{accuracy / 10000.0 * 100:N2}%";

            if (lblAccuracyValue.InvokeRequired)
            {
                lblAccuracyValue.Invoke((Action)(() =>
                {
                    lblAccuracyValue.Text = text2;
                }));
            }
            else
            {
                lblAccuracyValue.Text = text2;
            }
        }
        
        public void DrawGrid(ImageGridModel model)
        {
            _imageGrid.DrawGrid(model);
        }

        public void Display()
        {
            Show();
        }

        public void Close()
        {
            Hide();
        }

        private void InitializeView()
        {
            btnRunBenchmark.Click += OnBtnRunBenchmarkClick;

            btnCancelBenchmark.Click += OnBtnCancelBenchmarkClick;
            
            ResetView();
        }

        private void OnBtnRunBenchmarkClick(object sender, EventArgs e)
        {
            ResetView();

            _imageGrid.ResetGrid();

            RunBenchmark?.Invoke(this, EventArgs.Empty);
        }

        private void OnBtnCancelBenchmarkClick(object sender, EventArgs e)
        {
            CancelBenchmark?.Invoke(this, EventArgs.Empty);
        }

        private void ResetView()
        {
            pBarAccuracy.Value = 0;

            pBarProgress.Value = 0;

            lblAccuracy.Text = @"Accuracy";

            lblProgressVal.Text = lblAccuracyValue.Text = string.Empty;
        }

        #endregion
    }
}
