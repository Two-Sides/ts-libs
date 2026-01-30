using TSLib.Utility.Patterns.Scene.Contexts;

namespace TSLib.Utility.Management.Component.Capabilities
{
    public interface IBinding
    {
        public void Bind(SceneCtx sceneCtx, AppCtx appCtx);
    }
}
