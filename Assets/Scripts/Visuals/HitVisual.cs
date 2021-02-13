using DG.Tweening;
using UnityEngine;

/// <summary>
/// The visual when the object is hit
/// </summary>
[RequireComponent(typeof(Damageable))]
public class HitVisual : MonoBehaviour
{
    // Must have access to the Damageable Component
    private Damageable _damageable;

    private void Awake()
    {
        _damageable = GetComponent<Damageable>();
    }

    private void OnEnable()
    {
        _damageable.AddOnHitListener(Hit);
    }

    private void OnDisable()
    {
        _damageable.RemoveOnHitListener(Hit);
    }

    private void Hit()
    {
        transform.DOComplete();
        transform.DOShakeScale(.5f, .2f, 10, 90, true);
    }
}
