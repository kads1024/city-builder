using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Utility Class used for procedural point generation.
/// </summary>
public static class PoissonDiscSampling
{
    /// <summary>
    /// Initializes the sampling
    /// </summary>
    public static void Init(string p_seed)
    {
        int seedHash = p_seed.GetHashCode();
        Random.InitState(seedHash);
    }

    /// <summary>
    /// Used to genereate the points for a sample region
    /// </summary>
    /// <param name="p_radius">Radius of a point</param>
    /// <param name="p_regionSize">Size of the region to place the points</param>
    /// <param name="p_numSamplesBeforeRejection">How many times to check for a point before rejection</param>
    /// <returns>Returns the list of points position inside the specified region</returns>
    public static List<Vector2> GeneratePoints(float p_radius, Vector2 p_regionSize, int p_numSamplesBeforeRejection = 30)
    {
        // Get the cell size by using the inverse of the pythagorean theorem
        float cellSize = p_radius / Mathf.Sqrt(2f);

        // Make the list of cells inside the region size
        int[,] grid = new int[Mathf.CeilToInt(p_regionSize.x / cellSize), Mathf.CeilToInt(p_regionSize.y / cellSize)];

        // The points that will be generated
        List<Vector2> points = new List<Vector2>();
        List<Vector2> spawnPoints = new List<Vector2>();

        spawnPoints.Add(p_regionSize/2f);

        while(spawnPoints.Count > 0)
        {
            int spawnIndex = Random.Range(0, spawnPoints.Count);
            Vector2 spawnCenter = spawnPoints[spawnIndex];

            bool isCandidateValid = false;
            for (int i = 0; i < p_numSamplesBeforeRejection; i++)
            {
                float angle = Random.value * Mathf.PI * 2f;
                Vector2 direction = new Vector2(Mathf.Sin(angle), Mathf.Cos(angle));
                Vector2 candidate = spawnCenter + direction * Random.Range(p_radius, 2f * p_radius);
                if(IsValid(candidate, p_regionSize, cellSize, p_radius, points, grid))
                {
                    points.Add(candidate);
                    spawnPoints.Add(candidate);
                    grid[(int)(candidate.x / cellSize), (int)(candidate.y / cellSize)] = points.Count;
                    isCandidateValid = true;
                    break;
                }
            }
            if(!isCandidateValid)
            {
                spawnPoints.RemoveAt(spawnIndex);
            }
        }

        return points;
    }

    /// <summary>
    /// Checks if the current point is a valid point to be generated
    /// </summary>
    /// <param name="p_candidate">The Point to check</param>
    /// <param name="p_regionSize">The size of the region</param>
    /// <param name="p_cellSize">The Size of the indivudual cell</param>
    /// <param name="p_radius">Radius of the point</param>
    /// <param name="p_points">The List of points already generated</param>
    /// <param name="p_grid">The Grid that will be used</param>
    /// <returns></returns>
    private static bool IsValid(Vector2 p_candidate, Vector2 p_regionSize, float p_cellSize, float p_radius, List<Vector2> p_points, int[,] p_grid)
    {
        // Check if the point is within the map
        if(p_candidate.x >= 0 && p_candidate.x < p_regionSize.x && p_candidate.y >= 0 && p_candidate.y < p_regionSize.y)
        {
            // Calculate the search points x value
            int cellX = (int)(p_candidate.x / p_cellSize);
            int searchStartX = Mathf.Max(0, cellX - 2);
            int searchEndX = Mathf.Min(cellX + 2, p_grid.GetLength(0) - 1);

            // Calculate the search points y value
            int cellY = (int)(p_candidate.y / p_cellSize);
            int searchStartY = Mathf.Max(0, cellY - 2);
            int searchEndY = Mathf.Min(cellY + 2, p_grid.GetLength(1) - 1);

            // Start Searching for a valid point in the area
            for (int x = searchStartX; x <= searchEndX; x++)
            {
                for (int y = searchStartY; y <= searchEndY; y++)
                {
                    int pointIndex = p_grid[x, y] - 1;
                    if (pointIndex != -1)
                    {
                        float sqrDistance = (p_candidate - p_points[pointIndex]).sqrMagnitude;
                        if(sqrDistance < p_radius* p_radius)
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        // If not, then it is not valid
        return false;
    }
}
