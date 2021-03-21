using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

/// <summary>
/// Container forbuildings needed for the scene
/// </summary>
[CreateAssetMenu(menuName = "RTS/Building Container")]
public class BuildingContainer : SerializedScriptableObject
{
    /// <summary>
    /// List of all buildings available for building
    /// </summary>
    public Dictionary<BuildingType, Building> Buildings = new Dictionary<BuildingType, Building>();
}

[System.Serializable]
public enum BuildingType
{
    Hotel,
    WoodStorage,
    StoneStorage,
    FoodStorage,
}