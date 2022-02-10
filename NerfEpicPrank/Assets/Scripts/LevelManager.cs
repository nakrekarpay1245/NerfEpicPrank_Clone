using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public int enemyCount;

    public static LevelManager instance;

    public int patrolIndex;

    public bool isGameStart;

    public float money;
    public float levelMoney;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Update()
    {
        if (!isGameStart)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                GameStart();
            }
        }
    }

    public void GameStart()
    {
        isGameStart = true;
        UserInterfaceManager.instance.GameStart();
    }

    public void IncreaseEnemyCount()
    {
        enemyCount++;
    }

    public void DecreaseEnemyCount()
    {
        enemyCount--;
        patrolIndex++;
        if (enemyCount <= 0)
        {
            Debug.Log("Level Completed");
            LevelCompleted();
        }
        else
        {
            PlayerController.instance.Patrol();
            Debug.Log("Change Patrol Point");
        }
    }

    public void LevelCompleted()
    {
        money += levelMoney;
        UserInterfaceManager.instance.LevelCompleted();
    }
    public void LevelFailed()
    {
        money += levelMoney;
        UserInterfaceManager.instance.LevelFailed();
    }

    public void IncreaseMoney()
    {
        levelMoney++;
    }
}
