using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy02 : BaseEnemy
{
    [SerializeField] LayerMask playermask;
    [SerializeField] float visionRange;
    GameObject Player;
    [SerializeField] GameObject shootPoint;
    [SerializeField] GameObject enemyBullet;
    public bool isidle;
    public bool alert;
    [SerializeField] float speed = 5;
    bool wonderleft;
    bool wonderright;
    float enemyAttackSpeed = 1.2f;
    [SerializeField] float enemyAttackTime = 0;
    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        wonderleft = true;
        isidle = true;
        SetInitialHealth();
    }
    private void Update()
    {
        CheckForPlayer();
        if(health <= 0)
        {
            Death();
        }
        if (transform.position.x >= 8)
        {
            wonderright = false;
            wonderleft = true;
        }
        if(transform.position.x <= -8)
        {
            wonderleft =false;
            wonderright=true;
        }
        if(alert)
        {
            Vector3 rotation = transform.position - Player.transform.position;
            float rot = Mathf.Atan2 (rotation.y, rotation.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0,0,rot - 20);
            if(enemyAttackTime >= 0)
            {
                enemyAttackTime -= Time.deltaTime;
            }
            if(enemyAttackTime <= 0)
            {
                StartCoroutine(Alert());
                enemyAttackTime = enemyAttackSpeed;
            }
            
        }
    }
    private void FixedUpdate()
    {
        if (isidle)
        {
            if (wonderleft)
            {
                WonderLeft();
            }
            if (wonderright)
            {
                WonderRight();
            }
        }
    }
    void WonderLeft()
    {
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(-8,transform.position.y), speed);
    }
    void WonderRight()
    {
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(8, transform.position.y), speed);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.gameObject.tag == "Bullet")
        {
            GameObject Bullet = collision.gameObject;
            Destroy(Bullet);
            TakeDamage(BulletScript.BulletDamage);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            isidle = true; alert = false;
        } 
    }

    IEnumerator Alert()
    {
        yield return new WaitForSeconds(0.4f);
        EnemyShoot();
    } 
    void EnemyShoot()
    {
        GameObject Bull = Instantiate(enemyBullet, shootPoint.transform.position, Quaternion.identity);
    }
    void CheckForPlayer()
    {
        Collider2D player = Physics2D.OverlapCircle(transform.position, visionRange, playermask);
        if(player != null)
        {
            Debug.Log("PlayerFound");
            isidle = false;
            alert = true;
        }
        if(player == null)
        {
            alert = false;
            isidle = true;
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere (transform.position, visionRange);
    }
}
