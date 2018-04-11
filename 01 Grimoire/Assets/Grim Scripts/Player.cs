using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	[System.Serializable]
	public class PlayerStats {
        //Batteries hold Shields in sets of 10
        //Health is the number of hits you can take before you die
            //Note: Shields block Health
        
        //Maximum number of Batteries overall
        public int maxBattery = 5;
        //Number of batteries you start with at start of game
        public int currentMaxBattery = 1;
        public int currentBattery = 1;
        //Current Shields per battery
        public int currentShields = 10;

        public int Health = 1;

	}

	public PlayerStats playerStats = new PlayerStats();

	public int fallBoundary = -20;

    

    void Update()
    {
        if (transform.position.y <= fallBoundary)
        {
            DamagePlayer(9999999);
        }
        
    }



    //I guess the way that this is set up...
    //The player can withstand any amount of damage per shield
    public void DamageCurrentShields(int damage)
    {
        playerStats.currentShields -= damage;
        if (playerStats.currentShields <= 0)
        {
            GameMaster.DecreaseCurrentBattery(this);
        }
    }
	public void DamagePlayer (int damage) {
		playerStats.Health -= damage;
		if (playerStats.Health <= 0) {
			GameMaster.KillPlayer(this);
            //if Damage == 10 or something, GameMaster.WoundPlayer(this);
		}
	}

}
