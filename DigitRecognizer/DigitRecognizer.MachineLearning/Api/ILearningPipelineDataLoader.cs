namespace DigitRecognizer.MachineLearning.Interfaces.Pipeline
{
    public interface ILearningPipelineDataLoader : ILearningPipelineItem
    {
        object LoadData();
    }
}
