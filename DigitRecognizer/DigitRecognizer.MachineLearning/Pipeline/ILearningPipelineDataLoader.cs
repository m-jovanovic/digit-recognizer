namespace DigitRecognizer.MachineLearning.Pipeline
{
    public interface ILearningPipelineDataLoader : ILearningPipelineItem
    {
        object LoadData();
    }
}
