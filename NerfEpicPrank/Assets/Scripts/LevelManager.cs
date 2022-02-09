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
    public GameObject circlePrefab;
    public GameObject circleInsidePrefab;
    public List<GameObject> circles;
    public List<GameObject> circleInsides;
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
            GameObject currentCircle = Instantiate(circlePrefab, enemyDisplay.transform);
            circles.Add(currentCircle);
            GameObject currentCircleInside = Instantiate(circleInsidePrefab, currentCircle.transform);
            circleInsides.Add(currentCircleInside);
            currentCircleInside.SetActive(false);
        }
    }
    public void DisplayCircleInsides()
    {
        circleInsides[0].SetActive(true);
    }
}
