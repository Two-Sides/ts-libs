using System;
using System.Collections.Generic;

namespace TSLib.Utility.Patterns.Scene.Contexts
{
    public class ContainerRegistry<T>
    {
        private readonly Dictionary<Type, T> _containers = new();

        public bool Active { get; private set; } = true;

        public void SetActive(bool active) => Active = active;

        public void Register(T container)
        {
            if (container == null)
                throw new ArgumentNullException(nameof(container));

            var type = container.GetType();

            if (_containers.ContainsKey(type))
            {
                throw new InvalidOperationException(
                    $"A container of type '{type.FullName}' is already registered. " +
                    "Duplicate container registration is not allowed."
                );
            }

            _containers[type] = container;
        }

        public void Unregister<Type>()
        {
            _containers.Remove(typeof(Type));
        }

        public Type Get<Type>() where Type : T
        {
            if (!Active) throw new InvalidOperationException(
                "(disabled) The container getter is currently disabled. Call SetActive(true) before attempting to use it.");

            if (_containers.TryGetValue(typeof(Type), out var service))
                return (Type)service;

            return default;
        }

        public void Clear()
        {
            _containers.Clear();
        }
    }
}
