using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using DigitRecognizer.Presentation.Data;

namespace DigitRecognizer.Presentation.Components
{
    public partial class ImageGrid : UserControl
    {
        #region Fields

        private ImageGridModel _gridModel;

        private readonly List<int> _incorrectlyClassifiedImagesPredictions;
        private readonly List<int> _incorrectlyClassifiedImagesLabels;
        private readonly List<Image> _incorrectlyClassifiedImages;

        #endregion

        #region Ctor

        public ImageGrid()
        {
            InitializeComponent();

            _incorrectlyClassifiedImagesPredictions = new List<int>();
            _incorrectlyClassifiedImagesLabels = new List<int>();
            _incorrectlyClassifiedImages = new List<Image>();

            panelImagesGrid.Paint += OnPanelImagesGridPaint;
            panelIncorrectImagesGrid.Paint += OnPanelIncorrectImagesGridPaint;
        }

        #endregion

        #region Methods

        protected override void OnResize(EventArgs e)
        {
            double half = Width / 2.0;
            
            // Left panel

            double center = half / 2.0;

            panelImagesGrid.Left = (int) (center - panelImagesGrid.Width / 2.0);
            panelImagesGrid.Top = (Height - panelImagesGrid.Height) / 2;
            lblLeft.Left = panelImagesGrid.Left - 5;
            lblLeft.Top = panelImagesGrid.Top - 25;

            // Right panel

            center = Width - half / 2.0;

            panelIncorrectImagesGrid.Left = (int)(center - panelIncorrectImagesGrid.Width / 2.0);
            panelIncorrectImagesGrid.Top = (Height - panelIncorrectImagesGrid.Height) / 2;
            lblRight.Left = panelIncorrectImagesGrid.Left - 5;
            lblRight.Top = panelIncorrectImagesGrid.Top - 25;

            lblNote.Left = panelIncorrectImagesGrid.Right - lblNote.Width + 5;
            lblNote.Top = panelIncorrectImagesGrid.Bottom + lblNote.Height - 10;

            base.OnResize(e);
        }

        private void OnPanelImagesGridPaint(object sender, PaintEventArgs e)
        {
            if (_gridModel == null) return;

            int xOffset = _gridModel.Images[0].Width;
            int yOffset = _gridModel.Images[0].Height;

            for (var i = 0; i < _gridModel.Count; i++)
            {
                int x = i % 10 * xOffset;
                int y = i / 10 * yOffset;

                Image img = _gridModel.Images[i];

                e.Graphics.DrawImage(img, new Point(x, y));

                Size imgSize = img.Size;

                var drawFont = new Font("Arial", 10);
                var drawBrush = new SolidBrush(Color.Black);
                var drawFormat = new StringFormat();

                string label = _gridModel.Labels[i].ToString();
                SizeF labelSize = e.Graphics.MeasureString(label, drawFont);

                e.Graphics.DrawString(label, drawFont, drawBrush, x, y + imgSize.Height - labelSize.Height, drawFormat);

                string prediction = _gridModel.Predictions[i].ToString();
                SizeF predictionSize = e.Graphics.MeasureString(prediction, drawFont);

                e.Graphics.DrawString(prediction, drawFont, drawBrush, x + imgSize.Width - predictionSize.Width, y + imgSize.Height - predictionSize.Height, drawFormat);
            }
        }

        private void OnPanelIncorrectImagesGridPaint(object sender, PaintEventArgs e)
        {
            if (_gridModel == null) return;

            int xOffset = _gridModel.Images[0].Width;
            int yOffset = _gridModel.Images[0].Height;

            for (var i = 0; i < _incorrectlyClassifiedImages.Count; i++)
            {
                int x = i % 10 * xOffset;
                int y = i / 10 * yOffset;

                Image img = _incorrectlyClassifiedImages[i];

                e.Graphics.DrawImage(img, new Point(x, y));

                Size imgSize = img.Size;

                var drawFont = new Font("Arial", 10);
                var drawBrush = new SolidBrush(Color.Black);
                var drawFormat = new StringFormat();

                string label = _incorrectlyClassifiedImagesLabels[i].ToString();
                SizeF labelSize = e.Graphics.MeasureString(label, drawFont);

                e.Graphics.DrawString(label, drawFont, drawBrush, x, y + imgSize.Height - labelSize.Height, drawFormat);

                string prediction = _incorrectlyClassifiedImagesPredictions[i].ToString();
                SizeF predictionSize = e.Graphics.MeasureString(prediction, drawFont);

                e.Graphics.DrawString(prediction, drawFont, drawBrush, x + imgSize.Width - predictionSize.Width, y + imgSize.Height - predictionSize.Height, drawFormat);
            }
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

        #endregion
    }
}
