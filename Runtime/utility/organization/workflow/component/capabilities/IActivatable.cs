using Cysharp.Threading.Tasks;
using System.Threading;

public interface IActivatable
{
    /// <summary>
    /// Activates the element.
    /// </summary>
    public UniTask ActivateAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Deactivates the element.
    /// </summary>
    public UniTask DeactivateAsync(CancellationToken cancellationToken);
}