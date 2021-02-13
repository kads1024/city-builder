using System.Collections;
using UnityEngine;

/// <summary>
/// Component to enable the player to attack an object. Must have a movement component so that player can go there first before attacking
/// </summary>
[RequireComponent(typeof(Movement))]
public class Attack : MonoBehaviour
{
    [SerializeField] private CoroutineVariable _playerTask;
    [SerializeField] private Animator _animator;

    [SerializeField] private float _attackSpeed;

    private Damageable _target;
    private Movement _movement;

    private void Awake()
    {
        _movement = GetComponent<Movement>();
    }

    private void Start()
    {
        // Calculate attack rate
        _attackSpeed = 1f / _attackSpeed;
    }

    public void AttackTarget(Damageable p_target)
    {
        StopAttacking();
        _target = p_target;

        _playerTask.SetValue(StartCoroutine(StartAttack()));
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

        _playerTask.Value = null;    
    }

    public void AttackEventListener()
    {
        if (_target)
            _target.Hit(10);
    }

    public void StopAttacking()
    {
        _target = null;
        if (_playerTask.Value != null)
        {
            StopCoroutine(_playerTask.Value);
        }
            
    }
}
