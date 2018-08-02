﻿using System.Collections.Generic;
using System.IO;
using System.Linq;
using DigitRecognizer.Core.IO;
using DigitRecognizer.MachineLearning.Infrastructure;
using DigitRecognizer.MachineLearning.Infrastructure.NeuralNetwork;

namespace DigitRecognizer.MachineLearning.Serialization
{
    /// <summary>
    /// Serializer for serializing <see cref="NnLayer"/> objects.
    /// </summary>
    public class NnSerializer : INnSerializer<NnLayer>
    {
        /// <summary>
        /// Serializes the specified collection of <see cref="NnLayer"/> objects to a file.
        /// </summary>
        /// <param name="filename">The filename to write to.</param>
        /// <param name="collection">The collection of objects to serialize.</param>
        public void Serialize(string filename, IEnumerable<NnLayer> collection)
        {
            using (var serializer = new NnBinarySerializer(filename, FileMode.Create))
            {
                IEnumerable<NnSerializationContext> contexts = collection.Select(layer => layer.Serialize());

                serializer.Serialize(contexts);
            }
        }
    }
}
