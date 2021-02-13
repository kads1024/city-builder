using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Responsible for handling the Interface for placing buildings
/// </summary>
public class BuildingUI : MonoBehaviour
{
    [SerializeField] private PlayerInput _input;
    [SerializeField] private BuildingController _buildingController;

    private bool _isPlacing = false;

    private Mesh _buildingPreviewMesh;
    [SerializeField] private Material _buildingPreviewMat;

    private void Update()
    {
        if (_isPlacing)
        {
            Vector3 position = Utility.MouseToTerrainPosition();
            Graphics.DrawMesh(_buildingPreviewMesh, position, Quaternion.identity, _buildingPreviewMat, 0);

            if (_input.OnSelectionClick())
            {
                _buildingController.SpawnBuilding(BuildingType.Hotel, position);
                _isPlacing = false;
            }
        }
    }

    public void SelectBuilding()
    {
        _isPlacing = true;
        _buildingPreviewMesh = _buildingController.GetPrefab(BuildingType.Hotel).GetComponentInChildren<MeshFilter>().sharedMesh;
    }
}
