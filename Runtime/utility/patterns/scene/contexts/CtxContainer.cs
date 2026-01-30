using System;
using System.Collections.Generic;

namespace TSLib.Utility.Patterns.Scene.Contexts
{
    public class CtxContainer<T>
    {
        private readonly Dictionary<Type, T> _registry = new();

        public bool Active { get; private set; } = true;

        public void SetActive(bool active) => Active = active;

        public void Register(T ctx)
        {
            if (ctx == null)
                throw new ArgumentNullException(nameof(ctx));

            var type = ctx.GetType();

            if (_registry.ContainsKey(type))
            {
                throw new InvalidOperationException(
                    $"A container of type '{type.FullName}' is already registered. " +
                    "Duplicate container registration is not allowed."
                );
            }

            _registry[type] = ctx;
        }

        public void Unregister<Type>()
        {
            _registry.Remove(typeof(Type));
        }

        public Type Get<Type>() where Type : T
        {
            if (!Active) throw new InvalidOperationException(
                "(disabled) The context is currently disabled. Call SetActive(true) before attempting to use it.");

            if (_registry.TryGetValue(typeof(Type), out var service))
                return (Type)service;

            return default;
        }

        public void Clear()
        {
            _registry.Clear();
        }
    }
}
