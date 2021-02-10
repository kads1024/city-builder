using UnityEngine;

/// <summary>
/// The object that the GroundRuntimeSet will be tracking
/// </summary>
public class Ground : MonoBehaviour
{
    // The set which we will be using
    [SerializeField] private GroundRuntimeSet _runTimeSet;

    // Add the object to the set when it is enabled
    private void OnEnable()
    {
        _runTimeSet.Add(this);
    }

    // Remove the object when it is disabled
    private void OnDisable()
    {
        _runTimeSet.Remove(this);
    }
}
