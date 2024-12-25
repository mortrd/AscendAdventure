using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBody : MonoBehaviour
{
    [SerializeField] GameObject Player;
    public int health;
    public int maxHealth = 3;
    [SerializeField] List<GameObject> hearts = new List<GameObject>();

    private void Start()
    {
        health = maxHealth;
    }

    private void Update()
    {
        if(health <= 0)
        {
            PlayerDeath();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == ("Enemy"))
        {
            GameObject enemy = collision.gameObject;
            int damagetotake = (int)enemy.gameObject.GetComponent<Enemy01>().damage;            
            PlayerTakeDamage(damagetotake);
            Destroy(enemy.gameObject);
        }
        if(collision.gameObject.tag == "EnemyBullet")
        {
            PlayerTakeDamage(1);
            Destroy(collision.gameObject);
        }
    }

    public void PlayerTakeDamage(int DMG)
    {
        if(health <= 3)
        {
            health--;
            Image heartimage = hearts[hearts.Count - 1].gameObject.GetComponent<Image>();
            heartimage.color = Color.black;
            hearts.RemoveAt(hearts.Count - 1);
        }
        else if (health == 4)
        {
            health--;
            maxHealth--;
            GameObject heartimage = GameObject.FindGameObjectWithTag("Heart1");
            Image heartimageColor = heartimage.GetComponent<Image>();
            heartimageColor.color = Color.red;
        }
        else if (health == 5)
        {
            health--;
            maxHealth--;
            GameObject heartimage = GameObject.FindGameObjectWithTag("Heart2");
            Image heartimageColor = heartimage.GetComponent<Image>();
            heartimageColor.color = Color.red;
        }
        else if (health == 6)
        {
            health--;
            maxHealth--;
            GameObject heartimage = GameObject.FindGameObjectWithTag("Heart3");
            Image heartimageColor = heartimage.GetComponent<Image>();
            heartimageColor.color = Color.red;
        }

    }
    
    public void PlayerDeath()
    {
        Player.SetActive(false);
    }

    public void PlayerHeal()
    {
        if (health < maxHealth)
        {
            
            if(health == 1)
            {
                Debug.Log("Heal1");
                GameObject heartimage = GameObject.FindGameObjectWithTag("Heart2");
                hearts.Add(heartimage);
                Image heartimageColor = heartimage.GetComponent<Image>();
                heartimageColor.color = Color.red;
                health++;
            }
            else if(health == 2)
            {
                Debug.Log("Heal2");
                GameObject heartimage = GameObject.FindGameObjectWithTag("Heart3");
                hearts.Add(heartimage);
                Image heartimageColor = heartimage.GetComponent<Image>();
                heartimageColor.color = Color.red;
                health++;
            }
            

        }
        else if (health >= maxHealth && maxHealth < 6)
        {
            if (health == 3)
            {
                Debug.Log("yelloHEAL1");
                maxHealth++;
                GameObject heartimage = GameObject.FindGameObjectWithTag("Heart1");
                Image heartimageColor = heartimage.GetComponent<Image>();
                heartimageColor.color = Color.yellow;
                health++;
            }
            else if (health == 4)
            {
                Debug.Log("yelloHEAL2");
                maxHealth++;
                GameObject heartimage = GameObject.FindGameObjectWithTag("Heart2");
                Image heartimageColor = heartimage.GetComponent<Image>();
                heartimageColor.color = Color.yellow;
                health++;
            }
            else if (health == 5)
            {
                Debug.Log("yelloHEAL3");
                maxHealth++;
                GameObject heartimage = GameObject.FindGameObjectWithTag("Heart3");
                Image heartimageColor = heartimage.GetComponent<Image>();
                heartimageColor.color = Color.yellow;
                health++;
            }

        }
        
    }//no Healing 
}
