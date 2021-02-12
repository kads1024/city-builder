using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Movement Component - Must require A Navmesh Agent to properly work
/// </summary>
[RequireComponent(typeof(NavMeshAgent))]
public class Movement : MonoBehaviour
{
    // Components neededs
    [SerializeField] private Animator _animator;
    private NavMeshAgent _agent;
   
    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if(_animator) _animator.SetFloat("Speed", Mathf.Clamp(_agent.velocity.magnitude, 0, 1));

    }

    /// <summary>
    /// Sets the destination of the object to move to
    /// </summary>
    /// <param name="destination">Destination to move to</param>
    public void SetDestination(Vector3 p_destination)
    {
        _agent.destination = p_destination;
    }

    /// <summary>
    /// Check if the agent finishes processing the path
    /// </summary>
    /// <returns>Whether or not the Path has finished processing</returns>
    public WaitUntil WaitForNavMeshToLoad() 
    {
        return new WaitUntil(() => !_agent.pathPending && _agent.remainingDistance <= _agent.stoppingDistance);
    }
}
