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
            PipelineSettings.Instance.UseGradientClipping = true;

            return pipeline;
        }

        public static LearningPipeline UseL2Regularization(this LearningPipeline pipeline)
        {
            PipelineSettings.Instance.UseL2Regularization = true;

            return pipeline;
        }

        public static LearningPipeline UseBiasRegularization(this LearningPipeline pipeline)
        {
            PipelineSettings.Instance.UseBiasRegularization = true;

            return pipeline;
        }
        
        public static LearningPipeline SetWeightsInitializer(this LearningPipeline pipeline, InitializerType initializerType)
        {
            PipelineSettings.Instance.WeightsInitializerType = initializerType;

            return pipeline;
        }

        public static LearningPipeline ResetPipelineSettings(this LearningPipeline pipeline)
        {
            PipelineSettings.Instance.Reset();

            return pipeline;
        }
    }
}
