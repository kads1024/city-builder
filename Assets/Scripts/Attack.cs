using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Movement))]
public class Attack : MonoBehaviour
{
    [SerializeField] private float _attackSpeed;

    private Coroutine _currentTask;

    private Damageable _target;
    private Movement _movement;
    [SerializeField] private Animator _animator;

    private void Awake()
    {
        _movement = GetComponent<Movement>();
    }

    private void Start()
    {
        _attackSpeed = 1f / _attackSpeed;
    }

    public void AttackTarget(Damageable p_target)
    {
        StopAttacking();
        _target = p_target;

        _currentTask = StartCoroutine(StartAttack());
    }

    private IEnumerator StartAttack()
    {
        
        while(_target)
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

        _currentTask = null;    
    }

    public void AttackEventListener()
    {
        if (_target)
            _target.Hit(10);
    }

    public void StopAttacking()
    {
        _target = null;
        if (_currentTask != null)
            StopCoroutine(_currentTask);
    }

}
