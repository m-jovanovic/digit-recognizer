using DigitRecognizer.MachineLearning.Infrastructure.Models;
using DigitRecognizer.Presentation.Infrastructure;

namespace DigitRecognizer.Presentation
{
    public static class Global
    {
        private static IPredictionModel _predictionModel;

        public static IPredictionModel PredictionModel => _predictionModel ?? (_predictionModel = LoadModel());

        public static readonly int ImageGridFieldCount = 100;

        private static IPredictionModel LoadModel()
        {
            var loader = new PredictionModelLoader();

            IPredictionModel model = loader.Load();

            return model;
        }
    }
}
