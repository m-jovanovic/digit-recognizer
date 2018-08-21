using DigitRecognizer.Core.Utilities;
using DigitRecognizer.MachineLearning.Infrastructure.Dropout;
using DigitRecognizer.MachineLearning.Infrastructure.Initialization;
using DigitRecognizer.MachineLearning.Optimization.LearningRateDecay;

namespace DigitRecognizer.MachineLearning.Pipeline
{
    /// <summary>
    /// Contains extension methods for configuring the <see cref="LearningPipeline"/>.
    /// </summary>
    public static class PipelineExtensions
    {
        /// <summary>
        /// Configures the <see cref="LearningPipeline"/> to use gradient clipping.
        /// </summary>
        /// <param name="pipeline">The learning pipeline.</param>
        /// <returns>The configured pipeline.</returns>
        public static LearningPipeline UseGradientClipping(this LearningPipeline pipeline)
        {
            pipeline.PipelineSettings.UseGradientClipping = true;

            return pipeline;
        }

        /// <summary>
        /// Configures the <see cref="LearningPipeline"/> to use L2 regularization.
        /// </summary>
        /// <param name="pipeline">The learning pipeline.</param>
        /// <param name="regularizationFactor">The regularization factor.</param>
        /// <returns>The configured pipeline.</returns>
        public static LearningPipeline UseL2Regularization(this LearningPipeline pipeline, double regularizationFactor)
        {
            pipeline.PipelineSettings.UseL2Regularization = true;

            return pipeline.SetRegularizationFactor(regularizationFactor);
        }

        /// <summary>
        /// Configures the <see cref="LearningPipeline"/> to use learning rate decay.
        /// </summary>
        /// <param name="pipeline">The learning pipeline.</param>
        /// <param name="learingRateDecay">The learning rate decay scheduler.</param>
        /// <returns>The configured pipeline.</returns>
        public static LearningPipeline UseLearningRateDecay(this LearningPipeline pipeline, ILearningRateDecay learingRateDecay)
        {
            pipeline.PipelineSettings.UseLearningRateDecay = true;
            pipeline.PipelineSettings.LearningRateScheduler = learingRateDecay;

            return pipeline;
        }

        /// <summary>
        /// Sets the regularization factor setting of the <see cref="LearningPipeline"/> to the specified value.
        /// </summary>
        /// <param name="pipeline">The learning pipeline.</param>
        /// <param name="regularizationFactor">The regularization factor.</param>
        /// <returns>The configured pipeline.</returns>
        internal static LearningPipeline SetRegularizationFactor(this LearningPipeline pipeline, double regularizationFactor)
        {
            Contracts.ValueGreaterThanZero(regularizationFactor, nameof(regularizationFactor));

            pipeline.PipelineSettings.RegularizationFactor = regularizationFactor;

            return pipeline;
        }

        /// <summary>
        /// Configures the <see cref="LearningPipeline"/> to use dropout.
        /// </summary>
        /// <param name="pipeline">The learning pipeline.</param>
        /// <param name="keepProbability">The keep probability.</param>
        /// <returns>The configured pipeline.</returns>
        public static LearningPipeline UseDropout(this LearningPipeline pipeline, double keepProbability)
        {
            pipeline.PipelineSettings.UseDropout = true;
            
            pipeline.PipelineSettings.Dropout = new Dropout(keepProbability);

            return pipeline;
        }

        /// <summary>
        /// Configures the sizes of the hidden layers of the network.
        /// </summary>
        /// <param name="pipeline">The learning pipeline.</param>
        /// <param name="sizes">The array of sizes.</param>
        /// <returns>The configured pipeline.</returns>
        internal static LearningPipeline SetHiddenLayerSizes(this LearningPipeline pipeline, int[] sizes)
        {
            pipeline.PipelineSettings.HiddenLayerSizes = sizes;

            return pipeline;
        }

        /// <summary>
        /// Setst the <see cref="InitializerType"/> setting of the <see cref="LearningPipeline"/> to the specified value.
        /// </summary>
        /// <param name="pipeline">The learning pipeline.</param>
        /// <param name="initializerType">The type of initializer.</param>
        /// <returns>The configured pipeline.</returns>
        public static LearningPipeline SetWeightsInitializer(this LearningPipeline pipeline, InitializerType initializerType)
        {
            pipeline.PipelineSettings.WeightsInitializerType = initializerType;

            return pipeline;
        }

        /// <summary>
        /// Sets the epoch count setting of the <see cref="LearningPipeline"/> to the specified value.
        /// </summary>
        /// <param name="pipeline">The learning pipeline.</param>
        /// <param name="epochCount">The number of epochs.</param>
        /// <returns>The configured pipeline.</returns>
        public static LearningPipeline SetEpochCount(this LearningPipeline pipeline, int epochCount)
        {
            Contracts.ValueGreaterThanZero(epochCount, nameof(epochCount));

            pipeline.PipelineSettings.EpochCount = epochCount;

            return pipeline;
        }

        /// <summary>
        /// Sets the batchs size setting of the <see cref="LearningPipeline"/> to the specified value.
        /// </summary>
        /// <param name="pipeline">The learning pipeline.</param>
        /// <param name="batchSize">The batch size.</param>
        /// <returns>The configured pipeline.</returns>
        internal static LearningPipeline SetBatchSize(this LearningPipeline pipeline, int batchSize)
        {
            Contracts.ValueGreaterThanZero(batchSize, nameof(batchSize));

            pipeline.PipelineSettings.BatchSize = batchSize;

            return pipeline;
        }

        /// <summary>
        /// Sets the dataset size setting of the <see cref="LearningPipeline"/> to the specified value.
        /// </summary>
        /// <param name="pipeline">The learning pipeline.</param>
        /// <param name="datasetSize">The dataset size.</param>
        /// <returns>The configured pipeline.</returns>
        internal static LearningPipeline SetDatasetSize(this LearningPipeline pipeline, int datasetSize)
        {
            Contracts.ValueGreaterThanZero(datasetSize, nameof(datasetSize));

            pipeline.PipelineSettings.DatasetSize = datasetSize;

            return pipeline;
        }
        
        /// <summary>
        /// Resets the <see cref="LearningPipeline"/> settings to default values.
        /// </summary>
        /// <param name="pipeline">The learning pipeline.</param>
        /// <returns>The configured pipeline.</returns>
        public static LearningPipeline ResetPipelineSettings(this LearningPipeline pipeline)
        {
            pipeline.PipelineSettings.Reset();

            return pipeline;
        }
    }
}
