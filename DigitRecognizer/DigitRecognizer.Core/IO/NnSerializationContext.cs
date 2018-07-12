namespace DigitRecognizer.Core.IO
{
    /// <summary>
    /// Model for neural netowrk serialization.
    /// </summary>
    public class NnSerializationContext
    {
        private readonly NnSerializationContextInfo _serializationContextInfo;
        private readonly double[] _data;

        /// <summary>
        /// Initializes a new instance of the <see cref="NnSerializationContext"/> class with the specified parameters.
        /// </summary>
        /// <param name="fileData">The file info to associate with the file.</param>
        /// <param name="serializationContextInfo">The data for the file.</param>
        public NnSerializationContext(double[] fileData, NnSerializationContextInfo serializationContextInfo)
        {
            _serializationContextInfo = serializationContextInfo;
            _data = fileData;
        }

        /// <summary>
        /// Gets the <see cref="NnSerializationContextInfo"/>.
        /// </summary>
        public NnSerializationContextInfo SerializationContextInfo => _serializationContextInfo;

        /// <summary>
        /// Gets the data.
        /// <para>
        /// The data is a combined array of all weights and biases.
        /// </para>
        /// </summary>
        public double[] FileData => _data;
    }
}
