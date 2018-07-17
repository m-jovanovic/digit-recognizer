namespace DigitRecognizer.MachineLearning.Api
{
    public interface ILearningPipelineDataLoader : ILearningPipelineItem
    {
        object LoadData();
    }
}
