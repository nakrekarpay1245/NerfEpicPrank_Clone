using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [Header("Düşman Sayısı")]
    public int enemyCount;

    public static LevelManager instance;

    [Header("Kaakter Patrol Noktası Indexi")]
    public int patrolIndex;

    [Header("Oyun başladı mı ? ")]
    public bool isGameStart;

    [Header("Toplam para")]
    public float money;

    [Header("O bölümde kazanılan para")]
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
            //Debug.Log("Level Completed");
            LevelCompleted();
        }
        else
        {
            PlayerController.instance.Patrol();
            // Debug.Log("Change Patrol Point");
        }
        UserInterfaceManager.instance.DisplayCircleInsides();
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
