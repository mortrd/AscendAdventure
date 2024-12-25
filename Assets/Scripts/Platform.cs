using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{

    GameObject cam;
    [SerializeField] float deadLine = 7f;
    public float jumpForce = 10f;

    private void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera");
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.relativeVelocity.y <= 0f)
        {
            Rigidbody2D Rb = collision.collider.GetComponent<Rigidbody2D>();
            if (Rb != null)
            {
                Vector2 Velocity = Rb.velocity;
                Velocity.y = jumpForce;
                Rb.velocity = Velocity;
                //PlayerControl.numberOfDash = PlayerControl.maxNumberOfJumps;
            }
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {

        if (collision.relativeVelocity.y <= 0f)
        {
            Rigidbody2D Rb = collision.collider.GetComponent<Rigidbody2D>();
            if (Rb != null)
            {
                Vector2 Velocity = Rb.velocity;
                Velocity.y = jumpForce;
                Rb.velocity = Velocity;
                //PlayerControl.numberOfDash = PlayerControl.maxNumberOfJumps;
            }
        }
    }
    private void Update()
    {
        if (transform.position.y < cam.transform.position.y - deadLine)
        {
           Destroy(gameObject);
        }
    }
    void PlayerJump()
    {

    }
}
