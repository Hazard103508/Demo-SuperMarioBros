using Mario.Application.Interfaces;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Mario.Application.Services
{
    public class ServiceLocator : IDisposable
    {
        #region Objects
        private readonly Dictionary<Type, IGameService> services = new Dictionary<Type, IGameService>();
        #endregion

        #region Properties
        public static ServiceLocator Current { get; private set; }
        #endregion

        #region Constructor
        private ServiceLocator() { }
        #endregion

        #region Public Methods
        /// <summary>
        /// Initalizes the service locator with a new instance.
        /// </summary>
        public static void Initiailze()
        {
            Current = new ServiceLocator();
        }

        /// <summary>
        /// Gets the service instance of the given type.
        /// </summary>
        /// <typeparam name="T">The type of the service to lookup.</typeparam>
        /// <returns>The service instance.</returns>
        public T Get<T>() where T : IGameService
        {
            Type key = typeof(T);
            if (!services.ContainsKey(key))
            {
                Debug.LogError($"{key.Name} not registered with {GetType().Name}");
                throw new InvalidOperationException();
            }

            return (T)services[key];
        }

        /// <summary>
        /// Registers the service with the current service locator.
        /// </summary>
        /// <typeparam name="T">Service type.</typeparam>
        /// <param name="service">Service instance.</param>
        public void Register<T>(T service) where T : IGameService
        {
            Type key = typeof(T);
            if (services.ContainsKey(key))
            {
                Debug.LogError($"Attempted to register service of type {key.Name} which is already registered with the {GetType().Name}.");
                return;
            }

            services.Add(key, service);
        }

        public void Initalize()
        {
            foreach (var service in services)
                service.Value.Initalize();
        }

        public void Dispose()
        {
            foreach (var service in services)
                service.Value.Dispose();

            services.Clear();
        }
        #endregion
    }
}