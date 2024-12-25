using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Coin : MonoBehaviour
{
    GameObject player;
    [SerializeField] float speed;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        MoveToPlayer();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerBody")
        {
            if (GameManager.instance != null)
            {
                GameManager.instance.GetCoin(5);
                Destroy(gameObject); 
            }
            else
            {
                Debug.LogError("GameManager Instance is null!");
            }
        }
    }

    void MoveToPlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed);
    }
}
