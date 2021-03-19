using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Listener to the VoidEvent channel
/// </summary>
public class VoidEventListener : MonoBehaviour
{
    // The VoidEvent channel where this listener will register to
    [SerializeField] private VoidEvent _event;

    // The Response to when the GameEvent channel will be raised
    [SerializeField] private UnityEvent _response;

    // When the object is enabled, you must immediately add it to the channel
    private void OnEnable()
    {
        _event.RegisterListener(this);
    }

    // When the object is disabled, remove this from the channel to avoid unnecessary event raising
    private void OnDisable()
    {
        _event.UnregisterListener(this);
    }

    /// <summary>
    /// The Function that will be called when the GameEvent channel will be raised
    /// </summary>
    public void OnEventRaised()
    {
        _response.Invoke();
    }
}
