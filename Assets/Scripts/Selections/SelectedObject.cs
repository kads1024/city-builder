using UnityEngine;

/// <summary>
/// Script to be attached to the object that is currently selected
/// </summary>
public class SelectedObject : MonoBehaviour
{
    private void OnEnable()
    {
        transform.GetChild(0).gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        transform.GetChild(0).gameObject.SetActive(false);
    }
}
