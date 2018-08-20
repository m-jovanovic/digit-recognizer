namespace DigitRecognizer.Core.Utilities
{
    /// <summary>
    /// Provides utility for accessing commonly required folders.
    /// </summary>
    public static class DirectoryHelper
    {
        /// <summary>
        /// The path to the dataset folder.
        /// </summary>
        private const string DatasetPath = @"../../../Dataset";

        /// <summary>
        /// The path to the models folder.
        /// </summary>
        private const string ModelsPath = @"../../../../Models";

        /// <summary>
        /// Gets the path to the training labels file.
        /// </summary>
        public static string TrainLabelsPath => $"{DatasetPath}/train-labels.idx1-ubyte";

        /// <summary>
        /// Gets the path to the training images file.
        /// </summary>
        public static string TrainImagesPath => $"{DatasetPath}/train-images.idx3-ubyte";

        /// <summary>
        /// Gets the path to the expaneded training labels file.
        /// </summary>
        public static string ExpandedTrainLabelsPath => $"{DatasetPath}/train-exp-labels.idx1-ubyte";

        /// <summary>
        /// Gets the path to the expanded training images file.
        /// </summary>
        public static string ExpandedTrainImagesPath => $"{DatasetPath}/train-exp-images.idx3-ubyte";

        /// <summary>
        /// Gets the path to the testing labels file.
        /// </summary>
        public static string TestLabelsPath => $"{DatasetPath}/t10k-labels.idx1-ubyte";

        /// <summary>
        /// Gets the path to the testing images file.
        /// </summary>
        public static string TestImagesPath => $"{DatasetPath}/t10k-images.idx3-ubyte";

        /// <summary>
        /// Gets the path to the models folder.
        /// </summary>
        public static string ModelsFolder => ModelsPath;
    }
}
