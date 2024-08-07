using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    [Header("Player")]
    private PlayerController player;
    private float miHP;

    [Header("Enemy")]
    private EnemyController[] enemies;

    private void Start()
    {
        enemies = FindObjectsOfType<EnemyController>();
    }

    private void Update()
    {
        ManageEnemyHP();
    }

    private void ManageEnemyHP()
    {
        for (int i = 0; i < EnemyController.SpawnedEnemies.Count; i++)
        {
            GameObject enemy = EnemyController.SpawnedEnemies[i];

            if(enemy != null)
            {
            EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();

            if (enemyHealth != null && enemyHealth.currentHP <= 0)
            {
                EnemyController.RemoveEnemy(enemy);
                
            }
            }
        }
    }
}
