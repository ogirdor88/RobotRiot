using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon Name", menuName = "Weapon", order = 0)]
public class Weapons : ScriptableObject
{
    public float fireRate;
    public int damage;
    public float maxDistance;
    [Tooltip("Gets Multiplied by 100")]
    public float prjectileSpeed;
}
