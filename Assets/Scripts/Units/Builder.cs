using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Movement))]
public class Builder : MonoBehaviour
{
    private Building _currentBuilding;
    private Movement _movement;

    [SerializeField] private CoroutineVariable _currentTask;

    [SerializeField] private GameEvent _buildEventToBeUsed;
    [SerializeField] private AnimationEventListener _animationEvent;

    [SerializeField] private Animator _animator;
    private void Awake()
    {
        _movement = GetComponent<Movement>();
    }

    public void GiveJob(Building p_job)
    {
        _currentBuilding = p_job;

        if (_currentTask.Value != null)
            StopCoroutine(_currentTask.Value);

        _currentTask.SetValue(StartCoroutine(StartJob()));
    }

    private IEnumerator StartJob()
    {
        _animationEvent.SetAttackEvent(_buildEventToBeUsed);

        // Calculate where to go to when bulding a building
        Vector3 jobPosition = _currentBuilding.transform.position;
        Vector2 randomPosition = Random.insideUnitCircle.normalized * _currentBuilding.GetRadius();

        jobPosition.x += randomPosition.x;
        jobPosition.z += randomPosition.y;

        _movement.SetDestination(jobPosition);

        yield return _movement.WaitForNavMeshToLoad();

        transform.LookAt(_currentBuilding.transform);
        while (!_currentBuilding.IsFinished())
        {
            yield return new WaitForSeconds(1);
            if (!_currentBuilding.IsFinished())
                _animator.SetTrigger("Attack");
        }

        _currentBuilding = null;
        _currentTask.Value = null;
    }

    public bool HasTask()
    {
        return _currentTask.Value != null;
    }

    public void DoWork()
    {
        if (_currentBuilding)
            _currentBuilding.Build(10);
    }
}
