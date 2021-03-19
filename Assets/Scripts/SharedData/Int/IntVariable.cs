using UnityEngine;

/// <summary>
/// Serves as a Persistent Shared Variable
/// </summary>
[CreateAssetMenu(menuName = "Shared Data/Int Variable")]
public class IntVariable : ScriptableObject
{
    public int Value;
}
