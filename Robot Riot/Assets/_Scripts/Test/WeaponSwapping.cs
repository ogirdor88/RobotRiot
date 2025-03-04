using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

// Placeholder weapon swapping system, uses unused reloading button.
public class WeaponSwapping : MonoBehaviour
{
    [SerializeField] GameObject laserGun;
    [SerializeField] GameObject balloonSword;
    [SerializeField] GameObject cannon;
    [SerializeField] GameObject boomBox;

    //private bool holdingGun = true;
    private int selectedWeapon = 1;

    public void SwapWeapon(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            if (selectedWeapon == 4)
                selectedWeapon = 1;
            else
                selectedWeapon++;
            switch (selectedWeapon)
            {
                case 4:
                    boomBox.SetActive(false);
                    cannon.SetActive(true);
                    boomBox.GetComponent<LaserGun>().canShoot = true;
                    cannon.GetComponent<Cannon>().canShoot = true;
                    break;
                case 3:
                    laserGun.SetActive(false);
                    boomBox.SetActive(true);
                    laserGun.GetComponent<LaserGun>().canShoot = true;
                    boomBox.GetComponent<LaserGun>().canShoot = true;
                    break;
                case 2:
                    for (int i = 0; i < balloonSword.gameObject.transform.childCount; i++)
                    {
                        GameObject balloonSwordObject = balloonSword.gameObject.transform.GetChild(i).gameObject;
                        balloonSwordObject.SetActive(false);
                    }
                    laserGun.SetActive(true);
                    laserGun.GetComponent<LaserGun>().canShoot = true;
                    for (int i = 0; i < balloonSword.gameObject.transform.childCount; i++)
                    {
                        GameObject balloonSwordObject = balloonSword.gameObject.transform.GetChild(i).gameObject;
                        balloonSwordObject.GetComponent<BallonSword>().canShoot = true;
                    }
                    break;
                case 1:
                    cannon.SetActive(false);
                    for (int i = 0; i < balloonSword.gameObject.transform.childCount; i++)
                    {
                        GameObject balloonSwordObject = balloonSword.gameObject.transform.GetChild(i).gameObject;
                        balloonSwordObject.SetActive(true);
                    }
                    cannon.GetComponent<Cannon>().canShoot = true;
                    for (int i = 0; i < balloonSword.gameObject.transform.childCount; i++)
                    {
                        GameObject balloonSwordObject = balloonSword.gameObject.transform.GetChild(i).gameObject;
                        balloonSwordObject.GetComponent<BallonSword>().canShoot = true;
                    }
                    break;
                default:
                    Debug.Log("This shouldn't be possible.");
                    break;
            }
        }
    }
}
