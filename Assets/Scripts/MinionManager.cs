using UnityEngine;
using System.Collections.Generic;

public class MinionManager : MonoBehaviour
{
    [System.Serializable]
    public class MinionEntry
    {
        public GameObject prefab;
        [Range(0, 100)]
        public float spawnChance = 10f;
    }

    public List<MinionEntry> minionPool;
    public List<GameObject> allSpawners;
    public float spawnInterval = 2f;
    public int maxMinions = 20;

    private List<GameObject> activeMinions = new List<GameObject>();
    private float timer;

    void Update()
    {
        for (int i = activeMinions.Count - 1; i >= 0; i--)
        {
            if (activeMinions[i] == null)
            {
                activeMinions.RemoveAt(i);
            }
        }

        if (activeMinions.Count >= maxMinions) return;

        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnFromRandomPoint();
            timer = 0;
        }
    }

    void SpawnFromRandomPoint()
    {
        if (allSpawners.Count == 0 || minionPool.Count == 0) return;

        GameObject selectedPrefab = GetRandomMinionPrefab();
        if (selectedPrefab == null) return;

        int randomIndex = Random.Range(0, allSpawners.Count);
       GameObject selectedSpawner = allSpawners[randomIndex];

        GameObject newMinion = Instantiate(selectedPrefab, selectedSpawner.transform.position, Quaternion.identity);
        newMinion.GetComponent<SplineFollow>().waypoints = selectedSpawner.GetComponent<SpawnPoint>().waypoints;
        activeMinions.Add(newMinion);
    }

    GameObject GetRandomMinionPrefab()
    {
        float totalWeight = 0;
        foreach (var entry in minionPool) totalWeight += entry.spawnChance;

        if (totalWeight <= 0) return null;

        float randomValue = Random.Range(0, totalWeight);
        float processedWeight = 0;

        foreach (var entry in minionPool)
        {
            processedWeight += entry.spawnChance;
            if (randomValue <= processedWeight)
            {
                return entry.prefab;
            }
        }
        return null;
    }
}