using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Used for Displaying Resources Count
/// </summary>
public class ResourceUIManager : MonoBehaviour
{
    // List of all texts for the UI
    [SerializeField] private List<TextMeshProUGUI> m_resourceTexts;
    
    // The Resource Container that contains current resourceCount
    [SerializeField] private ResourceManager m_resources;

    private void Update()
    {
        for(int i = 0; i < m_resourceTexts.Count; i++)
        {
            m_resourceTexts[i].text = m_resources.CurrentResources[(ResourceType)i] + " / " + m_resources.CurrentResourceLimit[(ResourceType)i];
        }
    }
}
