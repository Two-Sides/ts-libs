using System;
using System.Collections.Generic;

namespace TSLib.Utility.Management.Managers
{
    public abstract class UtilityContainerBase
    {
        private readonly Dictionary<Type, UtilityBase> _utilities = new();

        public void Register(UtilityBase utility)
        {
            if (utility == null)
                throw new ArgumentNullException(nameof(utility));

            var type = utility.GetType();

            if (_utilities.ContainsKey(type))
            {
                throw new InvalidOperationException(
                    $"An utility of type '{type.FullName}' is already registered. " +
                    "Duplicate utility registration is not allowed."
                );
            }

            _utilities[type] = utility;
        }

        public void Unregister<T>()
        {
            _utilities.Remove(typeof(T));
        }

        public T Get<T>() where T : UtilityBase
        {
            if (_utilities.TryGetValue(typeof(T), out var utility))
                return (T)utility;

            return default;
        }

        public void Clear()
        {
            _utilities.Clear();
        }
    }
}
