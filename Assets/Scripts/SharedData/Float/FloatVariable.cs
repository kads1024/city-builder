using UnityEngine;

/// <summary>
/// Serves as a Persistent Shared Variable
/// </summary>
[CreateAssetMenu(menuName = "Shared Data/Float Variable")]
public class FloatVariable : ScriptableObject
{
    public float Value;
}
