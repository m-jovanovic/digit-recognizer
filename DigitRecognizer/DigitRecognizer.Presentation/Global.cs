using DigitRecognizer.MachineLearning.Infrastructure.Models;
using DigitRecognizer.Presentation.Infrastructure;

namespace DigitRecognizer.Presentation
{
    public static class Global
    {
        public static IPredictionModel PredictionModel { get; set; }

        public static readonly int ImageGridFieldCount = 100;

        public static IPredictionModel LoadModel()
        {
            var loader = new PredictionModelLoader();

            IPredictionModel model = loader.Load();

            return model;
        }
    }
}
