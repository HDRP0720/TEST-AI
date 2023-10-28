using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
  public Transform player;
  public int numberOfEnemiesToSpawn = 5;
  public float spawnInterval = 1f;
  public List<Enemy> enemyPrefabs = new List<Enemy>();
  public ESpawnMethod spawnMethod = ESpawnMethod.RoundRobin;

  private Dictionary<int, ObjectPool> _enemyObejctPools = new Dictionary<int, ObjectPool>();
  private NavMeshTriangulation _triangulation;

  private void Awake()
  {
    for (int i = 0; i < enemyPrefabs.Count; i++)
    {
      _enemyObejctPools.Add(i, ObjectPool.CreateInstance(enemyPrefabs[i], numberOfEnemiesToSpawn));
    }
  }
  private void Start()
  {
    _triangulation = NavMesh.CalculateTriangulation();
    StartCoroutine(SpawnEnemies());
  }

  private IEnumerator SpawnEnemies()
  {
    WaitForSeconds wait = new WaitForSeconds(spawnInterval);
    int spawnedEnemies = 0;
    
    while (spawnedEnemies < numberOfEnemiesToSpawn)
    {
      if (spawnMethod == ESpawnMethod.RoundRobin)
        SpawnRoundRobinEnemy(spawnedEnemies);
      else if (spawnMethod == ESpawnMethod.Random)
        SpawnRandomEnemy();

      spawnedEnemies++;
      
      yield return wait;
    }
  }
  
  private void SpawnRandomEnemy()
  {
    DoSpawnEnemy(Random.Range(0, enemyPrefabs.Count));
  }

  private void SpawnRoundRobinEnemy(int spawnedEnemies)
  {
    int spawnIndex = spawnedEnemies % enemyPrefabs.Count;
    DoSpawnEnemy(spawnIndex);
  }
  private void DoSpawnEnemy(int spawnIndex)
  {
    PoolableObejct poolableObejct = _enemyObejctPools[spawnIndex].GetObject();

    if (poolableObejct != null)
    {
      Enemy enemy = poolableObejct.GetComponent<Enemy>();
      int vertexIndex = Random.Range(0, _triangulation.vertices.Length);
      NavMeshHit hit;
      
      if (NavMesh.SamplePosition(_triangulation.vertices[vertexIndex], out hit, 2f, -1))
      {
        enemy.enemyMovement.target = player;
        enemy.agent.Warp(hit.position);
        enemy.agent.enabled = true;
        enemy.enemyMovement.StartChasing();
      }
      else
      {
        Debug.LogError($"Unable to place NavMeshAgent on NavMesh. Tried to use {_triangulation.vertices[vertexIndex]}");
      }
    }
  }
}

public enum ESpawnMethod { RoundRobin, Random }
