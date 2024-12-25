using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BaseEnemy : MonoBehaviour
{
    public Slider healthBar;
    public float maxHealth;
    public float health;
    public float damage;
    public GameObject coin;

    private void Start()
    {
        
    }
    private void Update()
    {

    }
    public virtual void TakeDamage(float damage) 
    {
        health -= damage;
        healthBar.value = health;
    }

    public virtual void SetInitialHealth()
    {
        health = maxHealth;
        healthBar.maxValue = maxHealth;
        healthBar.value = health;
    }
    public virtual void DamagePlayer(float damage)
    {
        PlayerControl.health -= damage;
        Debug.Log("PlayerDamage");//Damaging Player
    }
    public virtual void Death()
    {
        DropGold();
        Destroy(gameObject);
        //Death
    }
    public virtual void DropGold()
    {
        Instantiate(coin,transform.position,Quaternion.identity);
    }
}
