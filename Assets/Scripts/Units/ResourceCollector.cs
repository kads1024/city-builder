using System.Collections;
using UnityEngine;

/// <summary>
/// Component to enable the player to Collect Resources. Must have a movement component so that player can go there first before attacking
/// </summary>
[RequireComponent(typeof(Movement), typeof(TaskManager))]
public class ResourceCollector : MonoBehaviour
{
    // Animator used for animating collecting
    [SerializeField] private Animator _animator;

    // Event listeners and event to be used
    [SerializeField] private AnimationEventListener _animationEvent;
    [SerializeField] private GameEvent _collectEventToBeUsed;

    // Hits per second
    [SerializeField] private float _collectSpeed;

    // Current Resources of the player
    [SerializeField] private ResourceManager _resources;

    // Required Component
    private Damageable _targetResource;
    private Movement _movement;
    private TaskManager _playerTask;

    private void Awake()
    {
        _movement = GetComponent<Movement>();
        _playerTask = GetComponent<TaskManager>();
    }

    private void Start()
    {
        // Calculate Collect rate
        _collectSpeed = 1f / _collectSpeed;
    }

    /// <summary>
    /// Moves to the resource to collect it
    /// </summary>
    /// <param name="p_targetResource">Resource to be Collected</param>
    public void CollectTarget(Damageable p_targetResource)
    {
        // Set the target resource
        _targetResource = p_targetResource;

        Resource resource = _targetResource.GetComponent<Resource>();

        // Check if the target is a valid Resource
        if(resource)
        {
            // only attack the target if  there is no pending task and has enough storage to put the resource
            if (!_playerTask.HasPendingTask() && _resources.HasEnoughStorage(resource.GetCost().Resource, resource.GetCost().Amount))
            {
                _playerTask.SetTask(StartCoroutine(StartCollecting()));
            }
        }   
    }

    /// <summary>
    /// Collect coroutine that will hit the target depending on the collect speed
    /// </summary>
    private IEnumerator StartCollecting()
    {
        _animationEvent.SetAttackEvent(HitEventListener);

        // Recalculate movement when there is still a resorce but out of range
        while (_targetResource)
        {
            // Calculate where to go to when collecting a resource
            Vector3 jobPosition = _targetResource.transform.position + _targetResource.GetPositionOffset();
            Vector2 randomPosition = Random.insideUnitCircle.normalized * _targetResource.GetRadius();

            jobPosition.x += randomPosition.x;
            jobPosition.z += randomPosition.y;

            // Move to the calculated destination
            _movement.SetDestination(jobPosition);

            // Look at the destination when moving to the destination
            transform.LookAt(_targetResource.transform);

            // Wait for the navmesh to load
            yield return _movement.WaitForNavMeshToLoad();

            // Hit the resource while it is available and in range
            while (_targetResource && Vector3.Distance(_targetResource.transform.position, transform.position) < 4f)
            {
                yield return new WaitForSeconds(_collectSpeed);
                if (_targetResource && _animator)
                {
                    _animator.SetTrigger("Attack");
                }
            }
        }

        // Resets the player task
        _playerTask.ResetTask();
    }

    public void HitEventListener()
    {
        if (_targetResource)
            _targetResource.Hit(10);
    }
}
