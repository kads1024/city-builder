using System.Collections;
using UnityEngine;

/// <summary>
/// Component that indicates that this unit can build buildings
/// </summary>
[RequireComponent(typeof(Movement), typeof(TaskManager))]
public class Builder : MonoBehaviour
{
    // The building that the player is currently working with
    private Building _currentBuilding;

    // Required Components
    private Movement _movement;
    private TaskManager _playerTask;

    // Events to be Invokes
    [SerializeField] private GameEvent _buildEventToBeUsed;
    [SerializeField] private AnimationEventListener _animationEvent;

    // Animator to be used 
    [SerializeField] private Animator _animator;

    private void Awake()
    {
        _movement = GetComponent<Movement>();
        _playerTask = GetComponent<TaskManager>();
    }

    /// <summary>
    /// Assigns a current player a building to build
    /// </summary>
    /// <param name="p_job">The building that the player will build</param>
    public void GiveJob(Building p_job)
    {
        _currentBuilding = p_job;

        if (!_playerTask.HasPendingTask()) _playerTask.SetTask(StartCoroutine(StartJob()));
    }

    /// <summary>
    /// Building coroutine
    /// </summary>
    /// <returns></returns>
    private IEnumerator StartJob()
    {
        _animationEvent.SetAttackEvent(_buildEventToBeUsed);

        // Calculate where to go to when bulding a building
        Vector3 jobPosition = _currentBuilding.transform.position;
        Vector2 randomPosition = Random.insideUnitCircle.normalized * _currentBuilding.GetRadius();

        jobPosition.x += randomPosition.x;
        jobPosition.z += randomPosition.y;
        
        // Move to the calculated destination
        _movement.SetDestination(jobPosition);

        // Wait for the navmesh to load
        yield return _movement.WaitForNavMeshToLoad();

        // Look at the destination when moving to the destination
        transform.LookAt(_currentBuilding.transform);

        // Build every Second until finished
        while (!_currentBuilding.IsFinished())
        {
            yield return new WaitForSeconds(1);
            if (!_currentBuilding.IsFinished())
                _animator.SetTrigger("Attack");
        }

        // Reset values
        _currentBuilding = null;
        _playerTask.ResetTask();
    }

    /// <summary>
    /// Checks if the player is currently building
    /// </summary>
    /// <returns>Returns true if player is finised building</returns>
    public bool HasTask()
    {
        return _playerTask.HasPendingTask();
    }

    /// <summary>
    /// Animation Event fired every player hits the building
    /// </summary>
    public void DoWork()
    {
        if (_currentBuilding)
            _currentBuilding.Build(10);
    }
}
