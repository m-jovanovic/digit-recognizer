using DigitRecognizer.MachineLearning.Infrastructure.Functions;

namespace DigitRecognizer.MachineLearning.Infrastructure.Factories
{
    /// <summary>
    /// Factory for getting instances of <see cref="IFunction"/> by providing the name of the function.
    /// </summary>
    public class FunctionFactory : AbstractTypeFactory<IFunction, string>
    {
        /// <summary>
        /// The singleton instance.
        /// </summary>
        private static FunctionFactory _instance;

        /// <summary>
        /// The object used for locking. Required for thread safety.
        /// </summary>
        private static readonly object Lock = new object();

        /// <summary>
        /// Gets the singleton instance of the <see cref="FunctionFactory"/> class.
        /// </summary>
        public static FunctionFactory Instance
        {
            get
            {
                lock (Lock)
                {
                    return _instance ?? (_instance = new FunctionFactory());
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InitializerFactory"/> class.
        /// </summary>
        private FunctionFactory() : base(typeof(IFunction), "Name")
        {
        }
    }
}
