using TSLib.Utility.Patterns.Scene.Contexts;
using UnityEngine;

namespace TSLib.Utility.Management.Component.Capabilities
{
    public abstract class ComponentBase : MonoBehaviour, IComponent
    {
        public virtual void Initialize() { }
        public virtual void Bind(SceneCtx sceneCtx, AppCtx appCtx) { }
        public virtual void Configure() { }
        public virtual void Activate() { }
        public virtual void Deconfigure() { }
        public virtual void Deactivate() { }
    }
}
