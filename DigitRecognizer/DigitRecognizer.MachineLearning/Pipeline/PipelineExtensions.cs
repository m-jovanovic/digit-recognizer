using DigitRecognizer.Core.Utilities;
using DigitRecognizer.MachineLearning.Infrastructure.Initialization;

namespace DigitRecognizer.MachineLearning.Pipeline
{
    /// <summary>
    /// Contains extension methods for configuring the <see cref="LearningPipeline"/>.
    /// </summary>
    public static class PipelineExtensions
    {
        public static LearningPipeline UseGradientClipping(this LearningPipeline pipeline)
        {
            pipeline.PipelineSettings.UseGradientClipping = true;

            return pipeline;
        }

        public static LearningPipeline UseL2Regularization(this LearningPipeline pipeline)
        {
            pipeline.PipelineSettings.UseL2Regularization = true;

            return pipeline;
        }

        public static LearningPipeline UseBiasRegularization(this LearningPipeline pipeline)
        {
            pipeline.PipelineSettings.UseBiasRegularization = true;

            return pipeline;
        }
        
        public static LearningPipeline SetWeightsInitializer(this LearningPipeline pipeline, InitializerType initializerType)
        {
            pipeline.PipelineSettings.WeightsInitializerType = initializerType;

            return pipeline;
        }

        public static LearningPipeline SetEpochCount(this LearningPipeline pipeline, int epochCount)
        {
            Contracts.ValueGreaterThanZero(epochCount, nameof(epochCount));

            pipeline.PipelineSettings.EpochCount = epochCount;

            return pipeline;
        }

        public static LearningPipeline SetBatchSize(this LearningPipeline pipeline, int batchSize)
        {
            Contracts.ValueGreaterThanZero(batchSize, nameof(batchSize));

            pipeline.PipelineSettings.BatchSize = batchSize;

            return pipeline;
        }

        public static LearningPipeline SetDatasetSize(this LearningPipeline pipeline, int datasetSize)
        {
            Contracts.ValueGreaterThanZero(datasetSize, nameof(datasetSize));

            pipeline.PipelineSettings.DatasetSize = datasetSize;

            return pipeline;
        }

        public static LearningPipeline ResetPipelineSettings(this LearningPipeline pipeline)
        {
            pipeline.PipelineSettings.Reset();

            return pipeline;
        }
    }
}
