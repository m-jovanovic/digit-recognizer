using System.Collections.Generic;
using DigitRecognizer.Core.Extensions;
using DigitRecognizer.Core.Utilities;
using DigitRecognizer.MachineLearning.Infrastructure.NeuralNetwork;
using DigitRecognizer.MachineLearning.Serialization;

namespace DigitRecognizer.MachineLearning.Infrastructure.Models
{
    /// <summary>
    /// Represents a model that runs an internal <see cref="NeuralNetwork.NeuralNetwork"/> to predict input values.
    /// </summary>
    public class PredictionModel
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

        public static PredictionModel FromFile(string filename)
        {
            Contracts.FileExists(filename, nameof(filename));
            Contracts.FileExtensionValid(filename, ".nn", nameof(filename));
            
            var deserializer = new NnDeserializer();

            IEnumerable<NnLayer> layers = deserializer.Deseralize(filename);

            var neuralNetwork = new NeuralNetwork.NeuralNetwork(layers, 1.0);

            return new PredictionModel(neuralNetwork);
        }
    }
}
