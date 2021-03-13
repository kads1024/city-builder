using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceGenerator : MonoBehaviour
{
    [SerializeField] private string _seed = "";
    [SerializeField] private float _resourceRadius = 1f;
    [SerializeField] private Vector2 _regionSize = Vector2.one;
    [SerializeField] private Vector3 _regionoffset = Vector3.one;
    [SerializeField] private int _rejectionCount = 30;
    [SerializeField] private float _displayRadius = 1f;

    private List<Vector2> _points;

    private List<Vector3> _resourcePositions;

    [SerializeField] private List<Transform> _objectsToBeGenerated;
    [SerializeField] private LayerMask _ground;

    private void Start()
    {
        PoissonDiscSampling.Init(_seed);
        _points = PoissonDiscSampling.GeneratePoints(_resourceRadius, _regionSize, _rejectionCount);

        _resourcePositions = new List<Vector3>();

        if (_points != null)
        {
            foreach (Vector2 point in _points)
            {
                _resourcePositions.Add(new Vector3(point.x, 1f, point.y) + _regionoffset);
            }
            _points.Clear();
        }

        if (_resourcePositions != null)
        {
            foreach (Vector3 point in _resourcePositions)
            {
                Ray ray = new Ray(point, Vector3.down);
                RaycastHit hit;

                if(Physics.Raycast(ray, out hit, 100, _ground))
                {
                    float randomChance = Random.value;
                    float percentage = 1f / _objectsToBeGenerated.Count;
                    float currentPercentage = 0.0f;
                    for(int i = 0; i < _objectsToBeGenerated.Count; i++)
                    {
                        if(currentPercentage >= randomChance)
                        {
                            Instantiate(_objectsToBeGenerated[i], hit.point, Quaternion.identity);
                            break;
                        }
                        currentPercentage += percentage;
                    }
                }
            }
        }
    }

    //private void OnDrawGizmos()
    //{
    //    Vector3 terrainRegion = new Vector3(_regionSize.x, 0f, _regionSize.y);
        
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireCube((terrainRegion / 2f) + _regionoffset, terrainRegion);
    //    if (_resourcePositions != null)
    //    {
    //        foreach (Vector3 point in _resourcePositions)
    //        {
    //            //Gizmos.DrawSphere(point, _displayRadius);
    //        }
    //    }
    //}
}
