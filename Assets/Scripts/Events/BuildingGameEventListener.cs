using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;

/// <summary>
/// Listener to the BuildingGameEvent channel
/// </summary>
public class BuildingGameEventListener : SerializedMonoBehaviour
{
    // The BuildingGameEvent channel where this listener will register to
    [SerializeField] private BuildingGameEvent _event;

    // The Response to when the BuildingGameEvent channel will be raised
    [SerializeField] private BuildingEventResponse _response;

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
    /// <param name="p_building">The building that will be passed through</param>
    public void OnEventRaised(BuildingType p_building)
    {
        _response.Invoke(p_building);
    }
}

/// <summary>
/// Response to the BuildingGameEventListener
/// </summary>
[System.Serializable]
public class BuildingEventResponse : UnityEvent<BuildingType> { }