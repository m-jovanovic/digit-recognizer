namespace DigitRecognizer.MachineLearning.Infrastructure.Initialization
{
    /// <summary>
    /// Represents the type of initialization method.
    /// </summary>
    public enum InitializerType
    {
        /// <summary>
        /// Initializes all values to zero.
        /// </summary>
        ZeroInitialization = 0,

        /// <summary>
        /// Initializes all values to random values.
        /// </summary>
        RandomInitialization = 1,

        /// <summary>
        /// Initializes all values using the Xavier initialization method.
        /// </summary>
        XavierInitialization = 2,

        /// <summary>
        /// Initializes all values using the He-et-al initialization method.
        /// </summary>
        HeInitialization = 3
    }
}
