using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public int enemyCount;

    public static LevelManager instance;

    public int patrolIndex;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void IncreaseEnemyCount()
    {
        enemyCount++;
    }

    public void DecreaseEnemyCount()
    {
        enemyCount--;
        patrolIndex++;
        PlayerController.instance.Patrol();
        if (enemyCount <= 0)
        {
            Debug.Log("Level Completed");
        }
        else
        {
            Debug.Log("Change Patrol Point");
        }
    }
}
