using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Resource : MonoBehaviour
{
    [SerializeField] private ResourceManager _resourceManager;

    [SerializeField] private ResourceType _resourceType;
    [SerializeField] private int _amount;

    // Visuals
    private Renderer _renderer;
    private Color _emissionColor;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        if (_renderer)
            _emissionColor = _renderer.material.GetColor("_EmissionColor");
    }

    public void GiveResource()
    {
        _resourceManager.AddResource(_resourceType, _amount);
    }

    public void HitResource()
    {
        transform.DOComplete();
        transform.DOShakeScale(.5f, .2f, 10, 90, true);
    }

    private void OnMouseEnter()
    {   
        if (_renderer)
            _renderer.material.SetColor("_EmissionColor", Color.grey);
    }

    private void OnMouseExit()
    {
        if (_renderer)
            _renderer.material.SetColor("_EmissionColor", _emissionColor);
    }
}
