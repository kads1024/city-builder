using UnityEngine;

/// <summary>
/// Changes the Visual of the object when the mouse is hovering over
/// </summary>
public class HoverVisual : MonoBehaviour
{
    // Visuals
    private Renderer _renderer;
    private Color _emissionColor;

    [SerializeField] private Color _hoverColor;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        if (_renderer)
            _emissionColor = _renderer.material.GetColor(Constants.EMISSION_COLOR);
    }

    // Change the Emission color when hovering
    private void OnMouseEnter()
    {
        if (_renderer)
            _renderer.material.SetColor(Constants.EMISSION_COLOR, _hoverColor);
    }

    // Revert back the emission color when not hovering
    private void OnMouseExit()
    {
        if (_renderer)
            _renderer.material.SetColor(Constants.EMISSION_COLOR, _emissionColor);
    }
}
