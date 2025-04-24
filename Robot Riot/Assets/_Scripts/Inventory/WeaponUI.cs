using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEditor;
using Unity.VisualScripting;
using System.IO;

public class WeaponUI : MonoBehaviour
{
    [SerializeField] private RawImage weaponIcon;
    [SerializeField] private RawImage nextWeaponIcon1;
    [SerializeField] private RawImage nextWeaponIcon2;
    [SerializeField] private RawImage prevWeaponIcon1;
    [SerializeField] private RawImage prevWeaponIcon2;
    [SerializeField] private TMP_Text weaponName;
    [SerializeField] private TMP_Text weaponDesc;
    [SerializeField] private TMP_Text weaponAmmo;

    public void UpdateWeapon()
    {
        Texture2D weaponTexture;
        GameObject currentWeapon = null;
        GameObject nextWeapon1 = null;
        GameObject nextWeapon2 = null;
        GameObject prevWeapon1 = null;
        GameObject prevWeapon2 = null;

        // Set up each object
        if (transform.parent.parent.GetComponent<InventoryManager>().inventory[transform.parent.parent.GetComponent<InventoryManager>().activeSlot] != null)
            currentWeapon = transform.parent.parent.GetComponent<InventoryManager>().inventory[transform.parent.parent.GetComponent<InventoryManager>().activeSlot];
        if (transform.parent.parent.GetComponent<InventoryManager>().activeSlot + 1 < transform.parent.parent.GetComponent<InventoryManager>().inventory.Length)
            nextWeapon1 = transform.parent.parent.GetComponent<InventoryManager>().inventory[transform.parent.parent.GetComponent<InventoryManager>().activeSlot + 1];
        if (transform.parent.parent.GetComponent<InventoryManager>().activeSlot + 2 < transform.parent.parent.GetComponent<InventoryManager>().inventory.Length)
            nextWeapon2 = transform.parent.parent.GetComponent<InventoryManager>().inventory[transform.parent.parent.GetComponent<InventoryManager>().activeSlot + 2];
        if (transform.parent.parent.GetComponent<InventoryManager>().activeSlot - 1 >= 0)
            prevWeapon1 = transform.parent.parent.GetComponent<InventoryManager>().inventory[transform.parent.parent.GetComponent<InventoryManager>().activeSlot - 1];
        if (transform.parent.parent.GetComponent<InventoryManager>().activeSlot - 2 >= 0)
            prevWeapon2 = transform.parent.parent.GetComponent<InventoryManager>().inventory[transform.parent.parent.GetComponent<InventoryManager>().activeSlot - 2];

        nextWeaponIcon1.GetComponent<RawImage>().texture = null;
        nextWeaponIcon2.GetComponent<RawImage>().texture = null;
        prevWeaponIcon1.GetComponent<RawImage>().texture = null;
        prevWeaponIcon2.GetComponent<RawImage>().texture = null;

        // Assign textures
        if (currentWeapon != null)
        {
            //weaponTexture = AssetPreview.GetAssetPreview(currentWeapon);
            weaponTexture = currentWeapon.GetComponent<Weapon>().weapon.weaponTexure;
            weaponIcon.GetComponent<RawImage>().texture = weaponTexture;
            weaponIcon.color = new Color(weaponIcon.color.r, weaponIcon.color.g, weaponIcon.color.b, 100);
        }
        else
        {
            weaponIcon.color = new Color(weaponIcon.color.r, weaponIcon.color.g, weaponIcon.color.b, 0);
        }
        if (nextWeapon1 != null)
        {
            //weaponTexture = AssetPreview.GetAssetPreview(nextWeapon1);
            weaponTexture = nextWeapon1.GetComponent<Weapon>().weapon.weaponTexure;
            nextWeaponIcon1.GetComponent<RawImage>().texture = weaponTexture;
            nextWeaponIcon1.color = new Color(nextWeaponIcon1.color.r, nextWeaponIcon1.color.g, nextWeaponIcon1.color.b, 100);
        }
        else
        {
            nextWeaponIcon1.color = new Color(nextWeaponIcon1.color.r, nextWeaponIcon1.color.g, nextWeaponIcon1.color.b, 0);
        }
        if (nextWeapon2 != null)
        {
            //weaponTexture = AssetPreview.GetAssetPreview(nextWeapon2);
            weaponTexture = nextWeapon2.GetComponent<Weapon>().weapon.weaponTexure;
            nextWeaponIcon2.GetComponent<RawImage>().texture = weaponTexture;
            nextWeaponIcon2.color = new Color(nextWeaponIcon2.color.r, nextWeaponIcon2.color.g, nextWeaponIcon2.color.b, 100);
        }
        else
        {
            nextWeaponIcon2.color = new Color(nextWeaponIcon2.color.r, nextWeaponIcon2.color.g, nextWeaponIcon2.color.b, 0);
            //Debug.Log("transparent");
        }
        if (prevWeapon1 != null)
        {
            //weaponTexture = AssetPreview.GetAssetPreview(prevWeapon1);
            weaponTexture = prevWeapon1.GetComponent<Weapon>().weapon.weaponTexure;
            prevWeaponIcon1.GetComponent<RawImage>().texture = weaponTexture;
            prevWeaponIcon1.color = new Color(prevWeaponIcon1.color.r, prevWeaponIcon1.color.g, prevWeaponIcon1.color.b, 100);
        }
        else
        {
            prevWeaponIcon1.color = new Color(prevWeaponIcon1.color.r, prevWeaponIcon1.color.g, prevWeaponIcon1.color.b, 0);
        }
        if (prevWeapon2 != null)
        {
            //weaponTexture = AssetPreview.GetAssetPreview(prevWeapon2);
            weaponTexture = prevWeapon2.GetComponent<Weapon>().weapon.weaponTexure;
            prevWeaponIcon2.GetComponent<RawImage>().texture = weaponTexture;
            prevWeaponIcon2.color = new Color(prevWeaponIcon2.color.r, prevWeaponIcon2.color.g, prevWeaponIcon2.color.b, 100);
        }
        else
        {
            prevWeaponIcon2.color = new Color(prevWeaponIcon2.color.r, prevWeaponIcon2.color.g, prevWeaponIcon2.color.b, 0);
        }
        if (weaponName != null)
        {
            //weaponName.text = Path.GetFileNameWithoutExtension(AssetDatabase.GetAssetPath(currentWeapon.GetComponent<Weapon>().weapon));
            weaponName.text = currentWeapon.GetComponent<Weapon>().weapon.weaponName;
        }
        if (weaponDesc != null)
        {
            weaponDesc.text = "DMG: " + currentWeapon.GetComponent<Weapon>().weapon.damage.ToString();
        }
        if (weaponAmmo != null)
        {
            weaponAmmo.text = "AMMO: " + currentWeapon.GetComponent<Weapon>().remainingAmmo.ToString();
        }
    }
}