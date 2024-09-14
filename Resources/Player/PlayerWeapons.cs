using Godot;
using System;
using System.Collections.Generic;

//I'm taking advantage of partial classes to split things up a little
public partial class Player : CharacterBody3D
{
    //NOTE: Current limit for weapons is 5. We can change things up if required.
    private List<Weapon> inventory = new List<Weapon>();
    private int selectedWeapon = -1;

    public void InitializeWeapons()
    {
        inventory.Add(new PlaceholderWeapon(this));
    }

    public int AddWeapon(Weapon weapon)
    {
        inventory.Add(weapon);
        return inventory.Count-1;
    }

    public void RemoveWeapon(int i)
    {
        if(i < selectedWeapon)
        {
            selectedWeapon--; //When you delete weapons that are before the current one in the queue, shift the current one back so that no unexpected switches occur
        } else if(i == selectedWeapon)
        {
            selectedWeapon = -1;
        }
        inventory.RemoveAt(i);
    }

    public void ClearWeapons()
    {
        inventory.Clear();
        selectedWeapon = -1;
    }

    public void HandleWeaponInputs()
    {
        if(inventory.Count == 0)
        {
            return; //You got no weapons to use, so why bother?
        }
        if (selectedWeapon >= inventory.Count) //If some weapons get deleted for some reason
        {
            selectedWeapon = -1;
        }


        if (selectedWeapon == -1 && (Input.IsActionJustPressed("weapon_switch_weapon_up") || Input.IsActionJustPressed("weapon_switch_weapon_down")))
        {
            selectedWeapon = 0;
        }
        else if (Input.IsActionJustPressed("weapon_switch_weapon_up"))
        {
            GD.Print("Switched up");
            selectedWeapon++;
            if (selectedWeapon >= inventory.Count)
                selectedWeapon = 0;
        }
        else if (Input.IsActionJustPressed("weapon_switch_weapon_down"))
        {
            GD.Print("Switched down");
            selectedWeapon--;
            if (selectedWeapon < 0)
                selectedWeapon = inventory.Count-1;
        }
        if (selectedWeapon >= 0)
        {
            inventory[selectedWeapon].HandleActions();
        }
    }
}