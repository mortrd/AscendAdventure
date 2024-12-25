using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseGuns : MonoBehaviour
{
    public string gunName;
    public float damage;
    public int maxAmmo;
    public bool isReloading;
    public int currentAmmoCount;
    public float fireRate;

    public virtual void Shooting()
    {
        if(currentAmmoCount > 0)
        {
            currentAmmoCount--;
        }
        else
        {
            //Do Something
        }
        
    }
    public virtual void FinishReload()
    {
        currentAmmoCount = maxAmmo;
        isReloading = false;
    }

    public virtual void Reloading(float relaodTime)
    {
        if(!isReloading)
        {
            isReloading=true;
            //GunReloadAnimation
            Invoke(nameof(FinishReload),relaodTime);

        }
    }

}
