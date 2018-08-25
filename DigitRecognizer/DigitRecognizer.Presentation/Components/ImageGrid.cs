using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using DigitRecognizer.Presentation.Data;
using DigitRecognizer.Presentation.Infrastructure;

namespace DigitRecognizer.Presentation.Components
{
    public partial class ImageGrid : UserControl
    {
        #region Fields

        private ImageGridModel _gridModel;

        private List<int> _incorrectlyClassifiedImagesPredictions;
        private List<int> _incorrectlyClassifiedImagesLabels;
        private List<Image> _incorrectlyClassifiedImages;

        #endregion

        #region Ctor

        public ImageGrid()
        {
            InitializeComponent();

            InitializePanels();

            _incorrectlyClassifiedImagesPredictions = new List<int>();
            _incorrectlyClassifiedImagesLabels = new List<int>();
            _incorrectlyClassifiedImages = new List<Image>();
        }

        #endregion

        #region Methods

        private void InitializePanels()
        {
            PanelDoubleBuffering.Enable(panelImagesGrid);
            PanelDoubleBuffering.Enable(panelIncorrectImagesGrid);

            panelImagesGrid.Paint += OnPanelImagesGridPaint;
            panelIncorrectImagesGrid.Paint += OnPanelIncorrectImagesGridPaint;
        }

        public void ResetGrid()
        {
            _gridModel = null;
            _incorrectlyClassifiedImages = new List<Image>();
            _incorrectlyClassifiedImagesLabels = new List<int>();
            _incorrectlyClassifiedImagesPredictions = new List<int>();

            panelImagesGrid.Invalidate();
            panelIncorrectImagesGrid.Invalidate();
        }

        public void DrawGrid(ImageGridModel model)
        {
            _gridModel = model;

            panelImagesGrid.Invalidate();

            int count = _incorrectlyClassifiedImages.Count;

            for (var i = 0; i < _gridModel.Labels.Length; i++)
            {
                if (_gridModel.Labels[i] != _gridModel.Predictions[i])
                {
                    _incorrectlyClassifiedImagesPredictions.Add(_gridModel.Predictions[i]);
                    _incorrectlyClassifiedImagesLabels.Add(_gridModel.Labels[i]);
                    _incorrectlyClassifiedImages.Add(_gridModel.Images[i]);
                }
            }

            if (count != _incorrectlyClassifiedImages.Count)
            {
                panelIncorrectImagesGrid.Invalidate();
            }
        }

        private void OnPanelImagesGridPaint(object sender, PaintEventArgs e)
        {
            if (_gridModel == null) return;

            DrawGrid(e.Graphics, _gridModel.Images, _gridModel.Labels, _gridModel.Predictions);
        }

        private void OnPanelIncorrectImagesGridPaint(object sender, PaintEventArgs e)
        {
            if (_incorrectlyClassifiedImages.Count == 0) return;

            DrawGrid(e.Graphics, _incorrectlyClassifiedImages.ToArray(), _incorrectlyClassifiedImagesLabels.ToArray(), _incorrectlyClassifiedImagesPredictions.ToArray());
        }

        private static void DrawGrid(Graphics g, Image[] images, int[] labels, int[] predictions)
        {
            int xOffset = images[0].Width;
            int yOffset = images[0].Height;

            for (var i = 0; i < images.Length; i++)
            {
                int x = i % 10 * xOffset;
                int y = i / 10 * yOffset;

                Image img = images[i];

                g.DrawImage(img, new Point(x, y));

                Size imgSize = img.Size;

                var drawFont = new Font("Arial", 10);
                var drawBrush = new SolidBrush(Color.Black);
                var drawFormat = new StringFormat();

                string label = labels[i].ToString();
                SizeF labelSize = g.MeasureString(label, drawFont);

                g.DrawString(label, drawFont, drawBrush, x, y + imgSize.Height - labelSize.Height, drawFormat);

                string prediction = predictions[i].ToString();
                SizeF predictionSize = g.MeasureString(prediction, drawFont);

                g.DrawString(prediction, drawFont, drawBrush, x + imgSize.Width - predictionSize.Width, y + imgSize.Height - predictionSize.Height, drawFormat);
            }
        }

        protected override void OnResize(EventArgs e)
        {
            // Left panel
            CalculateGridLocation(panelImagesGrid, lblLeft, Width / 4.0);

            // Right panel
            CalculateGridLocation(panelIncorrectImagesGrid, lblRight, Width - Width / 4.0);

            lblNote.Left = panelIncorrectImagesGrid.Right - lblNote.Width + 5;
            lblNote.Top = panelIncorrectImagesGrid.Bottom + lblNote.Height - 10;

            base.OnResize(e);
        }

        private void CalculateGridLocation(object panelObj, object labelObj, double center)
        {
            var panel = (Panel)panelObj;
            panel.Left = (int)(center - panel.Width / 2.0);
            panel.Top = (Height - panel.Height) / 2;

            var label = (Label)labelObj;
            label.Left = panel.Left - 5;
            label.Top = panel.Top - 25;
        }

        #endregion
    }
}
