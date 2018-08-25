﻿using System;
using DigitRecognizer.Presentation.Data;

namespace DigitRecognizer.Presentation.Views.Interfaces
{
    public interface IBenchmarkView : IView
    {
        event EventHandler RunBenchmark;
        event EventHandler CancelBenchmark;

        bool IsBenchmarkRunning { get; set; }

        void PerformProgressStep();
        void SetAccuracy(int accuracy);
        void DrawGrid(ImageGridModel model);
    }
}