using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class UserInterfaceManager : MonoBehaviour
{
    [Tooltip("Yeniden Başlat")]
    public GameObject retryButton;

    [Tooltip("Sonraki Bçlüm")]
    public GameObject nextButton;

    [Tooltip("Mevcut Bölüm Numarası")]
    public int levelNumber;

    [HideInInspector]
    public int enemyCount;

    [Tooltip("Mevcut Bölüm Numarası Metni")]
    public Text levelText;

    [Tooltip("para metni")]
    public Text moneyText;

    [Tooltip("Oynanış gösterme objesi")]
    public GameObject tutorial;

    [Tooltip("Düşman göstergesi")]
    public GameObject enemyDisplay;

    public GameObject circlePrefab;
    public GameObject circleInsidePrefab;
    public List<GameObject> circles;
    public List<GameObject> circleInsides;

    public static UserInterfaceManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        //levelCompletedPanel.SetActive(false);
        retryButton.SetActive(false);
        nextButton.SetActive(false);

        levelText.text = "Level " + levelNumber.ToString();
    }

    public void LevelChanger(int number)
    {
        SceneManager.LoadScene(number);
    }
    public void LevelChanger(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void OpenUrl(string url)
    {
        Application.OpenURL(url);
    }

    public void GameStart()
    {
        tutorial.SetActive(false);
        DisplayEnemies();
    }


    public void LevelCompleted()
    {
        nextButton.SetActive(true);
        moneyText.text = LevelManager.instance.money.ToString();
    }

    public void LevelFailed()
    {
        retryButton.SetActive(true);
        moneyText.text = LevelManager.instance.money.ToString();
    }

    public void DisplayEnemies()
    {
        this.enemyCount = LevelManager.instance.enemyCount;
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
