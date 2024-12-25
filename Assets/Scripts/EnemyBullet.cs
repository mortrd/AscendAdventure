using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    Transform Player;
    Vector2 target;
    [SerializeField]float speed;
    [SerializeField] Rigidbody2D rb;
    Vector2 direction;
    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        target = Player.transform.position;
        direction = new Vector2(this.target.x - transform.position.x, this.target.y - transform.position.y).normalized * speed;
    }
    private void FixedUpdate()
    {
        Move();
    }
    void Move()
    {
        rb.velocity = direction;
    }
}
