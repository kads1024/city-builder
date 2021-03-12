using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceGenerator : MonoBehaviour
{
    [SerializeField] private string _seed = "";
    [SerializeField] private float _resourceRadius = 1f;
    [SerializeField] private Vector2 _regionSize = Vector2.one;
    [SerializeField] private Vector2 _regionoffset = Vector2.one;
    [SerializeField] private int _rejectionCount = 30;
    [SerializeField] private float _displayRadius = 1f;

    private List<Vector2> _points;

    private void Start()
    {
        PoissonDiscSampling.Init(_seed);
        _points = PoissonDiscSampling.GeneratePoints(_resourceRadius, _regionSize, _rejectionCount);
    }

    private void OnDrawGizmos()
    {
        Vector3 terrainRegion = new Vector3(_regionSize.x, 0f, _regionSize.y);
        Vector3 terrainRegionOffset = new Vector3(_regionoffset.x, 0f, _regionoffset.y);
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube((terrainRegion / 2f) + terrainRegionOffset, terrainRegion);
        if (_points != null)
        {
            foreach (Vector2 point in _points)
            {
                Vector3 terrainPoints = new Vector3(point.x, 1f, point.y) + terrainRegionOffset;
                Gizmos.DrawSphere(terrainPoints, _displayRadius);
            }
        }
    }
}
