using UnityEngine;

/// <summary>
/// Acts as a resource for the game
/// </summary>
[RequireComponent(typeof(Damageable))]
public class Resource : MonoBehaviour
{
    // Resource Manager where the resource will be added
    [SerializeField] private ResourceManager _resourceManager;

    // The resource to be added and the amount
    [SerializeField] private Cost _resourceGained;

    private void OnEnable()
    {
        GetComponent<Damageable>().AddOnDestroyListener(GiveResource);
    }

    private void OnDisable()
    {
        GetComponent<Damageable>().RemoveOnDestroyListener(GiveResource);
    }

    public void GiveResource()
    {
        _resourceManager.AddResource(_resourceGained);
    }
}
