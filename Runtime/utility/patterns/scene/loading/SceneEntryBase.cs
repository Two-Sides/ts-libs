using System.Threading;
using Cysharp.Threading.Tasks;
using TSLib.Utility.Patterns.EventChannels.NonPrimitive;
using TSLib.Utility.Patterns.Scene.Contexts;
using UnityEngine;

namespace TSLib.Utility.Patterns.Scene.Loading
{
    public abstract class SceneEntryBase : MonoBehaviour
    {
        [SerializeField] private SceneChannelSo onSceneLoaded;

        private AppCtx _appCtx;
        private SceneCtx _sceneCtx;

        public async UniTask LoadAsync(AppCtx appCtx, CancellationToken ct)
        {
            _appCtx = appCtx;
            _sceneCtx = new SceneCtx();

            _sceneCtx.SetActive(false);

            await PreconfigureSceneAsync(ct);
            await InstantiateAsync(ct);
            await InitializeAsync(ct);
            await RegisterAsync(_sceneCtx, _appCtx, ct);

            _sceneCtx.SetActive(true);

            await BindingAsync(_sceneCtx, _appCtx, ct);
            await ConfigureAsync(ct);
            await ExecuteCustomOperationsAsync(ct);

            if (onSceneLoaded == null) return;
            onSceneLoaded.TriggerEvent(gameObject.scene);
        }

        protected virtual UniTask PreconfigureSceneAsync(CancellationToken ct) => UniTask.CompletedTask;
        protected abstract UniTask InstantiateAsync(CancellationToken ct);
        protected abstract UniTask InitializeAsync(CancellationToken ct);
        protected abstract UniTask RegisterAsync(SceneCtx sceneCtx, AppCtx appCtx, CancellationToken ct);
        protected abstract UniTask BindingAsync(SceneCtx sceneCtx, AppCtx appCtx, CancellationToken ct);
        protected abstract UniTask ConfigureAsync(CancellationToken ct);
        protected virtual UniTask ExecuteCustomOperationsAsync(CancellationToken ct) => UniTask.CompletedTask;
        protected virtual UniTask LoadSceneAdditiveAsync(CancellationToken ct) => UniTask.CompletedTask;
        protected virtual UniTask UnLoadSceneAdditiveAsync(CancellationToken ct) => UniTask.CompletedTask;
    }
}

