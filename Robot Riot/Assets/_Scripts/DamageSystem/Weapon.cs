using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int slot;
    public int bonusDamage = 0;

    public PlayerController playerMove;

    public bool canShoot = true;
    public bool canTrap = true;
    public bool isContinousWeapon;
}
