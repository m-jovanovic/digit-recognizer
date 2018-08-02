using System.Text;
using DigitRecognizer.Core.Utilities;

namespace DigitRecognizer.Core.IO
{
    /// <summary>
    /// Contains context information about a neural netowrk file.
    /// </summary>
    public struct NnSerializationContextInfo
    {
        private readonly int _weightMatrixRowCount;
        private readonly int _weightMatrixColCount;
        private readonly int _biasLength;
        private readonly string _activationFunctionName;

        /// <summary>
        /// Initializes a new instance of the <see cref="NnSerializationContextInfo"/> struct.
        /// </summary>
        /// <param name="weightMatrixRowCount">The number of rows of the weight matrix.</param>
        /// <param name="weightMatrixColCount">The number of columns of the weight matrix.</param>
        /// <param name="biasLength">The length of the bias.</param>
        /// <param name="activationFunctionName">The name of the activation function for the layer.</param>
        public NnSerializationContextInfo(int weightMatrixRowCount, int weightMatrixColCount, int biasLength, string activationFunctionName)
        {
            Contracts.ValueGreaterThanZero(weightMatrixRowCount, nameof(weightMatrixRowCount));
            Contracts.ValueGreaterThanZero(weightMatrixColCount, nameof(weightMatrixColCount));
            Contracts.ValueGreaterThanZero(biasLength, nameof(biasLength));
            
            _weightMatrixRowCount = weightMatrixRowCount;
            _weightMatrixColCount = weightMatrixColCount;
            _biasLength = biasLength;
            _activationFunctionName = activationFunctionName;
        }

        /// <summary>
        /// Gets the weight matrix row count.
        /// </summary>
        public int WeightMatrixRowCount => _weightMatrixRowCount;

        /// <summary>
        /// Gets the weight matrix column count.
        /// </summary>
        public int WeightMatrixColCount => _weightMatrixColCount;

        /// <summary>
        /// Gets the bias length.
        /// </summary>
        public int BiasLength => _biasLength;
        
        /// <summary>
        /// Gets the name of the activation function.
        /// </summary>
        public string ActivationFunctionName => _activationFunctionName;

        /// <summary>
        /// Gets the number of bytes in the activation function name.
        /// </summary>
        public int ActivationFunctionNameSizeInBytes => Encoding.Unicode.GetByteCount(_activationFunctionName);

        /// <summary>
        /// Gets the length of the data array.
        /// </summary>
        public int DataLength => _weightMatrixRowCount * _weightMatrixColCount + _biasLength;
        
        /// <summary>
        /// Gets the size of the data array in bytes.
        /// </summary>
        public int DataSizeInBytes => DataLength * sizeof(double);
    }
}
