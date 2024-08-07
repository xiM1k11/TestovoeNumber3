using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public static bool Win;
    public GameObject[] Enemys;
    public static float SecForSpawn;

    public Vector3 spawnCenter;     // Центр области спавна
    public float spawnRadius = 10f; // Радиус области спавна
    public float spawnHeight = 1f;

    public static List<GameObject> SpawnedEnemies = new List<GameObject>();

    private IEnumerator Start()
    {
        SecForSpawn = 5f;
        while (!Win)
        {
            StartCoroutine(SpawnEnemy());
            yield return new WaitForSeconds(SecForSpawn);
        }
    }

    public IEnumerator SpawnEnemy()
    {
        yield return null;
        int randomIndex = UnityEngine.Random.Range(0, Enemys.Length);
        Vector3 randomPosition = GetRandomPositionInCircle();
        GameObject enemy = Instantiate(Enemys[randomIndex], randomPosition, Quaternion.identity, gameObject.transform);
        SpawnedEnemies.Add(enemy); // Добавляем врага в список
        if (SecForSpawn > 0.5f)
        {
            SecForSpawn -= 0.5f;
        }
        else if (SecForSpawn < 0.5f && SecForSpawn > 0.1f)
        {
            SecForSpawn -= 0.1f;
        }
        else if (SecForSpawn < 0.1f && SecForSpawn > 0.01f)
        {
            SecForSpawn -= 0.01f;
        }
        else
        {
            yield return null;
        }
    }

    private Vector3 GetRandomPositionInCircle()
    {
        float angle = UnityEngine.Random.Range(0, Mathf.PI * 2);
        float radius = UnityEngine.Random.Range(0, spawnRadius);
        float x = Mathf.Cos(angle) * radius;
        float z = Mathf.Sin(angle) * radius;
        Vector3 randomPosition = new Vector3(x, spawnHeight, z);
        return spawnCenter + randomPosition;
    }

    public static void RemoveEnemy(GameObject enemy)
    {
        if (SpawnedEnemies.Contains(enemy))
        {
            SpawnedEnemies.Remove(enemy);
        }
    }
}
