using System;
using System.Collections.Generic;

namespace Include
{
    public static class ServiceLocator
    {
        private static Dictionary<Type, object> _services = new Dictionary<Type, object>();

        public static void AddService<T>(T service)
        {
            if (_services.ContainsKey(typeof(T)))
                throw new Exception("Service already added");

            _services.Add(typeof(T), service);
        }

        public static void RemoveService<T>()
        {
            if (_services.ContainsKey(typeof(T)))
                _services.Remove(typeof(T));
        }

        public static T GetService<T>()
        {
            if (_services.ContainsKey(typeof(T)) == false)
                throw new Exception($"Service {typeof(T).Name} not found");

            return (T)_services[typeof(T)];
        }

        public static bool HasService<T>() => _services.ContainsKey(typeof(T)); //xD?? 
    }
}