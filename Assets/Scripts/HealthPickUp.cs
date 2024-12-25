using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickUp : MonoBehaviour
{
    PlayerBody body;

    private void Start()
    {
        body = GameObject.FindGameObjectWithTag("PlayerBody").GetComponent<PlayerBody>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.gameObject.tag == "Player")
        {
            body.PlayerHeal();
        }
    }
}
