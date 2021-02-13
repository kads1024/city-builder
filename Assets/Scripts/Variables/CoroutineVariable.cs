using UnityEngine;

/// <summary>
/// SO Variable for tracking coroutines
/// </summary>
[CreateAssetMenu(menuName = "Variables/Coroutine")]
public class CoroutineVariable : ScriptableObject
{
    // The coroutine that will be tracked
    public Coroutine Value;

    private void Awake()
    {
        Value = null;
    }

    // Setting of value
    public void SetValue(Coroutine p_value)
    {
        Value = p_value;
    }

    public void SetValue(CoroutineVariable p_value)
    {
        Value = p_value.Value;
    }
}
