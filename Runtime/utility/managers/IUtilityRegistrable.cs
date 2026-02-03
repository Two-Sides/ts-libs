using TSLib.Utility.Patterns.Scene.Contexts;

namespace TSLib.Utility.Management.Managers
{
    public interface IUtilityRegistrable
    {
        public void Register<T>(AppCtx appCtx) where T : UtilityContainerBase, new();
    }
}
