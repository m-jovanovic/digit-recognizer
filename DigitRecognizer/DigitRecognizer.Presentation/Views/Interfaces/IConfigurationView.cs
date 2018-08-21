using System;

namespace DigitRecognizer.Presentation.Views.Interfaces
{
    /// <summary>
    /// Interface for the configuration view.
    /// </summary>
    public interface IConfigurationView
    {
        event EventHandler LoadPredictionModel;
        event EventHandler LoadClusterPredictionModel;

        bool IsPredictionModelLoaded { get; }


    }
}
