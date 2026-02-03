
using UnityEngine;

namespace TSLib.Utility.Management.Managers
{
    public abstract class UtilityComponentBase : MonoBehaviour, IUtility
    {
        public virtual void Initialize() { }
        public virtual void Configure() { }
        public virtual void Activate() { }
        public virtual void Deconfigure() { }
        public virtual void Deactivate() { }
    }
}
