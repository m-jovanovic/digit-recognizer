using System.Collections.Generic;
using System.Windows.Forms;
using DigitRecognizer.Presentation.Views.Interfaces;

namespace DigitRecognizer.Presentation.Views.Implementations
{
    public partial class MainForm : Form, IMainFormView
    {
        private BenchmarkView _benchmarkView;
        private List<IView> _views;

        public MainForm()
        {
            InitializeComponent();

            InitializeViews();

            HideView();
        }

        private void InitializeViews()
        {
            FillControlsCollection();

            FillViewsCollection();
        }

        private void FillControlsCollection()
        {
            _benchmarkView = new BenchmarkView { Dock = DockStyle.Fill };

            Controls.Add(_benchmarkView);
        }

        private void FillViewsCollection()
        {
            _views = new List<IView>
            {
                this,
                BenchmarkView
            };
            
            _views.ForEach(x => x.HideView());
        }

        public IBenchmarkView BenchmarkView => _benchmarkView;

        private void BenchmarkToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            _benchmarkView.ShowView();
        }

        public void ShowView()
        {
            Show();
        }

        public void HideView()
        {
            Hide();
        }
    }

    //public partial class MainForm : Form
    //{
    //    private Point _currentPoint;
    //    private Point _previousPoint;
    //    private readonly Pen _pen = new Pen(Color.Black, 70);
    //    private readonly Bitmap _bitmap;
    //    private PredictionModel _predictionModel;

    //    public MainForm()
    //    {
    //        InitializeComponent();
    //        _bitmap = new Bitmap(drawingPanel.Width, drawingPanel.Height);
    //        using (Graphics g = Graphics.FromImage(_bitmap))
    //        {
    //            g.Clear(Color.White);
    //        }
    //        _pen.SetLineCap(LineCap.Round, LineCap.Round, DashCap.Round);

    //        openFileDialog.InitialDirectory = Path.GetFullPath(Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory) + DirectoryHelper.ModelsFolder);
    //        openFileDialog.Filter = @"Neural network files (*.nn)|*.nn";

    //        drawingPanel.MouseDown += DrawingPanel_MouseDown;
    //        drawingPanel.MouseMove += DrawingPanel_MouseMove;
    //        drawingPanel.Paint += DrawingPanel_Paint;

    //        btnClear.Click += BtnClear_Click;
    //        btnDecide.Click += BtnDecide_Click;

    //        lblDecision.Text = string.Empty;
    //    }

    //    private void DrawingPanel_MouseDown(object sender, MouseEventArgs e)
    //    {
    //        _previousPoint = e.Location;
    //    }

    //    private void DrawingPanel_MouseMove(object sender, MouseEventArgs e)
    //    {
    //        if (e.Button == MouseButtons.Left)
    //        {
    //            _currentPoint = e.Location;
    //            const int retryCount = 5;
    //            for (var i = 0; i < retryCount; i++)
    //            {
    //                try
    //                {
    //                    using (Graphics g = Graphics.FromImage(_bitmap))
    //                    {
    //                        g.DrawLine(_pen, _previousPoint, _currentPoint);
    //                    }
    //                }
    //                catch
    //                {
    //                    continue;
    //                }

    //                break;
    //            }
    //            drawingPanel.Invalidate();
    //            _previousPoint = _currentPoint;
    //        }
    //    }

    //    private void DrawingPanel_Paint(object sender, PaintEventArgs e)
    //    {
    //        e.Graphics.DrawImage(_bitmap, Point.Empty);
    //    }

    //    private void BtnClear_Click(object sender, EventArgs e)
    //    {
    //        Clear();
    //    }

    //    private void Clear()
    //    {
    //        const int retryCount = 5;
    //        for (var i = 0; i < retryCount; i++)
    //        {
    //            try
    //            {
    //                using (Graphics g = Graphics.FromImage(_bitmap))
    //                {
    //                    g.Clear(Color.White);
    //                }
    //            }
    //            catch
    //            {
    //                continue;
    //            }

    //            break;
    //        }
    //        drawingPanel.Invalidate();
    //        lblDecision.Text = string.Empty;
    //    }

    //    private void BtnDecide_Click(object sender, EventArgs e)
    //    {
    //        if (_predictionModel == null)
    //        {
    //            MessageBox.Show(@"No neural network is currently loaded", @"Neural Network Info", MessageBoxButtons.OK,
    //                MessageBoxIcon.Information);

    //            return;
    //        }

    //        var processor = new ImagePreprocessor();

    //        double[] pixels = processor.Preprocess(_bitmap);

    //        double[] prediction = _predictionModel.Predict(pixels);

    //        int argMax = prediction.ArgMax();

    //        double percent = prediction[argMax];

    //        lblDecision.Text = $@"Network: {argMax} ({percent:P2})";
    //    }

    //    private void LoadToolStripMenuItem_Click(object sender, EventArgs e)
    //    {
    //        DialogResult dialogResult = openFileDialog.ShowDialog();

    //        if (dialogResult == DialogResult.OK && !string.IsNullOrEmpty(openFileDialog.FileName))
    //        {
    //            _predictionModel = PredictionModel.FromFile(openFileDialog.FileName);
    //        }
    //    }
    //}
}
