using System;
using System.Collections.Generic;
using DigitRecognizer.MachineLearning.Infrastructure.Dropout;
using DigitRecognizer.MachineLearning.Infrastructure.Initialization;
using DigitRecognizer.MachineLearning.Optimization.LearningRateDecay;

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
            LearningRateScheduler = null;
            Dropout = null;
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
        /// If true, L2 regularization is applied  on weight updates.
        /// L2 regularization is used to avoid large weights.
        /// </remarks>
        internal bool UseL2Regularization { get; set; }

        /// <summary>
        /// Gets or sets the regularization factor, also called lambda.
        /// </summary>
        internal double RegularizationFactor { get; set; }

        /// <summary>
        /// Gets or sets the weights <see cref="InitializerType"/>.
        /// </summary>
        internal InitializerType WeightsInitializerType { get; set; }

        /// <summary>
        /// Gets or sets the number of epochs.
        /// </summary>
        internal int EpochCount { get; set; }

        /// <summary>
        /// Gets or sets the current epoch.
        /// </summary>
        internal int CurrentEpoch { get; set; }

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

        /// <summary>
        /// Gets or sets the current training iteration.
        /// </summary>
        internal int CurrentIteration { get; set; }

        /// <summary>
        /// Gets or sets the use learning rate decay property.
        /// </summary>
        /// <remarks>
        /// If true, learning rate decay will be aplied to the model during training.
        /// </remarks>
        internal bool UseLearningRateDecay { get; set; }

        /// <summary>
        /// Gets or sets the learning rate schduler.
        /// </summary>
        internal ILearningRateDecay LearningRateScheduler { get; set; }

        /// <summary>
        /// Gets or sets the use dropout property.
        /// </summary>
        internal bool UseDropout { get; set; }

        /// <summary>
        /// Gets or sets the dropout property.
        /// </summary>
        internal Dropout Dropout { get; set; }

        /// <summary>
        /// Gets or sets the layer sizes.
        /// </summary>
        internal int[] HiddenLayerSizes { get; set; }

        /// <summary>
        /// Gets the value indicating if dropout can be performed.
        /// </summary>
        internal bool CanPerformDropout => UseDropout && Dropout != null && HiddenLayerSizes.Length > 0;
        
        /// <summary>
        /// Gets or sets the dropout vectors.
        /// </summary>
        internal List<double[]> DropoutVectors { get; set; }
    }
}
