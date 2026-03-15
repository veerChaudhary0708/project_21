using UnityEngine;
using System.Collections;

public class ZombieSpawner : MonoBehaviour 
{
    public GameObject zombiePrefab;
    public int maxZombies = 10;
    public float spawnRadius = 15f;

    void Start() 
    {
        for (int i = 0; i < maxZombies; i++) SpawnZombie(); 
    }

    public void SpawnZombie() 
    {
        Vector3 randomPos = new Vector3(Random.Range(-spawnRadius, spawnRadius), 0, Random.Range(-spawnRadius, spawnRadius));
        Instantiate(zombiePrefab, randomPos, Quaternion.identity);
    }

    public void ZombieHit() 
    {
        StartCoroutine(RespawnRoutine()); 
    }

    IEnumerator RespawnRoutine() 
    {
        yield return new WaitForSeconds(Random.Range(2f, 3f)); 
        SpawnZombie();
    }
}