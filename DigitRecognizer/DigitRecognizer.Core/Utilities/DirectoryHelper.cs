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
        private const string ModelsPath = @"../../../Models";

        /// <summary>
        /// Gets the path to the dataset folder.
        /// </summary>
        public static string DatasetFolder => DatasetPath;

        /// <summary>
        /// Gets the path to the models folder.
        /// </summary>
        public static string ModelsFolder => ModelsPath;
    }
}
