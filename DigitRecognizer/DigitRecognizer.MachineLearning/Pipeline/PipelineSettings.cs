using DigitRecognizer.MachineLearning.Infrastructure.Data;
using DigitRecognizer.MachineLearning.Infrastructure.Initialization;

namespace DigitRecognizer.MachineLearning.Pipeline
{
    /// <summary>
    /// Represents a singleton class with settings for configuring behaviour of the pipeline.
    /// </summary>
    public class PipelineSettings
    {
        private static PipelineSettings _instance;

        /// <summary>
        /// Initializes a new instance of the <see cref="PipelineSettings"/> class.
        /// This constructor is only called by the singleton instance.
        /// </summary>
        private PipelineSettings()
        {
            WeightsInitializerType = InitializerType.RandomInitialization;
        }

        /// <summary>
        /// Resets the <seealso cref="Instance"/>.
        /// </summary>
        public void Reset()
        {
            _instance = new PipelineSettings();
        }

        /// <summary>
        /// The object used for locking. Required for thread safety.
        /// </summary>
        private static readonly object Lock = new object();

        /// <summary>
        /// Gets the singleton instance of the <see cref="PipelineSettings"/> class.
        /// </summary>
        public static PipelineSettings Instance
        {
            get
            {
                lock (Lock)
                {
                    return _instance ?? (_instance = new PipelineSettings());
                }
            }
        }

        /// <summary>
        /// Gets or sets the use gradient clipping property.
        /// </summary>
        /// <remarks>
        /// If true, gradient clipping technique is used in the gradient calculation method.
        /// This is useful to avoid large gradient values, that could break learning.
        /// </remarks>
        public bool UseGradientClipping { get; set; }

        /// <summary>
        /// Gets or sets the use L2 regularization property.
        /// </summary>
        /// <remarks>
        /// If true, L2 regularization is applied  on weight or bias updates.
        /// L2 regularization is used to avoid large weight or bias values.
        /// It is applied on biases, if <seealso cref="UseBiasRegularization"/> is true.  
        /// </remarks>
        public bool UseL2Regularization { get; set; }

        /// <summary>
        /// Gets or sets the use bias regularization property.
        /// </summary>
        /// <remarks>
        /// If true, regularization is applied on bias updates.
        /// </remarks>
        public bool UseBiasRegularization { get; set; }
        
        /// <summary>
        /// Gets or sets the weights <see cref="InitializerType"/>.
        /// </summary>
        public InitializerType WeightsInitializerType { get; set; }
    }
}
