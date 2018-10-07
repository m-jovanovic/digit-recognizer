using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using DigitRecognizer.Core.Utilities;
using DigitRecognizer.Presentation.Infrastructure;
using DigitRecognizer.Presentation.Views.Interfaces;

namespace DigitRecognizer.Presentation.Views.Implementations
{
    public partial class UploadImageView : UserControl, IUploadImageView
    {
        #region Fields

        private Image _image;
        private OpenFileDialog _openFileDialog;

        #endregion

        #region Ctor
        
        public UploadImageView()
        {
            InitializeComponent();

            Clear();

            _openFileDialog = new OpenFileDialog
            {
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                Filter = @"Image|*.bmp;*.png;*.jpeg;*.jpg"
            };

            PanelDoubleBuffering.Enable(uploadPanel);

            btnClassifyImg.Click += BtnClassifyImgOnClick;

            btnClearImg.Click += BtnClearImgOnClick;

            btnUploadImg.Click += BtnUploadImgOnClick;

            uploadPanel.Paint += UploadPanelOnPaint; 
        }

        #endregion

        #region Properties

        public Image Image => _image ?? throw new NullReferenceException(nameof(_image));

        #endregion

        #region Event handlers

        public event EventHandler ClassifyImage;
        public event EventHandler ClearImage;

        #endregion

        #region Methods
        
        private void BtnClassifyImgOnClick(object sender, EventArgs e)
        {
            ClassifyImage?.Invoke(this, EventArgs.Empty);
        }

        private void BtnClearImgOnClick(object sender, EventArgs e)
        {
            ClearImage?.Invoke(this, EventArgs.Empty);
        }

        private void BtnUploadImgOnClick(object sender, EventArgs e)
        {
            DialogResult dlgResult = _openFileDialog.ShowDialog();

            if (dlgResult == DialogResult.OK)
            {
                Image rawImage = Image.FromFile(_openFileDialog.FileName);

                _image = ImageUtilities.Resize(rawImage, uploadPanel.Width, uploadPanel.Height);

                predictionPane.Clear();

                uploadPanel.Invalidate();
            }
        }

        private void UploadPanelOnPaint(object sender, PaintEventArgs e)
        {
            if (_image is null)
            {
                return;
            }

            e.Graphics.DrawImage(_image, Point.Empty);
        }

        protected override void OnResize(EventArgs e)
        {
            uploadPanel.Top = (Height - uploadPanel.Height) / 2;
            uploadPanel.Left = (Width - (uploadPanel.Width + predictionPane.Width - 35)) / 2;

            lblInstructions.Left = uploadPanel.Left - 5;
            lblInstructions.Top = uploadPanel.Top - lblInstructions.Height - 5;

            int top = uploadPanel.Bottom + 5;

            btnClassifyImg.Left = uploadPanel.Left;
            btnClassifyImg.Top = top;

            btnClearImg.Left = btnClassifyImg.Right + 5;
            btnClearImg.Top = top;

            btnUploadImg.Left = uploadPanel.Right - btnUploadImg.Width;
            btnUploadImg.Top = top;

            predictionPane.Left = uploadPanel.Right + 40;
            predictionPane.Top = uploadPanel.Top;

            base.OnResize(e);
        }

        #endregion

        #region IUploadImageView implementation

        public void Clear()
        {
            _image = null;

            predictionPane.Clear();
            
            uploadPanel.Invalidate();
        }

        public void ProcessPrediction(double[] prediction)
        {
            predictionPane.ProcessPrediction(prediction);
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
