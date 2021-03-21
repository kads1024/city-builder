using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Responsible for UI for Population
/// </summary>
public class PopulationGrowthUI : MonoBehaviour
{
    // Resources of the population
    [SerializeField] private ResourceManager _resources;

    // Manager
    [SerializeField] private PopulationGrowth _populationManager;

    // Texts
    [SerializeField] private TextMeshProUGUI _timerText;
    [SerializeField] private TextMeshProUGUI _populationText;
    [SerializeField] private TextMeshProUGUI _populationGrowthText;
    [SerializeField] private Image _populationBar;
    [SerializeField] private Image _timerBar;

    private float _timer;

    private void Start()
    {
        _timer = _populationManager.SpawnRate;
    }

    private void Update()
    {
        _timerText.text = ((int)_timer).ToString() + "s";
        _timerBar.fillAmount = (int)_timer / _populationManager.SpawnRate;

        _populationText.text = _resources.CurrentResources[ResourceType.Person].ToString() + " / " + _resources.CurrentResourceLimit[ResourceType.Person].ToString();
        _populationBar.fillAmount = (float)_resources.CurrentResources[ResourceType.Person]  / _resources.CurrentResourceLimit[ResourceType.Person];

        _populationGrowthText.text = "Population Growth:\n" + _populationManager.SpawnAmount.ToString();
        _timer -= Time.deltaTime;

        if (_timer <= 0.0f)
        {
            _timer = _populationManager.SpawnRate;
        }
    }

}
