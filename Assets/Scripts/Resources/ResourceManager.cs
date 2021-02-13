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

    [ReadOnly, SerializeField] private Dictionary<ResourceType, int> _currentResources;
    public Dictionary<ResourceType, int> CurrentResources
    {
        get { return _currentResources; }
        set { _currentResources = value; }
    }

    private void OnEnable()
    {
        _currentResources = new Dictionary<ResourceType, int>(_resources);
    }

    // Add a certain resource
    public void AddResource(Cost p_cost)
    {
        _currentResources[p_cost.Resource] += p_cost.Amount;
    }

    // Deduct a certain Resource
    public void DeductResource(Cost p_cost)
    {
        _currentResources[p_cost.Resource] += p_cost.Amount;
    }

    // Check if the player has enough of a specific resource
    public bool HasEnoughResource(ResourceType p_resource, int p_amount)
    {
        return _currentResources[p_resource] >= p_amount;
    }
}

public enum ResourceType
{
    Wood,
    Stone
}