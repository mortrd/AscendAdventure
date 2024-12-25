using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShoot : MonoBehaviour
{
    Camera cam;
    Vector3 mousePos;
    [SerializeField] SpriteRenderer playerGun;
    [SerializeField] Image playerGunIcon;
    [SerializeField] float shutgunHorizontalOffset;
    [SerializeField] float shutgunVerticalOffset;
    [SerializeField] Color[] bulletColors;
    [SerializeField] Color[] gunColors;
    [SerializeField] public static int Gun;
    [SerializeField] GameObject gunPoint;
    [SerializeField] GameObject bullet;
    [SerializeField] Slider gunSlider;
    [SerializeField] float gunAttackSpeed = 1f;
    [SerializeField] float gunTime;
    float pistolFireRate = 0.7f;
    float shutgunFireRate = 1.3f;
    float AkFireRate = 0.36f;

    private void Start()
    {
        Gun = 1;
        EquipPistol();
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        gunTime = gunAttackSpeed;
        gunSlider.maxValue = gunAttackSpeed;
        
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            EquipAK();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            EquipShutGun();
        }
        else if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            EquipPistol();
        }
        Aim();
        if(Gun == 1) //Pistol
        {
            if (gunTime < gunAttackSpeed)
            {
                gunTime += Time.deltaTime;
                gunSlider.value -= Time.deltaTime;
            }
            else if (Input.GetKey(KeyCode.Mouse0) && gunTime >= gunAttackSpeed)
            {
                ShootPistol();
                gunTime = 0;
                gunSlider.value = gunAttackSpeed;
            }
        }
        else if(Gun == 2) //ShutGun
        {
            if (gunTime < gunAttackSpeed)
            {
                gunTime += Time.deltaTime;
                gunSlider.value -= Time.deltaTime;
            }
            else if (Input.GetKey(KeyCode.Mouse0) && gunTime >= gunAttackSpeed)
            {
                ShootShutGun();
                gunTime = 0;
                gunSlider.value = gunAttackSpeed;
            }
        }
        else if (Gun == 3) //AK
        {
            if (gunTime < gunAttackSpeed)
            {
                gunTime += Time.deltaTime;
                gunSlider.value -= Time.deltaTime;
            }
            else if (Input.GetKey(KeyCode.Mouse0) && gunTime >= gunAttackSpeed)
            {
                ShootAK();
                gunTime = 0;
                gunSlider.value = gunAttackSpeed;
            }
        }

    }

    void Aim()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 rotation = mousePos - transform.position;
        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotZ);
    }
    void ShootPistol()
    {
        GameObject bull = Instantiate(bullet, gunPoint.transform.position, Quaternion.identity);
        SpriteRenderer image = bull.GetComponent<SpriteRenderer>();
        image.color = bulletColors[0];
    }
    void ShootShutGun()
    {
        for (int i = 0; i < 5 ; i++)
        {
            GameObject bull = Instantiate(bullet,new Vector2(Random.Range(gunPoint.transform.position.x,gunPoint.transform.position.x+shutgunHorizontalOffset),Random.Range(gunPoint.transform.position.y + shutgunVerticalOffset, gunPoint.transform.position.y - shutgunVerticalOffset)), Quaternion.identity);
            SpriteRenderer image = bull.GetComponent<SpriteRenderer>();
            image.color = bulletColors[1];
        }
        
    }
    void ShootAK()
    {
        GameObject bull = Instantiate(bullet, gunPoint.transform.position, Quaternion.identity);
        SpriteRenderer image = bull.GetComponent<SpriteRenderer>();
        image.color = bulletColors[2];
    }
    void EquipAK()
    {
        gunAttackSpeed = AkFireRate;
        gunSlider.maxValue = gunAttackSpeed;
        BulletScript.BulletRage = 0.8f;
        BulletScript.BulletDamage = BulletScript.AKDamage;
        playerGun.color = gunColors[2];
        playerGunIcon.color = gunColors[2];
        Gun = 3;
    }
    void EquipShutGun()
    {
        gunAttackSpeed = shutgunFireRate;
        gunSlider.maxValue = gunAttackSpeed;
        BulletScript.BulletRage = 0.32f;
        BulletScript.BulletDamage = BulletScript.ShutgunDamage;
        playerGun.color = gunColors[1];
        playerGunIcon.color = gunColors[1];
        Gun = 2;
    }
    void EquipPistol()
    {
        gunAttackSpeed = pistolFireRate;
        gunSlider.maxValue = gunAttackSpeed;
        BulletScript.BulletRage = 0.8f;
        BulletScript.BulletDamage = BulletScript.PistolDamage;
        playerGun.color = gunColors[0];
        playerGunIcon.color = gunColors[0];
        Gun = 1;
    }
}
