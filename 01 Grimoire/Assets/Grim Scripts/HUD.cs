using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour {

    public Sprite[] BatterySprite;
    public Image BatteryUI;
    private Player player;

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    void Update() {
        BatteryUI.fillAmount = player.playerStats.Health * 0.1f;
    }

} 
