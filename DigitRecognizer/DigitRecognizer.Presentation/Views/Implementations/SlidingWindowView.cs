using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using DigitRecognizer.Core.Data;
using DigitRecognizer.Core.Utilities;
using DigitRecognizer.Presentation.Infrastructure;
using DigitRecognizer.Presentation.Views.Interfaces;

namespace DigitRecognizer.Presentation.Views.Implementations
{
    public partial class SlidingWindowView : UserControl, ISlidingWindowView
    {
        #region Fields

        private Point _currentPoint;
        private Point _previousPoint;
        private readonly Pen _pen = new Pen(Color.Black, 30);
        private Bitmap _bitmap;
        private const int RetryCount = 5;

        #endregion

        #region Ctor

        public SlidingWindowView()
        {
            InitializeComponent();

            _bitmap = new Bitmap(panelDrawing.Width, panelDrawing.Height);

            Clear();

            PanelDoubleBuffering.Enable(panelDrawing);

            _pen.SetLineCap(LineCap.Round, LineCap.Round, DashCap.Round);

            panelDrawing.MouseDown += DrawingPanel_MouseDown;

            panelDrawing.MouseMove += DrawingPanel_MouseMove;

            panelDrawing.Paint += DrawingPanel_Paint;

            btnClassifyDrawing.Click += BtnClassifyDrawing_Click;

            btnClearDrawing.Click += BtnClearDrawing_Click;
        }

        #endregion

        #region Properties

        public Image Drawing => _bitmap;

        #endregion

        #region Event handlers

        public event EventHandler ClassifyDrawing;
        public event EventHandler ClearDrawing;

        #endregion

        #region Methods

        private void BtnClassifyDrawing_Click(object sender, EventArgs e)
        {
            ClassifyDrawing?.Invoke(this, EventArgs.Empty);
        }

        private void BtnClearDrawing_Click(object sender, EventArgs e)
        {
            ClearDrawing?.Invoke(this, EventArgs.Empty);
        }

        private void DrawingPanel_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(_bitmap, Point.Empty);
        }

        private void DrawingPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
            {
                return;
            }

            _currentPoint = e.Location;

            for (var i = 0; i < RetryCount; i++)
            {
                try
                {
                    using (Graphics g = Graphics.FromImage(_bitmap))
                    {
                        g.DrawLine(_pen, _previousPoint, _currentPoint);
                    }
                }
                catch
                {
                    continue;
                }

                break;
            }

            panelDrawing.Invalidate();

            _previousPoint = _currentPoint;
        }

        private void DrawingPanel_MouseDown(object sender, MouseEventArgs e)
        {
            _previousPoint = e.Location;
        }

        protected override void OnResize(EventArgs e)
        {
            panelDrawing.Height = Height - Padding.All * 2 - btnClassifyDrawing.Height - 10;

            if (_bitmap != null)
            {
                _bitmap = new Bitmap(ImageUtilities.Resize(_bitmap, panelDrawing.Width, panelDrawing.Height));

                panelDrawing.Invalidate();
            }

            btnClassifyDrawing.Left = panelDrawing.Left;
            btnClearDrawing.Left = btnClassifyDrawing.Right + 5;

            base.OnResize(e);
        }

        #endregion

        #region ISlidingWindowView implementation

        public void DrawBoundingBox(BoundingBox boundingBox, int prediction, double predictionAccuracy)
        {
            var pen = new Pen(Color.Red, 3.0f);
            Brush brush = Brushes.Red;

            using (Graphics g = Graphics.FromImage(_bitmap))
            {
                g.DrawRectangle(pen, boundingBox.X, boundingBox.Y, boundingBox.Image.Width, boundingBox.Image.Height);

                Rectangle rectForPredition  = new Rectangle(boundingBox.X + boundingBox.Image.Width - 100, boundingBox.Y + boundingBox.Image.Height - 25, 100, 25);
                g.FillRectangle(brush, rectForPredition);

                var drawFont = new Font("Arial", 12, FontStyle.Bold);
                var drawBrush = new SolidBrush(Color.White);
                var drawFormat = new StringFormat();

                string text = $"{prediction} ({predictionAccuracy:P})";
                SizeF labelSize = g.MeasureString(text, drawFont);

                g.DrawString(text, 
                    drawFont, 
                    drawBrush, 
                    rectForPredition.Left + (rectForPredition.Width - labelSize.Width) / 2, 
                    rectForPredition.Top + (rectForPredition.Height - labelSize.Height) / 2, 
                    drawFormat);

                panelDrawing.Invalidate();
            }
        }

        public void Clear()
        {
            ClearDrawingBitmap();

            panelDrawing.Invalidate();
        }

        private void ClearDrawingBitmap()
        {
            for (var i = 0; i < RetryCount; i++)
            {
                try
                {
                    using (Graphics g = Graphics.FromImage(_bitmap))
                    {
                        g.Clear(Color.White);
                    }
                }
                catch
                {
                    continue;
                }

                break;
            }
        }

        public void ShowView()
        {
            BringToFront();

            Show();
        }

        public void HideView()
        {
            Hide();
        }

        #endregion
    }
}
