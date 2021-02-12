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

    // Add a certain resource
    public void AddResource(ResourceType p_resource, int p_amount)
    {
        _resources[p_resource] += p_amount;
    }

    // Deduct a certain Resource
    public void DeductResource(ResourceType p_resource, int p_amount)
    {
        _resources[p_resource] += p_amount;
    }

    // Check if the player has enough of a specific resource
    public bool HasEnoughResource(ResourceType p_resource, int p_amount)
    {
        return _resources[p_resource] >= p_amount;
    }
}

public enum ResourceType
{
    Wood,
    Stone
}