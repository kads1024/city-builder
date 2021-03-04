using UnityEngine;

/// <summary>
/// Serves to store a specific type of resource
/// </summary>
[RequireComponent(typeof(Building))]
public class Storage : MonoBehaviour
{
    // Building Component of the object
    private Building _building;

    // Resource Manager that will be used to increase storage
    [SerializeField] private ResourceManager _resourceManager;

    // Amount of Added Storage to be used
    [SerializeField] private Cost _storageAmount;

    private void Awake()
    {
        _building = GetComponent<Building>();
    }

    private void OnEnable()
    {
        _building.RegisterOnFinishListener(AddStorage);
    }

    private void OnDisable()
    {
        _building.UnregisterOnFinishListener(AddStorage);
    }

    /// <summary>
    /// Adds extra storage for a specif type of resource
    /// </summary>
    private void AddStorage()
    {
        _resourceManager.IncreaseResourceLimit(_storageAmount.Resource, _storageAmount.Amount);
    }
}
