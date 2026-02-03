using System;
using TSLib.Utility.Patterns.Scene.Contexts;
using UnityEngine;

namespace TSLib.Utility.Management.Managers
{
    public abstract class UtilityControllerBase : UtilityComponentBase, IUtilityRegistrable
    {
        [SerializeField] protected UtilityComponentBase[] Utilities;

        public override void Initialize()
        {
            if (Utilities == null)
                throw new ArgumentNullException(nameof(Utilities));

            for (int i = 0; i < Utilities.Length; i++)
            {
                var utility = Utilities[i];
                if (utility == null) continue;
                utility.Initialize();
            }
        }

        public virtual void Register<T>(AppCtx appCtx) where T : UtilityContainerBase, new()
        {
            var container = new T();
            for (int i = 0; i < Utilities.Length; i++)
            {
                var utility = Utilities[i];
                if (utility == null) throw new ArgumentNullException(nameof(utility));

                container.Register(utility);
            }
            appCtx.UtilityCtx.Register(container);
        }

        public override void Configure()
        {
            if (Utilities == null)
                throw new ArgumentNullException(nameof(Utilities));

            for (int i = 0; i < Utilities.Length; i++)
            {
                var utility = Utilities[i];
                if (utility == null) continue;
                utility.Configure();
            }
        }

        public override void Deconfigure()
        {
            for (int i = 0; i < Utilities.Length; i++)
            {
                var utility = Utilities[i];
                if (utility == null) continue;

                utility.Deconfigure();
                Utilities[i] = null;
            }

            Utilities = null;
        }

        protected virtual void OnEnable()
        {
            if (Utilities == null)
                throw new ArgumentNullException(nameof(Utilities));

            for (int i = 0; i < Utilities.Length; i++)
            {
                var utility = Utilities[i];
                if (utility == null) continue;
                utility.Activate();
            }
        }

        protected virtual void OnDisable()
        {
            if (Utilities == null)
                throw new ArgumentNullException(nameof(Utilities));

            for (int i = 0; i < Utilities.Length; i++)
            {
                var utility = Utilities[i];
                if (utility == null) continue;
                utility.Deactivate();
            }
        }
    }
}
