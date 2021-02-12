using UnityEngine;
using DG.Tweening;

/// <summary>
/// Acts as a resource for the game
/// </summary>
[RequireComponent(typeof(Damageable))]
public class Resource : MonoBehaviour
{
    // Resource Manager where the resource will be added
    [SerializeField] private ResourceManager _resourceManager;

    // The resource to be added and the amount
    [SerializeField] private ResourceType _resourceType;
    [SerializeField] private int _amount;

    private void OnEnable()
    {
        GetComponent<Damageable>().AddOnDestroyListener(GiveResource);
        GetComponent<Damageable>().AddOnHitListener(HitResource);
    }

    private void OnDisable()
    {
        GetComponent<Damageable>().RemoveOnDestroyListener(GiveResource);
        GetComponent<Damageable>().RemoveOnHitListener(HitResource);
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
}
