﻿using System;
using System.Collections.Generic;

namespace BrickBreaker.Scenes
{
    public static class ServiceLocator
    {
        private static Dictionary<Type, object> _services = new Dictionary<Type, object>();

        public static void Register<T>(T service)
        {
            if (_services.ContainsKey(typeof(T)))
                throw new InvalidOperationException($"Service of type {typeof(T)} already registered");
            _services[typeof(T)] = service;
        }

        public static T Get<T>()
        {
            if (!_services.ContainsKey(typeof(T)))
                throw new InvalidOperationException($"Service of type {typeof(T)} not registered");
            return (T)_services[typeof(T)];
        }
    }
}