using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Float Event is a scriptable object event with a FloatType parameter that serves as a channel to raise events in a more modular manner
/// </summary>
[CreateAssetMenu(menuName = "Events/Float Event")]
public class FloatEvent : ScriptableObject
{
    private List<FloatEventListener> _listeners = new List<FloatEventListener>();

    /// <summary>
    /// Invoke All Listeners registered to this event
    /// </summary>
    public void Raise(float p_value)
    {
        for (int i = _listeners.Count - 1; i >= 0; i--)
        {
            _listeners[i].OnEventRaised(p_value);
        }
    }

    /// <summary>
    /// Add a certain listener for the event 
    /// </summary>
    /// <param name="p_listener">The Listener that will be added</param>
    public void RegisterListener(FloatEventListener p_listener)
    {
        _listeners.Add(p_listener);
    }

    /// <summary>
    /// Remove a certain listener for the event 
    /// </summary>
    /// <param name="p_listener">The Listener that will be removed</param>
    public void UnregisterListener(FloatEventListener p_listener)
    {
        _listeners.Remove(p_listener);
    }
}