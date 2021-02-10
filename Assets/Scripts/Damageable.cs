using UnityEngine;

/// <summary>
/// Component that has health and can be damaged
/// </summary>
public class Damageable : MonoBehaviour
{
    // Game Events
    [SerializeField] private GameEvent _onDestroy;
    [SerializeField] private GameEvent _onHit;

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
        _onHit.Raise();
        _currentHealth -= p_damage;

        if (_currentHealth <= 0)
            Destroy();
    }

    /// <summary>
    /// Raise any Destroy event before destroying this object from the scene
    /// </summary>
    private void Destroy()
    {
        _onDestroy.Raise();
        Destroy(gameObject);
    }
}
