using DG.Tweening;
using UnityEngine;

/// <summary>
/// Script to be attached to the object that is currently selected
/// </summary>
public class SelectedObject : MonoBehaviour
{
    private void OnEnable()
    {
        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(0).DOScale(0, .2f).From().SetEase(Ease.OutBack);
    }

    private void OnDisable()
    {
        transform.GetChild(0).gameObject.SetActive(false);
    }
}
