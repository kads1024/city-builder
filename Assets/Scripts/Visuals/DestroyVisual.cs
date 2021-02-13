using UnityEngine;

/// <summary>
/// Used to add visuals when the object is destroyed
/// </summary>
[RequireComponent(typeof(Damageable))]
public class DestroyVisual : MonoBehaviour
{
    // Must have access to the Damageable Component
    private Damageable _damageable;

    // The destroy particle
    [SerializeField] private GameObject _destroyParticle;

    private void Awake()
    {
        _damageable = GetComponent<Damageable>();
    }

    private void OnEnable()
    {
        _damageable.AddOnDestroyListener(Destroy);
    }

    private void OnDisable()
    {
        _damageable.RemoveOnDestroyListener(Destroy);
    }

    private void Destroy()
    {
        if (Application.isPlaying)
            Instantiate(_destroyParticle, transform.position + Vector3.up, Quaternion.identity);
    }
}
