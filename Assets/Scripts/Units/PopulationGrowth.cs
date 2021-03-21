using System.Collections;
using UnityEngine;

/// <summary>
/// Responsible for Growing the population of the game
/// </summary>
public class PopulationGrowth : MonoBehaviour
{
    // The Prefab of the builder to use
    [SerializeField] private Builder _builder;

    // Spawn rate at which the builder will be generated
    [SerializeField] private float _spawnRate;

    // Area on where to spawn the builders
    [SerializeField] private StartingArea _spawnArea;

    // Count of Builders Present
    [SerializeField] private ResourceManager m_resources;

    public float SpawnRate => _spawnRate;

    public float CurrentTime { get; private set; }
    public int SpawnAmount { get; private set; }
    public bool AboutToOverflow { get; private set; }
    public bool Overflowed { get; private set; }

    // Start is called before the first frame update
    private void Start()
    {
        CurrentTime = 0.0f;
        SpawnAmount = 1;
        StartCoroutine(SpawnBuilders());
        AboutToOverflow = false;
        Overflowed = false;
    }

    /// <summary>
    /// Used to spawn the builders at a time interval
    /// </summary>
    private IEnumerator SpawnBuilders()
    {
        while (true)
        {
            yield return new WaitForSeconds(_spawnRate);

            
            for (int i = 0; i < SpawnAmount; i++)
            {
                float randomX = _spawnArea.transform.position.x + Random.Range(-_spawnArea.Radius + 1, _spawnArea.Radius - 1);
                float randomZ = _spawnArea.transform.position.y + Random.Range(-_spawnArea.Radius + 1, _spawnArea.Radius - 1);

                Vector3 randomPosition = new Vector3(randomX, 1f, randomZ);
                Instantiate(_builder, randomPosition, Quaternion.identity);
                m_resources.AddResource(new Cost() { Resource = ResourceType.Person, Amount = 1 });
            }
            SpawnAmount++;            
        }
    }

    private void Update()
    {
        if (m_resources.CurrentResources[ResourceType.Person] >= m_resources.CurrentResourceLimit[ResourceType.Person])
        {
            Overflowed = true;

            // TODO: Game Over
        }
        else
        {
            Overflowed = false;
        }

        if (SpawnAmount + m_resources.CurrentResources[ResourceType.Person] >= m_resources.CurrentResourceLimit[ResourceType.Person])
        {
            AboutToOverflow = true;
        }
        else
        {
            AboutToOverflow = false;
        }

    }
}
