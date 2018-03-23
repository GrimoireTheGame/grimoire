using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour {
   
    public Image[] BatteryUI;
    public Image[] BatteryUIBG;
    public Transform AmmoUI;
    private Player player;
    private Weapon weapon;


    void Start() {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        weapon = GameObject.FindGameObjectWithTag("Weapon").GetComponent<Weapon>();
        //This for loop looks through each index of all the batteries available (5) even if the player doesn't have access to all those batteries yet
        for (int i = 0; i < player.playerStats.maxBattery; i++)
        {
            //Checks the current index is less than how many batteries the player has and draws it
            if (i < player.playerStats.currentBattery)
            {
                BatteryUI[i].enabled = true;
                BatteryUIBG[i].enabled = true;
            }
            else
            {
                //If the player does not have that many batteries yet, don't draw it
                BatteryUI[i].enabled = false;
                BatteryUIBG[i].enabled = false;
            }
        }
    }

    void Update() {
        //Works but has bugs
        //The HUD will not work if the player dies, 
        //and I'm guessign it won't work if the player changes weapons.
        //The code needs to check each instance of the player and the weapon,
        //because rn, it only checks at the beginning of the game 
        //so if the player dies, it doesn't reset the player and weapon variables


        for (int i = 0; i < player.playerStats.maxBattery; i++)
        {
            //Checks the current index is less than how many batteries the player has and draws it
            if (i < player.playerStats.currentBattery)
            {
                BatteryUI[i].enabled = true;
                BatteryUIBG[i].enabled = true;
            }
            else
            {
                //If the player does not have that many batteries yet, don't draw it
                BatteryUI[i].enabled = false;
               // BatteryUIBG[i].enabled = false;
            }
        }

        //This updates the shields per battery on the 
        if (player.playerStats.currentBattery > 0)
        {
            BatteryUI[player.playerStats.currentBattery - 1].fillAmount = player.playerStats.currentShields * 0.1f;
        }
        //This updates the ammo displayed on the screen
            //displayed as " current ammo / max ammo" 
        AmmoUI.GetComponent<Text>().text = weapon.ammunition.ToString() + " / " + weapon.maxAmmunition.ToString(); 
    }

} 
