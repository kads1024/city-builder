using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Container for selected objects in the scene
/// </summary>
[CreateAssetMenu(menuName = "RTS/Selectable Objects Container")]
public class SelectableContainer : ScriptableObject
{
    /// <summary>
    /// List of all items that are selected
    /// </summary>
    public Dictionary<int, GameObject> Selectables = new Dictionary<int, GameObject>();

    /// <summary>
    /// Add a certain item to the dictionary
    /// </summary>
    /// <param name="p_objectToBeAdded">Item to be Added</param>
    public void Add(GameObject p_objectToBeAdded)
    {
        int id = p_objectToBeAdded.GetInstanceID();

        if (!(Selectables.ContainsKey(id)))
        {
            Selectables.Add(id, p_objectToBeAdded);
            p_objectToBeAdded.AddComponent<Selectable>();
        }
    }

    /// <summary>
    /// Remove the object in the selected list when it is deselected
    /// </summary>
    /// <param name="p_id">ID of the object to be removed</param>
    public void Deselect(int p_id)
    {
        Destroy(Selectables[p_id].GetComponent<Selectable>());
        Selectables.Remove(p_id);
    }

    /// <summary>
    /// Remove everything from the dictionary
    /// </summary>
    public void DeselectAll()
    {
        foreach (KeyValuePair<int, GameObject> pair in Selectables)
        {
            if (pair.Value != null)
            {
                Destroy(Selectables[pair.Key].GetComponent<Selectable>());
            }
        }
        Selectables.Clear();
    }
}
