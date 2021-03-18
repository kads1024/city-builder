using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The Start Area of the map
/// </summary>
public class StartingArea : MonoBehaviour
{
    // The Radius of the starting area
    [SerializeField] private float _radius;
    public float Radius => _radius;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }
}
