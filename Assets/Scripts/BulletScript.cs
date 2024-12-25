using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class BulletScript : MonoBehaviour
{
    public static float PistolDamage = 5f;
    public static float ShutgunDamage = 2.2f;
    public static float AKDamage = 3f;
    [SerializeField] float shutgunSpreadOffset;
    public static float BulletDamage = 5f;
    public static float BulletRage;
    Camera cam;
    Rigidbody2D rb;
    Vector3 mousePos;
    [SerializeField] float force = 5f;
    int Layer1 = 9;
    int Layer2 = 8;

    private void Start()
    {

        Destroy(gameObject, BulletRage);
        Physics2D.IgnoreLayerCollision(Layer1, Layer2);
        Destroy(gameObject, 1.5f);
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        rb = GetComponent<Rigidbody2D>();
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        if(PlayerShoot.Gun == 1 || PlayerShoot.Gun == 3)
        {
            LookAtMouseDirection();
            PistolBulletMovement();
        }
        if (PlayerShoot.Gun == 2)
        {
            LookAtMouseDirection();
            ShutGunBulletMovement();
        }

    }
    private void Update()
    {
        if(transform.position.y > cam.transform.position.y + 5.5f)
        {
            BulletImpact();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == ("Enemy"))
        {
            BulletImpact(); 
        }
    }

    private void LookAtMouseDirection()
    {
        if(PlayerShoot.Gun == 1 || PlayerShoot.Gun == 3)
        {
            Vector3 rotation = transform.position - mousePos;
            float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, rot);
        }
        else if (PlayerShoot.Gun == 2)
        {
            Vector3 rotation = transform.position - mousePos;
            float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, Random.Range(rot - shutgunSpreadOffset, rot + shutgunSpreadOffset));
        }
        
    }
    void PistolBulletMovement()
    {
        Vector3 direction = mousePos - transform.position;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * force;
    }
    void ShutGunBulletMovement()
    {
        Vector3 direction = new Vector3(Random.Range(mousePos.x - shutgunSpreadOffset,mousePos.x+shutgunSpreadOffset),Random.Range(mousePos.y-shutgunSpreadOffset,mousePos.y+shutgunSpreadOffset),mousePos.z) - transform.position;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * force;
    }
    void BulletImpact()
    {
        Destroy(gameObject);
    }

}
