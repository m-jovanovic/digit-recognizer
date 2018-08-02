using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DigitRecognizer.MachineLearning.Infrastructure.Factories
{
    /// <summary>
    /// Represents a factory for getting instances of <see cref="Type"/> based on a key.
    /// </summary>
    /// <typeparam name="TInstance">The instance type the abstract factory returns.</typeparam>
    /// <typeparam name="TKey">The key used in the type dictionary.</typeparam>
    public abstract class AbstractTypeFactory<TInstance, TKey>
    {
        private readonly Dictionary<TKey, Type> _typeDictionary;

        private readonly string _propName;
        private readonly Type _baseType;

        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractTypeFactory{TInstance,TKey}"/> class.
        /// </summary>
        /// <param name="baseType">The base <see cref="Type"/></param>
        /// <param name="propName">The key of the property of the <see cref="Type"/> <paramref name="baseType"/>.</param>
        protected AbstractTypeFactory(Type baseType, string propName)
        {
            _propName = propName;
            _baseType = baseType;

            _typeDictionary = new Dictionary<TKey, Type>();

            FillTypeDictionary();
        }

        /// <summary>
        /// Fills the internal dictionary with data.
        /// </summary>
        private void FillTypeDictionary()
        {
            // Get all the types in the executing assembly that implement our interface, and are classes. 
            IEnumerable<Type> types = Assembly
                .GetExecutingAssembly()
                .GetTypes()
                .Where(t => _baseType.IsAssignableFrom(t) && t.IsClass);

            // We get the value of the property that is on the interface and fill the dictionary with the property value and type.
            foreach (Type type in types)
            {
                PropertyInfo prop = type.GetProperty(_propName);

                if (prop == null)
                {
                    throw new ArgumentNullException(nameof(prop), $"Property {_propName} was not found on type {type.Name}");
                }

                object obj = Activator.CreateInstance(type);

                _typeDictionary.Add((TKey)prop.GetValue(obj), type);
            }
        }

        /// <summary>
        /// Gets the instance of  the <see cref="Type"/> for the specified key.
        /// </summary>
        /// <remarks>
        /// If no matching type is found an exception is thrown.
        /// This indicates something is wrong in the system.
        /// Generally, this should never be thrown.
        /// </remarks>
        /// <param name="key">The key of the dictionary.</param>
        /// <returns>The instance of the type, if found.</returns>
        public TInstance GetInstance(TKey key)
        {
            Type type = _typeDictionary[key];

            object result = Activator.CreateInstance(type);

            return (TInstance)result;
        }
    }
}
