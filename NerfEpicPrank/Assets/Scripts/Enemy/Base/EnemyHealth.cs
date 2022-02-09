using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    //public static EnemyHealth instance;

    public int health;

    public Text healthText;

    public bool impact;
    //private void Awake()
    //{
    //    if (instance == null)
    //    {
    //        instance = this;
    //    }
    //    healthText.text = health.ToString();
    //}
    public void TakeDamage()
    {
        health--;
        healthText.text = health.ToString();
        if (health <= 0)
        {
            gameObject.SetActive(false);
            LevelManager.instance.DecreaseEnemyCount();
        }
        else
        {
            impact = true;
        }
    }
}
