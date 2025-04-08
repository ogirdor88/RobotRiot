using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.InputSystem;
using static Unity.VisualScripting.Member;

public class InventoryManager : MonoBehaviour
{
    public GameObject[] inventory;

    private Weapons currentWeapon;

    public int activeSlot;

    [SerializeField] private GameObject weaponLocation;

    [SerializeField] private GameObject inventoryUI;

    private void Awake()
    {
        //inventory = new GameObject[5];
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Item" && other.GetComponent<Weapon>())
        {
            //Instantiate(other.)
        }
        //if you run into a weapon
        else if (other.gameObject.tag == "Weapon" && other.GetComponent<Weapon>() && !other.GetComponent<Weapon>().playerMove)
        {
            Debug.Log("Got Weapon");
            Debug.Log("Weapon slot is: " + other.GetComponent<Weapon>().slot);
                    other.gameObject.transform.position = weaponLocation.transform.position;
                    other.gameObject.transform.rotation = weaponLocation.transform.parent.transform.rotation * other.transform.rotation;
                    other.gameObject.transform.parent = weaponLocation.transform.parent;
                //turn off current weapon or trap
                if (inventory[activeSlot])
                {
                    inventory[activeSlot].SetActive(false);
                    inventory[activeSlot].GetComponent<Weapon>().canShoot = true;
                }
                //change the current slot to the weapon you just picked up
                activeSlot = other.GetComponent<Weapon>().slot;
                /*
                if (activeSlot != other.GetComponent<Weapon>().slot)
                    other.gameObject.SetActive(false);
                */

                // this prevents you from taking the other players weapons
                if (!other.gameObject.GetComponent<Weapon>().playerMove)
                {
                    inventory[other.GetComponent<Weapon>().slot] = other.gameObject;
                    other.gameObject.GetComponent<Weapon>().playerMove = gameObject.GetComponent<PlayerController>();
                    inventoryUI.GetComponent<WeaponUI>().UpdateWeapon();
                }
        }
    }

    // Direction = 1 is right
    // Direction = 0 is left
    public void ChangeWeaponLeft(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
            ChangeWeapon(context, false);
    }
    public void ChangeWeaponRight(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
            ChangeWeapon(context, true);
    }

    // Code added for balloon sword giver
    public void ForceAddWeapon(GameObject weapon)
    {
        weapon.gameObject.transform.position = weaponLocation.transform.position;
        weapon.gameObject.transform.rotation = weaponLocation.transform.parent.transform.rotation * weaponLocation.transform.rotation;
        weapon.gameObject.transform.parent = weaponLocation.transform.parent;

        if (inventory[activeSlot])
        {
            inventory[activeSlot].SetActive(false);
            inventory[activeSlot].GetComponent<Weapon>().canShoot = true;
        }
        activeSlot = weapon.GetComponent<Weapon>().slot;
        if (!weapon.gameObject.GetComponent<Weapon>().playerMove)
        {
            inventory[weapon.GetComponent<Weapon>().slot] = weapon.gameObject;
            weapon.gameObject.GetComponent<Weapon>().playerMove = gameObject.GetComponent<PlayerController>();
            inventoryUI.GetComponent<WeaponUI>().UpdateWeapon();
        }
    }

