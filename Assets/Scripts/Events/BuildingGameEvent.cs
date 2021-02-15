using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Building Game Event is a scriptable object that serves as a channel to raise events with a BuildingType parameter in a more modular manner 
/// </summary>
[CreateAssetMenu(menuName = "Events/Building Game Event")]
public class BuildingGameEvent : ScriptableObject
{
    private List<BuildingGameEventListener> _listeners = new List<BuildingGameEventListener>();

    /// <summary>
    /// Invoke All Listeners registered to this event
    /// </summary>
    /// <param name="p_building">The building that will be passed through</param>
    public void Raise(int p_buildingIndex)
    {
        for (int i = _listeners.Count - 1; i >= 0; i--)
        {
            _listeners[i].OnEventRaised((BuildingType)p_buildingIndex);
        }
    }

    /// <summary>
    /// Add a certain listener for the event 
    /// </summary>
    /// <param name="p_listener">The Listener that will be added</param>
    public void RegisterListener(BuildingGameEventListener p_listener)
    {
        _listeners.Add(p_listener);
    }

    /// <summary>
    /// Remove a certain listener for the event 
    /// </summary>
    /// <param name="p_listener">The Listener that will be removed</param>
    public void UnregisterListener(BuildingGameEventListener p_listener)
    {
        _listeners.Remove(p_listener);
    }
}
