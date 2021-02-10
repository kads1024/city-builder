using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Keeps Track of a set of objects in the scene during runtime
/// </summary>
/// <typeparam name="T">The type of object to be tracked</typeparam>
public class RuntimeSet<T> : ScriptableObject
{
    // List of all the objects in the scene
    public List<T> Items = new List<T>();

    /// <summary>
    /// Adds the item to the list
    /// </summary>
    /// <param name="p_item">Item to be added</param>
    public void Add(T p_item)
    {
        if (!Items.Contains(p_item)) Items.Add(p_item);
    }

    /// <summary>
    /// Removes the item to the list
    /// </summary>
    /// <param name="p_item">Item to be removed</param>
    public void Remove(T p_item)
    {
        if (Items.Contains(p_item)) Items.Remove(p_item);
    }
}
