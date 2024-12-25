using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Build;
using UnityEngine;

public class Enemy01 : BaseEnemy
{
    [SerializeField] float chargeRange;
    [SerializeField] float speed = 0.05f;
    bool isCharging;
    GameObject player;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        SetInitialHealth();
    }
    private void Update()
    {
        if(health <= 0)
        {
            Death();
        }
        if(transform.position.y < player.transform.position.y + chargeRange)
        {
            isCharging = true;
        }
        if(isCharging)
        {
            ChargeAttack();
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "PlayerBody")
        {
            Death();
        }
        if(collision.gameObject.tag == "Bullet")
        {
            GameObject Bullet = collision.gameObject;
            Destroy(Bullet);
            TakeDamage(BulletScript.BulletDamage);

        }
    }

    void ChargeAttack()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed);
    }
}
