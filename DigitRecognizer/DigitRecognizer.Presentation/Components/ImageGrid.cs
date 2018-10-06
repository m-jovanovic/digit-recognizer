using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DigitRecognizer.Presentation.Infrastructure;
using DigitRecognizer.Presentation.Models;

namespace DigitRecognizer.Presentation.Components
{
    public partial class ImageGrid : UserControl
    {
        #region Fields

        private ImageGridModel _currentlyInProcessing;
        private ImageGridModel _incorrectlyClassified;

        private int _gridCurrentImagesPage = 0;
        private int _gridIncorrectImagesPage = 0;

        #endregion

        #region Ctor

        public ImageGrid()
        {
            InitializeComponent();

            InitializePanels();

            InitializeButtons();

            ResetGrid();
        }

        #endregion

        #region Methods

        private void InitializePanels()
        {
            PanelDoubleBuffering.Enable(panelCurrentImagesGrid);
            PanelDoubleBuffering.Enable(panelIncorrectImagesGrid);

            panelCurrentImagesGrid.Paint += OnPanelCurrentImagesGridPaint;
            panelIncorrectImagesGrid.Paint += OnPanelIncorrectImagesGridPaint;
        }

        private void InitializeButtons()
        {
            btnCurrentGridPrev.Click += BtnCurrentGridOnClick;
            btnCurrentGridNext.Click += BtnCurrentGridOnClick;
            btnIncorrectGridPrev.Click += BtnIncorrectGridOnClick;
            btnIncorrectGridNext.Click += BtnIncorrectGridOnClick;
        }

        public void ResetGrid()
        {
            _currentlyInProcessing = new ImageGridModel();
            _incorrectlyClassified = new ImageGridModel();

            panelCurrentImagesGrid.Invalidate();
            panelIncorrectImagesGrid.Invalidate();

            btnCurrentGridPrev.Enabled = false;
            btnCurrentGridNext.Enabled = false;
            btnIncorrectGridPrev.Enabled = false;
            btnIncorrectGridNext.Enabled = false;
        }

        public void DrawGrid(ImageGridModel model)
        {
            _currentlyInProcessing += model;

            int initialCount = _incorrectlyClassified.Count;

            for (var i = 0; i < model.Labels.Count; i++)
            {
                if (model.Labels[i] != model.Predictions[i])
                {
                    _incorrectlyClassified.Predictions.Add(model.Predictions[i]);
                    _incorrectlyClassified.Labels.Add(model.Labels[i]);
                    _incorrectlyClassified.Images.Add(model.Images[i]);
                }
            }

            CalculatePaginationInfo();

            panelCurrentImagesGrid.Invalidate();

            if (initialCount != _incorrectlyClassified.Count)
            {
                panelIncorrectImagesGrid.Invalidate();
            }
        }

        private void CalculatePaginationInfo()
        {
            _gridCurrentImagesPage = (_currentlyInProcessing.Count - 1) / Global.ImageGridFieldCount;

            CalculatePaginationButtonsState(btnCurrentGridPrev, btnCurrentGridNext, _gridCurrentImagesPage, _gridCurrentImagesPage);

            _gridIncorrectImagesPage = (_incorrectlyClassified.Count - 1) / Global.ImageGridFieldCount; 

            CalculatePaginationButtonsState(btnIncorrectGridPrev, btnIncorrectGridNext, _gridIncorrectImagesPage, _gridIncorrectImagesPage);
        }

        private static void CalculatePaginationButtonsState(Control prevBtn, Control nextBtn, int page, int maxPage)
        {
            if (prevBtn.InvokeRequired)
            {
                prevBtn.Invoke((Action) (() => { prevBtn.Enabled = page > 0; }));
            }
            else
            {
                prevBtn.Enabled = page > 0;
            }

            if (nextBtn.InvokeRequired)
            {
                nextBtn.Invoke((Action) (() => { nextBtn.Enabled = page < maxPage; }));
            }
            else
            {
                nextBtn.Enabled = page < maxPage;
            }
        }

        private void OnPanelCurrentImagesGridPaint(object sender, PaintEventArgs e)
        {
            OnPanelPaint(_currentlyInProcessing, _gridCurrentImagesPage, Global.ImageGridFieldCount, e.Graphics);
        }

        private void OnPanelIncorrectImagesGridPaint(object sender, PaintEventArgs e)
        {
            OnPanelPaint(_incorrectlyClassified, _gridIncorrectImagesPage, Global.ImageGridFieldCount, e.Graphics);
        }

        private static void OnPanelPaint(ImageGridModel model, int page, int take, Graphics g)
        {
            if (model == null || model.Count == 0) return;

            int skip = page * take;
            
            DrawGrid(g,
                model.Images.Skip(skip).Take(take).ToArray(),
                model.Labels.Skip(skip).Take(take).ToArray(),
                model.Predictions.Skip(skip).Take(take).ToArray());
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

        private void BtnCurrentGridOnClick(object sender, EventArgs e)
        {
            OnPageButtonClick(btnCurrentGridPrev, btnCurrentGridNext, ref _gridCurrentImagesPage, (Button)sender == btnCurrentGridNext, (_currentlyInProcessing.Count - 1) / Global.ImageGridFieldCount);

            panelCurrentImagesGrid.Invalidate();
        }

        private void BtnIncorrectGridOnClick(object sender, EventArgs e)
        {
            OnPageButtonClick(btnIncorrectGridPrev, btnIncorrectGridNext, ref _gridIncorrectImagesPage, (Button)sender == btnIncorrectGridNext, (_incorrectlyClassified.Count - 1) / Global.ImageGridFieldCount);

            panelIncorrectImagesGrid.Invalidate();
        }

        private static void OnPageButtonClick(Control prevBtn, Control nextBtn, ref int page, bool shouldIncrease, int maxPage)
        {
            page += shouldIncrease ? 1 : -1;

            CalculatePaginationButtonsState(prevBtn, nextBtn, page, maxPage);
        }

        protected override void OnResize(EventArgs e)
        {
            // Left panel
            CalculateGridElementLocations(panelCurrentImagesGrid, lblLeft, btnCurrentGridPrev, btnCurrentGridNext, Width * 0.25);

            // Right panel
            CalculateGridElementLocations(panelIncorrectImagesGrid, lblRight, btnIncorrectGridPrev, btnIncorrectGridNext,  Width * 0.75);

            lblNote.Left = panelIncorrectImagesGrid.Right - lblNote.Width + 5;

            lblNote.Top = panelIncorrectImagesGrid.Bottom + 5;

            base.OnResize(e);
        }

        private void CalculateGridElementLocations(Control panel, Control label, Control btnPrev, Control btnNext, double center)
        {
            panel.Left = (int)(center - panel.Width / 2.0);

            panel.Top = (Height - panel.Height) / 2;
            
            label.Left = panel.Left - 5;

            label.Top = panel.Top - 25;

            btnPrev.Left = panel.Left;

            btnPrev.Top = panel.Bottom + 5;

            btnNext.Left = panel.Left + btnPrev.Width + 5;

            btnNext.Top = panel.Bottom + 5;
        }

        #endregion
    }
}
