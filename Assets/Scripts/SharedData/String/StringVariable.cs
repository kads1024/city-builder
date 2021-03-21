using UnityEngine;

/// <summary>
/// Serves as a Persistent Shared Variable
/// </summary>
[CreateAssetMenu(menuName = "Shared Data/String Variable")]
public class StringVariable : ScriptableObject
{
    public string Value;
}
