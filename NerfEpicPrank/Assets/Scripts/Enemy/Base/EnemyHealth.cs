using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public int health;

    public Text healthText;

    public bool impact;
    public void TakeDamage()
    {
        Debug.Log("nabre" + impact) ;
        health--;
        healthText.text = health.ToString();
        if (health <= 0)
        {
            gameObject.SetActive(false);
            LevelManager.instance.DecreaseEnemyCount();
        }
        else
        {
            Debug.Log("nabre" + impact);

            impact = true;
        }
    }
}
