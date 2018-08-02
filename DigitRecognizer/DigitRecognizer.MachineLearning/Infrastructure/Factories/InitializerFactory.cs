using DigitRecognizer.MachineLearning.Infrastructure.Initialization;

namespace DigitRecognizer.MachineLearning.Infrastructure.Factories
{
    /// <summary>
    /// Factory for getting instances of <see cref="IInitializer"/> by providing the <see cref="InitializerType"/>.
    /// </summary>
    public class InitializerFactory : AbstractTypeFactory<IInitializer, InitializerType>
    {
        /// <summary>
        /// The singleton instance.
        /// </summary>
        private static InitializerFactory _instance;

        /// <summary>
        /// The object used for locking. Required for thread safety.
        /// </summary>
        private static readonly object Lock = new object();

        /// <summary>
        /// Gets the singleton instance of the <see cref="InitializerFactory"/> class.
        /// </summary>
        public static InitializerFactory Instance
        {
            get
            {
                lock (Lock)
                {
                    return _instance ?? (_instance = new InitializerFactory());
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InitializerFactory"/> class.
        /// </summary>
        private InitializerFactory() : base(typeof(IInitializer), "InitializerType")
        {
        }
    }
}
