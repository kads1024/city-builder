using UnityEngine;

/// <summary>
/// Used for controlling building placements
/// </summary>
[CreateAssetMenu(menuName = "Managers/Building Controller")]
public class BuildingController : ScriptableObject
{
    // List of buildings to be used
    [SerializeField] private BuildingContainer _buildingContainer;

    // Currently owned resources
    [SerializeField] private ResourceManager _currentResources;

    // Current Task of the player
    //[SerializeField] private CoroutineVariable _playerTask;

    // List of all selected characters
    [SerializeField] private SelectableContainer _selectedCharacters;

    /// <summary>
    /// Gets the prefab of a certain building
    /// </summary>
    /// <param name="p_building">The building to get</param>
    /// <returns>The prefab of the said building</returns>
    public Building GetPrefab(BuildingType p_building)
    {
        return _buildingContainer.Buildings[p_building];
    }

    /// <summary>
    /// Used to build the building
    /// </summary>
    /// <param name="p_building">The building to be built</param>
    /// <param name="p_position">The position where to place the building</param>
    public void SpawnBuilding(BuildingType p_building, Vector3 p_position)
    {
        Building building = _buildingContainer.Buildings[p_building];

        // Check if have enough resources
        if (!_currentResources.HasEnoughResource(building.GetCost().Resource, building.GetCost().Amount))
            return;

        // Check if there are selected characters
        if (_selectedCharacters.Selectables.Count <= 0)
            return;

        // Create Building
        building = Instantiate(_buildingContainer.Buildings[p_building], p_position, Quaternion.identity);

        // Give builders build task
        foreach (GameObject character in _selectedCharacters.Selectables.Values)
        {
            Builder builder = character.GetComponent<Builder>();
            if (!builder.HasTask())
                builder.GiveJob(building);
        }

        // Subtract resources
        Cost cost = building.GetCost();
        _currentResources.DeductResource(cost);
    }
}
