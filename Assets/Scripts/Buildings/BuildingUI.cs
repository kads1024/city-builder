using UnityEngine;

/// <summary>
/// Responsible for handling the Interface for placing buildings
/// </summary>
public class BuildingUI : MonoBehaviour
{
    // Input and Controller that will drive the Building Placement
    [SerializeField] private PlayerInput _input;
    [SerializeField] private BuildingController _buildingController;

    private bool _isPlacing = false;
    private bool _isValidLocation = true;

    // Building preview
    private Mesh _buildingPreviewMesh;
    [SerializeField] private Material _buildingPreviewMat;
    [SerializeField] private Material _buildingInvalidPreviewMat;

    private void Update()
    {
        if (_isPlacing)
        {
            // Calculate Position
            Vector3 position = Utility.MouseToTerrainPosition();

            // Get All Overlapping objects within the Sphere
            Collider[] colliders = Physics.OverlapSphere(position, _buildingController.GetPrefab(BuildingType.Hotel).GetRadius());
            // Check if there is objects colliding with the sphere other than the ground
            _isValidLocation = colliders.Length <= 1;  

            // Replace preview Material Depending on whether it is valid to place or not
            Material buildingPlacementMaterial = _isValidLocation ? _buildingPreviewMat : _buildingInvalidPreviewMat;
            Graphics.DrawMesh(_buildingPreviewMesh, position, Quaternion.identity, buildingPlacementMaterial, 0);
            
            if (_input.OnSelectionClick())
            {
                if(_isValidLocation)   
                    _buildingController.SpawnBuilding(BuildingType.Hotel, position);
                     
                _isPlacing = false;
            }
            else if(_input.OnInteractionClick())
            {
                _isPlacing = false;
            }
        }
    }

    /// <summary>
    /// Select the building to be placed
    /// </summary>
    /// <param name="p_building">The building to be placed</param>
    public void SelectBuilding(BuildingType p_building)
    {
        _isPlacing = true;
        _buildingPreviewMesh = _buildingController.GetPrefab(p_building).GetComponentInChildren<MeshFilter>().sharedMesh;
    }
}
