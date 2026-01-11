using UnityEngine;
using UnityEngine.SceneManagement;

namespace TwoSides.Utility.EventChannels.NonPrimitive
{
    [CreateAssetMenu(
        fileName = "SceneChannelSo",
        menuName = "EventChannels/Actions/SceneChannelSo"
    )]
    public class SceneChannelSo : ActionChannelBaseSo<Scene> { }
}

