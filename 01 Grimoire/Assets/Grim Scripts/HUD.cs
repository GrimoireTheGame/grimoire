using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour {

   
    public Image BatteryUI;
    public Transform AmmoUI;
    private Player player;
    private Weapon weapon;


    void Start() {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        weapon = GameObject.FindGameObjectWithTag("Weapon").GetComponent<Weapon>();
    }

    void Update() {
        //Works but has bugs
        //The HUD will not work if the player dies, 
        //and I'm guessign it won't work if the player changes weapons.
            //The code needs to check each instance of the player and the weapon,
            //because rn, it only checks at the beginning of the game 
            //so if the player dies, it doesn't reset the player and weapon variables
        BatteryUI.fillAmount = player.playerStats.Health * 0.1f;
        AmmoUI.GetComponent<Text>().text = weapon.ammunition.ToString() + " / " + weapon.maxAmmunition.ToString(); 
    }

} 
