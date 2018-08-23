using System;
using System.Collections.Generic;
using DigitRecognizer.Core.Utilities;
using DigitRecognizer.DatasetExpansion.Api;
using DigitRecognizer.DatasetExpansion.Infrastructure;

namespace DigitRecognizer.DatasetExpansion
{
    internal class Program
    {
        private static void Main()
        {
            Console.Title = "Digit Recognizer - Dataset Expansion";

            Console.WriteLine("Starting dataset expansion");
            
            var expander = new DatasetExpander();
            
            List<MnistImage> expandedDataset = expander.ExpandDataset();
            
            Console.WriteLine("Completed dataset expansion");

            Console.WriteLine("Starting dataset serialization");

            var serializer = new DatasetSerializer();

            serializer.SerializeDataset(DirectoryHelper.ExpandedTrainLabelsPath, DirectoryHelper.ExpandedTrainImagesPath, expandedDataset);

            Console.WriteLine("Completed dataset serialization");

            Console.ReadKey();
        }
    }
}
