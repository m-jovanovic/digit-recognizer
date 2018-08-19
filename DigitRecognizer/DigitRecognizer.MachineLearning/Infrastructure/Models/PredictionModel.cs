using DigitRecognizer.Core.Extensions;
using DigitRecognizer.Core.Utilities;
using DigitRecognizer.MachineLearning.Infrastructure.NeuralNetwork;
using DigitRecognizer.MachineLearning.Serialization;

namespace DigitRecognizer.MachineLearning.Infrastructure.Models
{
    /// <summary>
    /// Represents a model that runs an internal neural network to predict input values.
    /// </summary>
    public class PredictionModel : IPredictionModel
    {
        private readonly INeuralNetwork _neuralNetwork;

        /// <summary>
        /// Initializes a new instance of the <see cref="PredictionModel"/> class.
        /// </summary>
        /// <param name="neuralNetwork"></param>
        public PredictionModel(INeuralNetwork neuralNetwork)
        {
            Contracts.ValueNotNull(neuralNetwork, nameof(neuralNetwork));

            _neuralNetwork = neuralNetwork;
        }

        /// <summary>
        /// Predicts the output for the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>The output as a vector of probabilites.</returns>
        public double[] Predict(double[] input)
        {
            double[][] prediction = _neuralNetwork.FeedForward(input.AsMatrix());

            return prediction[0];
        }

        /// <summary>
        /// Saves the <see cref="PredictionModel"/> to the specified file.
        /// </summary>
        /// <param name="filename">The filename.</param>
        public void Save(string filename)
        {
            var serializer = new NnSerializer();

            serializer.Serialize(filename, _neuralNetwork);
        }

        /// <summary>
        /// Creates a <see cref="PredictionModel"/> from the specified file.
        /// </summary>
        /// <param name="filename">The filename.</param>
        /// <returns>The deserialized prediction model.</returns>
        public static PredictionModel FromFile(string filename)
        {
            Contracts.FileExists(filename, nameof(filename));
            Contracts.FileExtensionValid(filename, ".nn", nameof(filename));
            
            var deserializer = new NnDeserializer();

            NeuralNetwork.NeuralNetwork neuralNetwork = deserializer.Deserialize(filename);

            return new PredictionModel(neuralNetwork);
        }
    }
}
