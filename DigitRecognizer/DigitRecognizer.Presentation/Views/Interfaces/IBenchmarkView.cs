using System;

namespace DigitRecognizer.Presentation.Views.Interfaces
{
    public interface IBenchmarkView
    {
        event EventHandler RunBenchmark;

        void PerformProgressStep();
        void SetAccuracy(int accuracy);
    }
}
