using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DigitRecognizer.MachineLearning.Functions;

namespace DigitRecognizer.MachineLearning.Infrastructure
{
    /// <summary>
    /// 
    /// </summary>
    public class FunctionFactory
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly Dictionary<string, Type> _functionsDictionary;

        /// <summary>
        /// 
        /// </summary>
        public FunctionFactory()
        {
            _functionsDictionary = new Dictionary<string, Type>();
            
            FillFunctionsDictionary();
        }

        /// <summary>
        /// 
        /// </summary>
        private void FillFunctionsDictionary()
        {
            Type baseType = typeof(IFunction);
            IEnumerable<Type> types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(t => baseType.IsAssignableFrom(t) && t.IsClass);

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
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IFunction GetFunction(string name)
        {
            Type type = _functionsDictionary[name];

            object result = Activator.CreateInstance(type);

            return (IFunction) result;
        }
    }
}