    private void ChangeWeapon(InputAction.CallbackContext context, bool direction)
    {
        int slotchange;
        int oldSlot;

        bool shouldLoop = false;
        bool checkForSlot = false;
        bool foundSlot = false;

        oldSlot = activeSlot;

        Debug.Log("Changing Weapon");

        if (direction)
            slotchange = 1;
        else
            slotchange = -1;

        Debug.Log("We're currently " + oldSlot + " wanting to change to " + (activeSlot + slotchange));

        // Slot is invalid, which means we need to loop around.
        if ((activeSlot + slotchange) < 0 || (activeSlot + slotchange) > inventory.Length - 1)
            shouldLoop = true;
        else if (inventory[activeSlot + slotchange] == null)
        {
            shouldLoop = true;
            checkForSlot = true;
        }

        // Check if the next slot is open, if not loop around
        if (context.phase == InputActionPhase.Performed)
        {
            if (checkForSlot)
            {
                int slot;
                int slotGoal;

                slot = activeSlot;
                if (direction)
                {
                    slotGoal = inventory.Length;
                }
                else
                {
                    slotGoal = -1;
                }
                for (; slot != slotGoal; slot += slotchange)
                {
                    Debug.Log("Next Open Slot Test is " + slot + " with the goal of "  + slotGoal);
                    if (inventory[slot] != null && slot != oldSlot && gameObject.GetComponent<PlayerController>().botMode == inventory[slot].GetComponent<Weapon>().canTrap)
                    {
                        //Debug.Log("Available Slot:" + slot + inventory[activeSlot].gameObject);
                        Debug.Log("The slot I found is " + slot);
                        activeSlot = slot;
                        break;
                    }
                    else if (inventory[slot] != null && slot != oldSlot && !gameObject.GetComponent<PlayerController>().botMode == !inventory[slot].GetComponent<Weapon>().canTrap)
                    {
                        //Debug.Log("Available Slot:" + slot + inventory[activeSlot].gameObject);
                        activeSlot = slot;
                        break;
                    }
                }
                Debug.Log("Final Slot Test was " + slot);
                if (oldSlot != activeSlot && inventory[activeSlot] && gameObject.GetComponent<PlayerController>().botMode == inventory[activeSlot].GetComponent<Weapon>().canTrap)
                {
                    Debug.Log("Found available slot");
                    foundSlot = true;
                    if (inventory[oldSlot])
                    {
                        inventory[oldSlot].gameObject.SetActive(false);
                        inventory[oldSlot].GetComponent<Weapon>().canShoot = true;
                    }
                    Debug.Log("This is number 3");
                    inventory[activeSlot].gameObject.SetActive(true);
                    inventory[activeSlot].transform.parent = weaponLocation.transform.parent;
                }
            }
            if (shouldLoop && !foundSlot)
            {
                int slot;
                int slotGoal;

                if (direction)
                {
                    slot = 0;
                    slotGoal = inventory.Length;
                }
                else
                {
                    slot = inventory.Length - 1;
                    slotGoal = -1;
                }
                for (; slot != slotGoal; slot += slotchange)
                {
                    Debug.Log("Current Slot Test is " + slot);
                    if (inventory[slot] != null && gameObject.GetComponent<PlayerController>().botMode == inventory[slot].GetComponent<Weapon>().canTrap)
                    {
                        //Debug.Log("Available Slot:" + slot + inventory[activeSlot].gameObject);
                        activeSlot = slot;
                        break;
                    }
                    else if (inventory[slot] != null && !gameObject.GetComponent<PlayerController>().botMode == !inventory[slot].GetComponent<Weapon>().canTrap)
                    {
                        //Debug.Log("Available Slot:" + slot + inventory[activeSlot].gameObject);
                        activeSlot = slot;
                        break;
                    }
                    // We completely looped, there's no other slots.
                    //else if (slot == activeSlot)
                    //break;
                }
                Debug.Log("Final Slot Test was " + slot);
                if (oldSlot != activeSlot && inventory[activeSlot] && gameObject.GetComponent<PlayerController>().botMode == inventory[activeSlot].GetComponent<Weapon>().canTrap)
                {
                    Debug.Log("Setting " + activeSlot + " active");
                    if (inventory[oldSlot])
                    {
                        inventory[oldSlot].gameObject.SetActive(false);
                        inventory[oldSlot].GetComponent<Weapon>().canShoot = true;
                    }
                    Debug.Log("This is number 1");
                    inventory[activeSlot].gameObject.SetActive(true);
                    inventory[activeSlot].transform.parent = weaponLocation.transform.parent;
                }
            }
            else if ((activeSlot + slotchange) >= 0 && (activeSlot + slotchange) <= inventory.Length - 1)
            { 
                if (inventory[activeSlot + slotchange] && !foundSlot && gameObject.GetComponent<PlayerController>().botMode == inventory[activeSlot + slotchange].GetComponent<Weapon>().canTrap)
                {
                    //Debug.Log("Slot ahead: " + (activeSlot += slotchange));
                    inventory[activeSlot].gameObject.SetActive(false);
                    inventory[activeSlot].GetComponent<Weapon>().canShoot = true;
                    activeSlot += slotchange;
                    Debug.Log("Setting " + activeSlot + " active");
                    Debug.Log("This is number 2");
                    inventory[activeSlot].gameObject.SetActive(true);
                }
            }
            inventoryUI.GetComponent<WeaponUI>().UpdateWeapon();
        }
    }
    public void SwapSlot(bool direction)
    {
        int slotchange;
        int oldSlot;

        bool shouldLoop = false;
        bool checkForSlot = false;
        bool foundSlot = false;

        oldSlot = activeSlot;

        Debug.Log("Changing Weapon");

        if (direction)
            slotchange = 1;
        else
            slotchange = -1;

        Debug.Log("We're currently " + oldSlot + " wanting to change to " + (activeSlot + slotchange));

        // Slot is invalid, which means we need to loop around.
        if ((activeSlot + slotchange) < 0 || (activeSlot + slotchange) > inventory.Length - 1)
        {
            if(gameObject.GetComponent<PlayerController>().botMode == inventory[activeSlot + slotchange].GetComponent<Weapon>().canTrap)
            shouldLoop = true;
        }
           
        else if (inventory[activeSlot + slotchange] == null)
        {
                shouldLoop = true;
                checkForSlot = true;
        }
        if (inventory[activeSlot] != null)
            inventory[activeSlot].SetActive(false);
        // Check if the next slot is open, if not loop around
        if (checkForSlot)
        {
            int slot;
            int slotGoal;

            slot = activeSlot;
            if (direction)
            {
                slotGoal = inventory.Length;
            }
            else
            {
                slotGoal = -1;
            }
            for (; slot != slotGoal; slot += slotchange)
            {
                Debug.Log("Next Open Slot Test is " + slot + " with the goal of " + slotGoal);
                if (inventory[slot] != null && slot != oldSlot && gameObject.GetComponent<PlayerController>().botMode == inventory[slot].GetComponent<Weapon>().canTrap)
                {
                    //Debug.Log("Available Slot:" + slot + inventory[activeSlot].gameObject);
                    Debug.Log("The slot I found is " + slot);
                    activeSlot = slot;
                    break;
                }
                else if (inventory[slot] != null && slot != oldSlot && !gameObject.GetComponent<PlayerController>().botMode == !inventory[slot].GetComponent<Weapon>().canTrap)
                {
                    //Debug.Log("Available Slot:" + slot + inventory[activeSlot].gameObject);
                    activeSlot = slot;
                    break;
                }
            }
            Debug.Log("Final Slot Test was " + slot);
            if (oldSlot != activeSlot && inventory[activeSlot] && gameObject.GetComponent<PlayerController>().botMode == inventory[activeSlot].GetComponent<Weapon>().canTrap)
            {
                Debug.Log("Found available slot");
                foundSlot = true;
                if (inventory[oldSlot])
                {
                    inventory[oldSlot].gameObject.SetActive(false);
                    inventory[oldSlot].GetComponent<Weapon>().canShoot = true;
                }
                Debug.Log("This is number 3");
                inventory[activeSlot].gameObject.SetActive(true);
                inventory[activeSlot].transform.parent = weaponLocation.transform.parent;
            }
            else
            {
                Debug.Log("Nothing found");
            }
        }
        if (shouldLoop && !foundSlot)
        {
            int slot;
            int slotGoal;

            if (direction)
            {
                slot = 0;
                slotGoal = inventory.Length;
            }
            else
            {
                slot = inventory.Length - 1;
                slotGoal = -1;
            }
            for (; slot != slotGoal; slot += slotchange)
            {
                Debug.Log("Current Slot Test is " + slot);
                if (inventory[slot] != null && gameObject.GetComponent<PlayerController>().botMode == inventory[slot].GetComponent<Weapon>().canTrap)
                {
                    //Debug.Log("Available Slot:" + slot + inventory[activeSlot].gameObject);
                    activeSlot = slot;
                    break;
                }
                else if (inventory[slot] != null && !gameObject.GetComponent<PlayerController>().botMode == !inventory[slot].GetComponent<Weapon>().canTrap)
                {
                    //Debug.Log("Available Slot:" + slot + inventory[activeSlot].gameObject);
                    activeSlot = slot;
                    break;
                }
                // We completely looped, there's no other slots.
                //else if (slot == activeSlot)
                //break;
            }
            Debug.Log("Final Slot Test was " + slot);
            if (oldSlot != activeSlot && inventory[activeSlot] && gameObject.GetComponent<PlayerController>().botMode == inventory[activeSlot].GetComponent<Weapon>().canTrap)
            {
                Debug.Log("Setting " + activeSlot + " active");
                if (inventory[oldSlot])
                {
                    inventory[oldSlot].gameObject.SetActive(false);
                    inventory[oldSlot].GetComponent<Weapon>().canShoot = true;
                }
                Debug.Log("This is number 1");
                inventory[activeSlot].gameObject.SetActive(true);
                inventory[activeSlot].transform.parent = weaponLocation.transform.parent;
            }
        }

        else if ((activeSlot + slotchange) >= 0 && (activeSlot + slotchange) <= inventory.Length - 1)
        {
            if (inventory[activeSlot + slotchange] && !foundSlot && gameObject.GetComponent<PlayerController>().botMode == inventory[activeSlot + slotchange].GetComponent<Weapon>().canTrap)
            {
                //Debug.Log("Slot ahead: " + (activeSlot += slotchange));
                inventory[activeSlot].gameObject.SetActive(false);
                inventory[activeSlot].GetComponent<Weapon>().canShoot = true;
                activeSlot += slotchange;
                Debug.Log("Setting " + activeSlot + " active");
                Debug.Log("This is number 2");
                inventory[activeSlot].gameObject.SetActive(true);
            }
        }
        inventoryUI.GetComponent<WeaponUI>().UpdateWeapon();
    }
}
