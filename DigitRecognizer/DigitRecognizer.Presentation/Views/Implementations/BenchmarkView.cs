using System;
using System.Windows.Forms;
using DigitRecognizer.Presentation.Views.Interfaces;

namespace DigitRecognizer.Presentation.Views.Implementations
{
    public partial class BenchmarkView : UserControl, IBenchmarkView
    {
        public BenchmarkView()
        {
            InitializeComponent();
        }

        public event EventHandler RunBenchmark;
        
        public void PerformProgressStep()
        {
            pBarProgress.Invoke(new Action(() =>
            {
                pBarProgress.PerformStep();
            }));
        }

        public void SetAccuracy(int accuracy)
        {
            pBarAccuracy.Invoke(new Action(() =>
            {
                pBarAccuracy.Value = accuracy;
            }));

            lblAccuracy.Invoke(new Action(() =>
            {
                lblAccuracy.Text = $@"Accuracy: {accuracy / 10000.0:P2} ({accuracy}/10000)";
            }));
        }
        
        private void BtnBenchmark_Click(object sender, EventArgs e)
        {
            pBarAccuracy.Value = 0;

            pBarProgress.Value = 0;

            lblAccuracy.Text = @"Accuracy";

            RunBenchmark?.Invoke(this, EventArgs.Empty);
        }
    }
}
