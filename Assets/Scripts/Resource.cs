using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Resource : MonoBehaviour
{
    [SerializeField] private ResourceManager _resourceManager;

    [SerializeField] private ResourceType _resourceType;
    [SerializeField] private int _amount;

    public void GiveResource()
    {
        _resourceManager.AddResource(_resourceType, _amount);
    }

    public void HitResource()
    {
        transform.DOComplete();
        transform.DOShakeScale(.5f, .2f, 10, 90, true);
    }
}
