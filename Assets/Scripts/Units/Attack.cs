﻿using System.Collections;
using UnityEngine;

/// <summary>
/// Component to enable the player to attack an object. Must have a movement component so that player can go there first before attacking
/// </summary>
[RequireComponent(typeof(Movement), typeof(TaskManager))]
public class Attack : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    [SerializeField] private AnimationEventListener _animationEvent;
    [SerializeField] private GameEvent _attackEventToBeUsed;

    [SerializeField] private float _attackSpeed;

    private Damageable _target;
    private Movement _movement;
    private TaskManager _playerTask;

    private void Awake()
    {
        _movement = GetComponent<Movement>();
        _playerTask = GetComponent<TaskManager>();
    }

    private void Start()
    {
        // Calculate attack rate
        _attackSpeed = 1f / _attackSpeed;
    }

    public void AttackTarget(Damageable p_target)
    {
        _target = p_target;

        if (!_playerTask.HasPendingTask())
        {
            _playerTask.SetTask(StartCoroutine(StartAttack()));
        }
    }

    private IEnumerator StartAttack()
    {
        _animationEvent.SetAttackEvent(_attackEventToBeUsed);
        while (_target)
        {
            _movement.SetDestination(_target.transform.position);
            yield return _movement.WaitForNavMeshToLoad();

            while (_target && Vector3.Distance(_target.transform.position, transform.position) < 4f)
            {
                yield return new WaitForSeconds(_attackSpeed);
                if (_target && _animator)
                {
                    _animator.SetTrigger("Attack");
                }
            }
        }

        _playerTask.ResetTask();
    }

    public void AttackEventListener()
    {
        if (_target)
            _target.Hit(10);
    }
}
