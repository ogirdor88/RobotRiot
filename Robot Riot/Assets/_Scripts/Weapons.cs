using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon Name", menuName = "Weapon", order = 0)]
public class Weapons : ScriptableObject
{
    public int weaponDmage;
    public int bleedDamge;
    public int splashDamge;
}
