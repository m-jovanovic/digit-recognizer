namespace DigitRecognizer.MachineLearning.Interfaces.Optimization
{
    /// <summary>
    /// 
    /// </summary>
    public interface IValueAdjustable
    {
        void AdjustValue(double[][] gradient, double learningRate);
    }
}
