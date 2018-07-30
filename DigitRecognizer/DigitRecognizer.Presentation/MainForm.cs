using DigitRecognizer.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;
using DigitRecognizer.Core.Extensions;
using DigitRecognizer.MachineLearning.Infrastructure;
using DigitRecognizer.MachineLearning.Serialization;

namespace DigitRecognizer.Presentation
{
    public partial class MainForm : Form
    {
        private Point _currentPoint;
        private Point _previousPoint;
        private readonly Pen _pen = new Pen(Color.Black, 70);
        private readonly Bitmap _bitmap;
        private NeuralNetwork _neuralNetwork;

        public MainForm()
        {
            InitializeComponent();
            _bitmap = new Bitmap(drawingPanel.Width, drawingPanel.Height);
            using (Graphics g = Graphics.FromImage(_bitmap))
            {
                g.Clear(Color.White);
            }
            _pen.SetLineCap(LineCap.Round, LineCap.Round, DashCap.Round);

            openFileDialog.InitialDirectory = Path.GetFullPath(Path.GetDirectoryName(Application.ExecutablePath) + DirectoryHelper.ModelsFolder);
            openFileDialog.Filter = @"Neural network files (*.nn)|*.nn";

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
            if (_neuralNetwork == null)
            {
                MessageBox.Show(@"No neural network is currently loaded", @"Neural Network Info", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            double[] pixels = _bitmap.Preprocess();
            double[][] prediction = _neuralNetwork.Decide(pixels.AsMatrix());
            int argMax = prediction[0].ArgMax();
            double percent = prediction[0][argMax];
            lblDecision.Text = $@"Network: {argMax} ({percent:P2})";
        }

        private void LoadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = openFileDialog.ShowDialog();

            if (dialogResult == DialogResult.OK && !string.IsNullOrEmpty(openFileDialog.FileName))
            {
                NnDeserializer deserializer = new NnDeserializer();
                IEnumerable<NnLayer> layers = deserializer.Deseralize(openFileDialog.FileName);
                _neuralNetwork = new NeuralNetwork(layers, 1.0);
            }
        }
    }
}
