using UnityEngine;

/// <summary>
/// A single item that will be added to the GameObjectRuntimeSet
/// </summary>
public class RSItemGameObject : MonoBehaviour
{
    // The Set where we will add this object tos
    [SerializeField] private GameObjectRuntimeSet _gameObjectSet;

    // Add whenever avaiable
    private void OnEnable()
    {
        _gameObjectSet.Items.Add(gameObject);
    }

    // Remove when disabled to avoid unecessary tracking
    private void OnDisable()
    {
        _gameObjectSet.Items.Remove(gameObject);
    }
}
