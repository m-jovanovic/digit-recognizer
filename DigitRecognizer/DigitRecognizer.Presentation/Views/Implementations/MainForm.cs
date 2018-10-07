using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DigitRecognizer.Presentation.Views.Interfaces;

namespace DigitRecognizer.Presentation.Views.Implementations
{
    public partial class MainForm : Form, IMainFormView
    {
        #region Fields

        private BenchmarkView _benchmarkView;
        private DrawingView _drawingView;
        private UploadImageView _uploadImageView;
        private SlidingWindowView _slidingWindowView;
        private List<IView> _views;

        #endregion

        #region Ctor

        public MainForm()
        {
            InitializeComponent();

            InitializeViews();

            HideView();
        }

        #endregion

        #region Properties
        
        public IBenchmarkView BenchmarkView => _benchmarkView;

        public IDrawingView DrawingView => _drawingView;

        public IUploadImageView UploadImageView => _uploadImageView;

        public ISlidingWindowView SlidingWindowView => _slidingWindowView;

        #endregion

        #region Methods

        private void InitializeViews()
        {
            FillControlsCollection();

            FillViewsCollection();
        }

        private void FillControlsCollection()
        {
            _benchmarkView = new BenchmarkView { Dock = DockStyle.Fill };
            _drawingView = new DrawingView { Dock = DockStyle.Fill };
            _uploadImageView = new UploadImageView { Dock = DockStyle.Fill };
            _slidingWindowView = new SlidingWindowView { Dock = DockStyle.Fill, Padding = new Padding(10)};

            Control[] controls =
            {
                _benchmarkView,
                _drawingView,
                _uploadImageView,
                _slidingWindowView
            };

            Controls.AddRange(controls);
        }

        private void FillViewsCollection()
        {
            _views = new List<IView>
            {
                _benchmarkView,
                _drawingView,
                _uploadImageView,
                _slidingWindowView,
                this
            };

            HideViews();

            _views.First().ShowView();
        }

        private void HideViews() => _views.ForEach(x => x.HideView());

        #endregion

        #region Event handlers
        
        private void BenchmarkToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            ToolstripMenuItem_Click_DisplayView(_benchmarkView);
        }

        private void DrawingToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            ToolstripMenuItem_Click_DisplayView(_drawingView);
        }

        private void UploadToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            ToolstripMenuItem_Click_DisplayView(_uploadImageView);
        }

        private void SlidingWindowToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            ToolstripMenuItem_Click_DisplayView(_slidingWindowView);
        }

        private static void ToolstripMenuItem_Click_DisplayView(IView view)
        {
            view.ShowView();
        }

        #endregion

        #region IView implementation

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
