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

    // Start is called before the first frame update
    private void Start()
    {
        StartCoroutine(SpawnBuilders());
    }

    /// <summary>
    /// Used to spawn the builders at a time interval
    /// </summary>
    private IEnumerator SpawnBuilders()
    {
        while(true)
        {
            yield return new WaitForSeconds(_spawnRate);

            float randomX = _spawnArea.transform.position.x + Random.Range(0.0f, _spawnArea.Radius);
            float randomZ = _spawnArea.transform.position.y + Random.Range(0.0f, _spawnArea.Radius);

            Vector3 randomPosition = new Vector3(randomX, 1f, randomZ);
            Instantiate(_builder, randomPosition, Quaternion.identity);
        }  
    }
}
