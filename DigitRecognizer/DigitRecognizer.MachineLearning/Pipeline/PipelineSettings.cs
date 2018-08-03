using System;
using DigitRecognizer.MachineLearning.Infrastructure.Initialization;

namespace DigitRecognizer.MachineLearning.Pipeline
{
    /// <summary>
    /// Represents a singleton class with settings for configuring behaviour of the pipeline.
    /// </summary>
    internal class PipelineSettings
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
        internal void Reset()
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
        internal static PipelineSettings Instance
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
        /// Gets or sets a value indicating if the current environment is a training environment.
        /// </summary>
        internal bool IsPipelingRunning { get; set; }

        /// <summary>
        /// Gets or sets the use gradient clipping property.
        /// </summary>
        /// <remarks>
        /// If true, gradient clipping technique is used in the gradient calculation method.
        /// This is useful to avoid large gradient values, that could break learning.
        /// </remarks>
        internal bool UseGradientClipping { get; set; }

        /// <summary>
        /// Gets or sets the use L2 regularization property.
        /// </summary>
        /// <remarks>
        /// If true, L2 regularization is applied  on weight or bias updates.
        /// L2 regularization is used to avoid large weight or bias values.
        /// It is applied on biases, if <seealso cref="UseBiasRegularization"/> is true.  
        /// </remarks>
        internal bool UseL2Regularization { get; set; }

        /// <summary>
        /// Gets or sets the use bias regularization property.
        /// </summary>
        /// <remarks>
        /// If true, regularization is applied on bias updates.
        /// </remarks>
        internal bool UseBiasRegularization { get; set; }

        /// <summary>
        /// Gets or sets the weights <see cref="InitializerType"/>.
        /// </summary>
        internal InitializerType WeightsInitializerType { get; set; }

        /// <summary>
        /// Gets or sets the number of epochs.
        /// </summary>
        internal int EpochCount { get; set; }

        /// <summary>
        /// Gets or sets the batch size.
        /// </summary>
        internal int BatchSize { get; set; }
        
        /// <summary>
        /// Gets or sets the dataset size.
        /// </summary>
        internal int DatasetSize { get; set; }

        /// <summary>
        /// Gets the number of iterations of the training cycle.
        /// </summary>
        internal int TrainingIterationsCount => (int) Math.Round(DatasetSize / (double)BatchSize);
    }
}
