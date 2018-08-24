using System;
using System.Collections.Generic;

namespace DigitRecognizer.Presentation.Infrastructure
{
    public class DependencyResolver
    {
        private readonly Dictionary<Type, Type> _dependencyDictionary;

        private static DependencyResolver _instance;

        public DependencyResolver()
        {
            _dependencyDictionary = new Dictionary<Type, Type>();
        }
        
        /// <summary>
        /// The object used for locking. Required for thread safety.
        /// </summary>
        private static readonly object Lock = new object();

        /// <summary>
        /// Gets the singleton instance of the <see cref="DependencyResolver"/> class.
        /// </summary>
        public static DependencyResolver Instance
        {
            get
            {
                lock (Lock)
                {
                    return _instance ?? (_instance = new DependencyResolver());
                }
            }
        }
        
        public static void Register<TContract, TImplementation>()
        {
            Register(typeof(TContract), typeof(TImplementation));
        }

        public static void Register(Type contract, Type implementation)
        {
            Instance._dependencyDictionary.Add(contract, implementation);
        }

        public static T Resolve<T>()
        {
            return (T)Resolve(typeof(T));
        }

        public static object Resolve(Type contract)
        {
            return Activator.CreateInstance(Instance._dependencyDictionary[contract]);
        }
    }
}
