using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Component that has health and can be damaged
/// </summary>
public class Damageable : MonoBehaviour
{
    // Game Events
    private UnityEvent _onDestroy = new UnityEvent();
    private UnityEvent _onHit = new UnityEvent();

    // Health
    [SerializeField] private int _totalHealth = 100;
    private int _currentHealth;

    // Initialization
    private void Start()
    {
        _currentHealth = _totalHealth;
    }

    /// <summary>
    /// Deduct the health of the object then raise the events that will happen when the object is hit.
    /// Check if the object has no more health so it can be removed from the scene
    /// </summary>
    /// <param name="p_damage"></param>
    public void Hit(int p_damage)
    {
        _onHit.Invoke();
        _currentHealth -= p_damage;

        if (_currentHealth <= 0)
            Destroy();
    }

    /// <summary>
    /// Raise any Destroy event before destroying this object from the scene
    /// </summary>
    private void Destroy()
    {
        _onDestroy.Invoke();
        Destroy(gameObject);
    }

    // Adds Listeners to the events
    public void AddOnDestroyListener(UnityAction p_listener)
    {
        _onDestroy.AddListener(p_listener);
    }

    public void AddOnHitListener(UnityAction p_listener)
    {
        _onHit.AddListener(p_listener);
    }

    // Removes Listeners to events
    public void RemoveOnDestroyListener(UnityAction p_listener)
    {
        _onDestroy.RemoveListener(p_listener);
    }

    public void RemoveOnHitListener(UnityAction p_listener)
    {
        _onHit.RemoveListener(p_listener);
    }
}
