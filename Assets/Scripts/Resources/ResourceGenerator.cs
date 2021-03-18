﻿using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Responsible for Generating the map of the game
/// </summary>
public class ResourceGenerator : MonoBehaviour
{
    // Seed to use in game
    [SerializeField] private string _seed = "";

    // Radius of each resource
    [SerializeField] private float _resourceRadius = 1f;

    // Size and offset of the map
    [SerializeField] private Vector2 _regionSize = Vector2.one;
    [SerializeField] private Vector3 _regionoffset = Vector3.one;

    // How many times should the generator try to relocate another point to place resource
    [SerializeField] private int _rejectionCount = 30;

    // List of points generated by the Poisson Disc Sampler
    private List<Vector2> _points;

    // List of all positions of the resources generated
    private List<Vector3> _resourcePositions;

    // Prefabs of the resources to be placed in the map
    [SerializeField] private List<Transform> _objectsToBeGenerated;

    // The Layer of the ground where we will spawn the Resources
    [SerializeField] private LayerMask _ground;

    private void Start()
    {
        // Initialize the Point Generator
        PoissonDiscSampling.Init(_seed);

        // Generate Points
        _points = PoissonDiscSampling.GeneratePoints(_resourceRadius, _regionSize, _rejectionCount);

        // Init Resource Positions
        _resourcePositions = new List<Vector3>();

        // Convert Generated Points To 3D Position
        if (_points != null)
        {
            foreach (Vector2 point in _points)
                _resourcePositions.Add(new Vector3(point.x, 1f, point.y) + _regionoffset);
           
            // Clear the list since we wont be needing it anymore
            _points.Clear();
        }

        // Generate the resources on the map
        if (_resourcePositions != null)
        {
            // Loop through Each Position
            foreach (Vector3 point in _resourcePositions)
            {
                // Cast A ray downwards to see if it hits a ground
                Ray ray = new Ray(point, Vector3.down);
                RaycastHit hit;

                // If it hits the ground, Generate a random object
                if(Physics.Raycast(ray, out hit, 100, _ground))
                {
                    float randomChance = Random.value;
                    float percentage = 1f / _objectsToBeGenerated.Count;
                    float currentPercentage = 0.0f;
                    for(int i = 0; i < _objectsToBeGenerated.Count; i++)
                    {
                        if(currentPercentage >= randomChance)
                        {
                            Instantiate(_objectsToBeGenerated[i], hit.point, Quaternion.identity, transform);
                            break;
                        }
                        currentPercentage += percentage;
                    }
                }
            }
        }
    }
}
