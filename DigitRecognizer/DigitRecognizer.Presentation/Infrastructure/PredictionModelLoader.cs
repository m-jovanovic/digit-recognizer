using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DigitRecognizer.Core.Utilities;
using DigitRecognizer.MachineLearning.Infrastructure.Models;

namespace DigitRecognizer.Presentation.Infrastructure
{
    public class PredictionModelLoader
    {
        private readonly OpenFileDialog _openFileDialog;

        public PredictionModelLoader()
        {
            _openFileDialog = new OpenFileDialog
            {
                Multiselect = true,
                CheckFileExists = true,
                InitialDirectory = Path.GetFullPath(Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory) + DirectoryHelper.ModelsFolder),
                Filter = @"Neural network file (*.nn)|*.nn"
            };
        }

        public IPredictionModel Load()
        {
            if (DialogResult.OK != _openFileDialog.ShowDialog() || _openFileDialog.FileNames.Length == 0 || _openFileDialog.FileNames.Any(string.IsNullOrWhiteSpace))
            {
                return null;
            }

            IPredictionModel model = ClusterPredictionModel.FromFiles(_openFileDialog.FileNames);

            return model;
        }
    }
}
