using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

/// <summary>
/// Responsible for handling resources
/// </summary>
[CreateAssetMenu(menuName = "Managers/Resource Manager")]
public class ResourceManager : SerializedScriptableObject
{
    // List of resources owned by the player
    [SerializeField] private Dictionary<ResourceType, int> _resources;
    // List of resources Limit for the player
    [SerializeField] private Dictionary<ResourceType, int> _resourceLimit;

    [ReadOnly, SerializeField] private Dictionary<ResourceType, int> _currentResources;
    public Dictionary<ResourceType, int> CurrentResources
    {
        get { return _currentResources; }
        set { _currentResources = value; }
    }

    [ReadOnly, SerializeField] private Dictionary<ResourceType, int> _currentResourceLimit;
    public Dictionary<ResourceType, int> CurrentResourceLimit
    {
        get { return _currentResourceLimit; }
        set { _currentResourceLimit = value; }
    }

    private void OnEnable()
    {
        _currentResources = new Dictionary<ResourceType, int>(_resources);
        _currentResourceLimit = new Dictionary<ResourceType, int>(_resourceLimit);
    }

    // Add a certain resource
    public void AddResource(Cost p_cost)
    {
        _currentResources[p_cost.Resource] += p_cost.Amount;
    }

    // Deduct a certain Resource
    public void DeductResource(Cost p_cost)
    {
        _currentResources[p_cost.Resource] -= p_cost.Amount;
    }

    // Check if the player has enough of a specific resource
    public bool HasEnoughResource(ResourceType p_resource, int p_amount)
    {
        return _currentResources[p_resource] >= p_amount;
    }

    // Increase the limit of a resource
    public void IncreaseResourceLimit(ResourceType p_resource, int p_amount)
    {
        _currentResourceLimit[p_resource] += p_amount;
    }

    // Check if the player has enough storage for a resou   
    public bool HasEnoughStorage(ResourceType p_resource, int p_amount)
    {
        return _currentResources[p_resource] + p_amount <= _currentResourceLimit[p_resource];
    }
}

public enum ResourceType
{
    Wood,
    Stone,
    Bread, 
    Person
}