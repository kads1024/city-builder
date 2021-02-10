using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Game Event is a scriptable object that serves as a channel to raise events in a more modular manner
/// </summary>
[CreateAssetMenu(menuName = "Events/Void Game Event")]
public class GameEvent : ScriptableObject
{
    private List<GameEventListener> _listeners = new List<GameEventListener>();

    /// <summary>
    /// Invoke All Listeners registered to this event
    /// </summary>
    public void Raise()
    {
        for (int i = _listeners.Count - 1; i >= 0; i--) {
            _listeners[i].OnEventRaised();
        }
    }

    /// <summary>
    /// Add a certain listener for the event 
    /// </summary>
    /// <param name="p_listener">The Listener that will be added</param>
    public void RegisterListener(GameEventListener p_listener)
    {
        _listeners.Add(p_listener);
    }

    /// <summary>
    /// Remove a certain listener for the event 
    /// </summary>
    /// <param name="p_listener">The Listener that will be removed</param>
    public void UnregisterListener(GameEventListener p_listener)
    {
        _listeners.Remove(p_listener);
    }
}
