using Cysharp.Threading.Tasks;
using UnityEngine;
using TwoSides.Utility.EventChannels.NonPrimitive;
using System;
using System.Threading;

namespace TwoSides.Utility.Organization.Workflow.SingleEntryPoint
{
    /// <summary>
    /// Base class that acts as a single entry point for scene initialization logic.
    /// 
    /// Implementations should perform all required asynchronous initialization work
    /// inside <see cref="Execute"/>, which is automatically invoked when the scene starts.
    /// 
    /// Once initialization completes successfully, this component notifies listeners
    /// by raising a scene-initialized event and then destroys its own GameObject.
    /// 
    /// Cancellation is automatically handled when the GameObject is destroyed,
    /// preventing notifications from being sent in invalid or unloaded scenes.
    /// </summary>
    public abstract class SceneInitBase : MonoBehaviour
    {
        /// <summary>
        /// Event channel triggered when the scene has finished its initialization.
        /// </summary>
        [SerializeField] private SceneChannelSo onSceneInitialized;

        /// <summary>
        /// Executes the scene-specific asynchronous initialization logic.
        /// Implementations should observe the provided cancellation token and
        /// stop execution when cancellation is requested.
        /// </summary>
        /// <param name="cToken">
        /// Cancellation token that is triggered when this GameObject is destroyed.
        /// </param>
        protected abstract UniTask Execute(CancellationToken cToken);

        private async void Start()
        {
            var ct = this.GetCancellationTokenOnDestroy();

            try
            {
                await Execute(ct);

                if (ct.IsCancellationRequested)
                    return;

                var scene = gameObject.scene;
                if (!scene.IsValid() || !scene.isLoaded) return;

                onSceneInitialized.TriggerEvent(scene);
            }
            catch (OperationCanceledException) when (ct.IsCancellationRequested)
            {
                // Expected cancellation when the GameObject is destroyed.
            }
            catch (Exception ex)
            {
                Debug.LogException(ex, this);
            }
            finally
            {
                // Self-destruct after initialization completes or fails.
                if (this) Destroy(gameObject);
            }
        }
    }
}



