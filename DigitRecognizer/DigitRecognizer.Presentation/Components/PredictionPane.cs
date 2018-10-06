using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace DigitRecognizer.Presentation.Components
{
    public partial class PredictionPane : UserControl
    {
        #region Fields

        private const string LabelName = "lbl";
        private const string LabelPredictionName = "lblPrediction";
        private const string ProgressBarName = "progressBar";
        private const int ClassNumber = 10;

        #endregion

        #region Ctor

        public PredictionPane()
        {
            InitializeComponent();

            InitializeDisplay();
        }

        #endregion

        #region Methods

        public void ProcessPrediction(double[] predictions)
        {
            if (predictions.Length != ClassNumber)
            {
                throw new ArgumentException(nameof(predictions));
            }

            for (var i = 0; i < predictions.Length; i++)
            {
                ProgressBarHandler(GetProgressBarName(i), (int)(Math.Round(predictions[i] * 100)));

                PredictionLabelHandler(GetPredictionLabelName(i), predictions[i].ToString("P"));
            }
        }

        public void Clear()
        {
            for (var i = 0; i < ClassNumber; i++)
            {
                ProgressBarHandler(GetProgressBarName(i), 0);

                PredictionLabelHandler(GetPredictionLabelName(i), string.Empty);
            }
        }

        private void ProgressBarHandler(string progressBarKey, int value)
        {
            if (Controls.ContainsKey(progressBarKey))
            {
                ((ProgressBar)Controls[progressBarKey]).Value = value;
            }
            else
            {
                throw new ArgumentException(nameof(progressBarKey));
            }
        }

        private void PredictionLabelHandler(string labelPredictionKey, string text)
        {
            if (Controls.ContainsKey(labelPredictionKey))
            {
                ((Label)Controls[labelPredictionKey]).Text = text;
            }
            else
            {
                throw new ArgumentException(nameof(labelPredictionKey));
            }
        }

        #endregion

        #region Utilities

        private void InitializeDisplay()
        {
            var controls = new List<Control>();

            for (var i = 0; i < 10; i++)
            {
                int top = Top + 40 * i + 1;

                Label labelNum = GenerateLabel(GetLabelName(i), Left, top + 1, i.ToString(), 20);

                ProgressBar progressBar = GenerateProgressBar(GetProgressBarName(i), labelNum.Right + 5, top);

                Label labelPrediction = GenerateLabel(GetPredictionLabelName(i), progressBar.Right + 5, top + 1, "Prediction");

                controls.Add(labelNum);
                controls.Add(progressBar);
                controls.Add(labelPrediction);
            }

            Controls.AddRange(controls.ToArray());
        }

        private static string GetPredictionLabelName(int i) => $"{LabelPredictionName}{i}";

        private static string GetProgressBarName(int i) => $"{ProgressBarName}{i}";

        private static string GetLabelName(int i) => $"{LabelName}{i}";

        private static ProgressBar GenerateProgressBar(string name, int left, int top)
        {
            return new ProgressBar
            {
                Name = name,
                Value = 0,
                Left = left,
                Top = top,
                Width = 450
            };
        }

        private static Label GenerateLabel(string name, int left, int right, string text = "", int? width = null)
        {
            var lbl = new Label
            {
                AutoSize = true,
                Name = name,
                Text = text,
                Left = left,
                Top = right,
                Font = new Font(FontFamily.GenericSansSerif, 12f)
            };

            if (width != null)
            {
                lbl.Width = (int)width;
            }

            return lbl;
        }

        #endregion
    }
}
