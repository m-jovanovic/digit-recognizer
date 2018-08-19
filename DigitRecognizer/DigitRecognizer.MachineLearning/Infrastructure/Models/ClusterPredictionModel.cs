using System.Collections.Generic;
using System.Linq;
using DigitRecognizer.Core.Extensions;
using DigitRecognizer.Core.Utilities;
using DigitRecognizer.MachineLearning.Infrastructure.NeuralNetwork;

namespace DigitRecognizer.MachineLearning.Infrastructure.Models
{
    public class ClusterPredictionModel : IPredictionModel
    {
        private readonly List<PredictionModel> _cluster;

        public ClusterPredictionModel(IEnumerable<PredictionModel> models)
        {
            Contracts.ValueNotNull(models, nameof(models));

            _cluster = new List<PredictionModel>(models);
        }

        public ClusterPredictionModel(IEnumerable<INeuralNetwork> networks)
        {
            Contracts.ValueNotNull(networks, nameof(networks));

            _cluster = new List<PredictionModel>(networks.Select(x=> new PredictionModel(x)));
        }

        /// <summary>
        /// Predicts the output for the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>The output as a vector of probabilites.</returns>
        public double[] Predict(double[] input)
        {
            var predictions = new double[_cluster.Count][];

            for (var i = 0; i < _cluster.Count; i++)
            {
                double[] prediction = _cluster[i].Predict(input);

                predictions[i] = prediction;
            }

            double[] result = predictions.Average();

            return result;
        }

        public static ClusterPredictionModel FromFiles(string[] filenames)
        {
            return new ClusterPredictionModel(filenames.Select(PredictionModel.FromFile));
        }
    }
}
