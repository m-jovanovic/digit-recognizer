using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DigitRecognizer.MachineLearning.Functions;

namespace DigitRecognizer.MachineLearning.Factories
{
    /// <summary>
    /// Factory for getting instances of <see cref="IFunction"/> by providing the name of the function.
    /// </summary>
    public class FunctionFactory
    {
        /// <summary>
        /// The singleton instance.
        /// </summary>
        private static FunctionFactory _instance;

        /// <summary>
        /// The object used for locking. Required for thread safety.
        /// </summary>
        private static readonly object Lock = new object();

        /// <summary>
        /// Gets the singleton instance of the <see cref="FunctionFactory"/> class.
        /// </summary>
        public static FunctionFactory Instance
        {
            get
            {
                lock (Lock)
                {
                    return _instance ?? (_instance = new FunctionFactory());
                }
            }
        }

        /// <summary>
        /// The dictionary that contains mapping from function names to the matching types.
        /// </summary>
        private readonly Dictionary<string, Type> _functionsDictionary;

        /// <summary>
        /// Initializes a new instance of the <see cref="FunctionFactory"/> class.
        /// </summary>
        private FunctionFactory()
        {
            _functionsDictionary = new Dictionary<string, Type>();
            
            FillFunctionsDictionary();
        }

        /// <summary>
        /// Fills the internal dictionary with data.
        /// </summary>
        private void FillFunctionsDictionary()
        {
            Type baseType = typeof(IFunction);

            // Get all the types in the executing assembly that implement our interface, and are classes. 
            IEnumerable<Type> types = Assembly
                .GetExecutingAssembly()
                .GetTypes()
                .Where(t => baseType.IsAssignableFrom(t) && t.IsClass);

            // We get the value of the Name property that is on the IFunction interface and fill the dictionary with the name and type.
            foreach (Type type in types)
            {
                PropertyInfo prop = type.GetProperty("Name");

                if (prop == null)
                {
                    throw new ArgumentNullException(nameof(prop), $"Property Name was not found on type {type.Name}");
                }

                object obj = Activator.CreateInstance(type);

                _functionsDictionary.Add(prop.GetValue(obj).ToString(), type);
            }
        }

        /// <summary>
        /// Gets the <see cref="IFunction"/> instance for the specified name.
        /// </summary>
        /// <remarks>
        /// If no matching type is found an exception is thrown.
        /// This indicates something is wrong in the system.
        /// </remarks>
        /// <param name="name">The name of the function.</param>
        /// <returns>The instance of the function, if found.</returns>
        public IFunction GetFunction(string name)
        {
            Type type = _functionsDictionary[name];

            object result = Activator.CreateInstance(type);

            return (IFunction) result;
        }
    }
}
