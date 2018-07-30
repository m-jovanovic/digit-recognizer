using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace DigitRecognizer.Presentation
{
    public partial class MainForm : Form
    {
        private Point _currentPoint;
        private Point _previousPoint;
        private readonly Pen _pen = new Pen(Color.Black, 55);
        private readonly Bitmap _bitmap;

        public MainForm()
        {
            InitializeComponent();
            _bitmap = new Bitmap(drawingPanel.Width, drawingPanel.Height);
            using (Graphics g = Graphics.FromImage(_bitmap))
            {
                g.Clear(Color.White);
            }
            _pen.SetLineCap(LineCap.Round, LineCap.Round, DashCap.Round);

            drawingPanel.MouseDown += DrawingPanel_MouseDown;
            drawingPanel.MouseMove += DrawingPanel_MouseMove;
            drawingPanel.Paint += DrawingPanel_Paint;

            btnClear.Click += BtnClear_Click;
            btnDecide.Click += BtnDecide_Click;

            lblDecision.Text = string.Empty;
        }

        private void DrawingPanel_MouseDown(object sender, MouseEventArgs e)
        {
            _previousPoint = e.Location;
        }

        private void DrawingPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _currentPoint = e.Location;
                const int retryCount = 5;
                for (var i = 0; i < retryCount; i++)
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
        }

        private void DrawingPanel_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(_bitmap, Point.Empty);
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void Clear()
        {
            const int retryCount = 5;
            for (var i = 0; i < retryCount; i++)
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
            drawingPanel.Invalidate();
            lblDecision.Text = string.Empty;
        }
        
        private void BtnDecide_Click(object sender, EventArgs e)
        {
            //double[] pixels = _bitmap.Preprocess();
            //var prediction = nn.Decide(pixels);
            //var argMax = prediction.ToList().IndexOf(prediction.Max());
            lblDecision.Text = "Hello thereeeeeeeeeeeeee"; //$@"{argMax} ({prediction[argMax]:P2})";
        }
    }
}
