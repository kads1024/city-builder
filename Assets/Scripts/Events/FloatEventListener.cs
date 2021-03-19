using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Listener to the FloatEvent channel
/// </summary>
public class FloatEventListener : MonoBehaviour
{
    // The FloatEvent channel where this listener will register to
    [SerializeField] private FloatEvent _event;

    // The Response to when the GameEvent channel will be raised
    [SerializeField] private FloatUnityEventEvent _response;

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
    public void OnEventRaised(float p_value)
    {
        _response.Invoke(p_value);
    }
}

[System.Serializable]
public class FloatUnityEventEvent : UnityEvent<float> { }