using UnityEngine;

/// <summary>
/// Serves as a reference for Strings, both shared and non-shared
/// </summary>
[System.Serializable]
public class StringReference
{
    [SerializeField] private bool _useConstant = true;

    // If UseConstant is true, this will appear in the inspector so that you will only supply a constant value
    [SerializeField] private string _constantValue;

    // If UseConstant is false, it means that the User wants to use a shared data
    [SerializeField] private StringVariable _variable;

    // The Value to be used for this IntReference
    public string Value => _useConstant ? _constantValue : _variable.Value;

    /// <summary>
    /// Sets the constant value for this Int reference
    /// </summary>
    /// <param name="p_value">The value to be set</param>
    public void SetConstantValue(string p_value)
    {
        _constantValue = p_value;
    }

    /// <summary>
    /// Sets the constant value for this Int reference
    /// </summary>
    /// <param name="p_value">The value to be set</param>
    public void SetConstantValue(StringVariable p_value)
    {
        _constantValue = p_value.Value;
    }

    /// <summary>
    /// Sets the constant value for this Int reference
    /// </summary>
    /// <param name="p_value">The value to be set</param>
    public void SetVariableValue(string p_value)
    {
        _variable.Value = p_value;
    }

    /// <summary>
    /// Sets the constant value for this Int reference
    /// </summary>
    /// <param name="p_value">The value to be set</param>
    public void SetVariableValue(StringVariable p_value)
    {
        _variable.Value = p_value.Value;
    }
}
