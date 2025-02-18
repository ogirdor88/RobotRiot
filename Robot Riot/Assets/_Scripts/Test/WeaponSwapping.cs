using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Placeholder weapon swapping system, uses unused reloading button.
public class WeaponSwapping : MonoBehaviour
{
    [SerializeField] GameObject laserGun;
    [SerializeField] GameObject balloonSword;

    private bool holdingGun = true;

    public void SwapWeapon()
    {
        if (holdingGun)
        {
            laserGun.SetActive(false);
            balloonSword.SetActive(true);
            laserGun.GetComponent<LaserGun>().canShoot = true;
            balloonSword.GetComponent<BallonSword>().canShoot = true;
            holdingGun = false;
        }
        else
        {
            balloonSword.SetActive(false);
            laserGun.SetActive(true);
            laserGun.GetComponent<LaserGun>().canShoot = true;
            balloonSword.GetComponent<BallonSword>().canShoot = true;
            holdingGun = true;
        }
    }
}
