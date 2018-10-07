using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using DigitRecognizer.Presentation.Infrastructure;
using DigitRecognizer.Presentation.Views.Interfaces;

namespace DigitRecognizer.Presentation.Views.Implementations
{
    public partial class DrawingView : UserControl, IDrawingView
    {
        #region Fields

        private Point _currentPoint;
        private Point _previousPoint;
        private readonly Pen _pen = new Pen(Color.Black, 70);
        private readonly Bitmap _bitmap;
        private const int RetryCount = 5;

        #endregion

        #region Ctor
        
        public DrawingView()
        {
            InitializeComponent();

            _bitmap = new Bitmap(drawingPanel.Width, drawingPanel.Height);

            Clear();

            PanelDoubleBuffering.Enable(drawingPanel);

            _pen.SetLineCap(LineCap.Round, LineCap.Round, DashCap.Round);

            drawingPanel.MouseDown += DrawingPanel_MouseDown;

            drawingPanel.MouseMove += DrawingPanel_MouseMove;

            drawingPanel.Paint += DrawingPanel_Paint;

            btnClassifyDrawing.Click += BtnClassifyDrawing_Click;

            btnClearDrawing.Click += BtnClearDrawing_Click;
        }

        #endregion

        #region Event handlers
        
        public event EventHandler ClassifyDrawing;

        public event EventHandler ClearDrawing;

        #endregion

        #region Properties

        public Image Drawing => _bitmap;

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

            drawingPanel.Invalidate();

            _previousPoint = _currentPoint;
        }

        private void DrawingPanel_MouseDown(object sender, MouseEventArgs e)
        {
            _previousPoint = e.Location;
        }
        
        protected override void OnResize(EventArgs e)
        {
            drawingPanel.Top = (Height - drawingPanel.Height) / 2; 
            drawingPanel.Left = (Width - (drawingPanel.Width + predictionPane.Width - 35)) / 2;

            lblInstructions.Left = drawingPanel.Left - 5;
            lblInstructions.Top = drawingPanel.Top - lblInstructions.Height - 5;

            int top = drawingPanel.Bottom + 5;

            btnClassifyDrawing.Left = drawingPanel.Left;
            btnClassifyDrawing.Top = top;

            btnClearDrawing.Left = btnClassifyDrawing.Right + 5;
            btnClearDrawing.Top = top;

            predictionPane.Left = drawingPanel.Right + 40;
            predictionPane.Top = drawingPanel.Top;
            
            base.OnResize(e);
        }

        #endregion

        #region IDrawingView implementation

        public void ProcessPrediction(double[] prediction)
        {
            predictionPane.ProcessPrediction(prediction);
        }

        public void Clear()
        {
            predictionPane.Clear();

            ClearDrawingBitmap();

            drawingPanel.Invalidate();
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

                        DrawMeshOnBitmap(g);
                    }
                }
                catch
                {
                    continue;
                }

                break;
            }
        }

        private void DrawMeshOnBitmap(Graphics g)
        {
            using(var pen = new Pen(Color.Gray, 0.5f))
            {
                int increment = _bitmap.Width / 28;

                for (var i = 1; i < 28; i++)
                {
                    int offset = i * increment;

                    DrawVerticalLine(_bitmap.Height, offset, g, pen);

                    DrawHorizontalLine(_bitmap.Width, offset, g, pen);
                }
            }
        }

        private static void DrawVerticalLine(int distance, int offset, Graphics g, Pen pen)
        {
            var point1 = new Point(offset, 0);

            var point2 = new Point(offset, distance);

            g.DrawLine(pen, point1, point2);
        }

        private static void DrawHorizontalLine(int distance, int offset, Graphics g, Pen pen)
        {
            var point1 = new Point(0, offset);

            var point2 = new Point(distance, offset);

            g.DrawLine(pen, point1, point2);
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
