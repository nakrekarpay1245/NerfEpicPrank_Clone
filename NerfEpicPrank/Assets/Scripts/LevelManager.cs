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

    public Text levelText;
    public Text moneyText;
    public GameObject tutorial;
    public GameObject enemyDisplay;
    public GameObject circle;
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
        tutorial.SetActive(false);
        DisplayEnemies();
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

    public void DisplayEnemies()
    {
        for (int i = 0; i < enemyCount; i++)
        {
            Instantiate(circle, enemyDisplay.transform);
        }
    }
}
