using UnityEngine;

/// <summary>
/// Serves as a reference for Floats, both shared and non-shared
/// </summary>
[System.Serializable]
public class FloatReference
{
    [SerializeField] private bool _useConstant = true;

    // If UseConstant is true, this will appear in the inspector so that you will only supply a constant value
    [SerializeField] private float _constantValue;

    // If UseConstant is false, it means that the User wants to use a shared data
    [SerializeField] private FloatVariable _variable;

    // The Value to be used for this FloatReference
    public float Value => _useConstant ? _constantValue : _variable.Value;

    /// <summary>
    /// Sets the constant value for this Float reference
    /// </summary>
    /// <param name="p_value">The value to be set</param>
    public void SetConstantValue(float p_value)
    {
        _constantValue = p_value;
    }

    /// <summary>
    /// Sets the constant value for this Float reference
    /// </summary>
    /// <param name="p_value">The value to be set</param>
    public void SetConstantValue(FloatReference p_value)
    {
        _constantValue = p_value.Value;
    }

    /// <summary>
    /// Sets the constant value for this Float reference
    /// </summary>
    /// <param name="p_value">The value to be set</param>
    public void SetVariableValue(float p_value)
    {
        _variable.Value = p_value;
    }

    /// <summary>
    /// Sets the constant value for this Float reference
    /// </summary>
    /// <param name="p_value">The value to be set</param>
    public void SetVariableValue(FloatReference p_value)
    {
        _variable.Value = p_value.Value;
    }
}
