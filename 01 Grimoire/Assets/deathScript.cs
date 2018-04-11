using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Look at the tutorials located at https://www.youtube.com/watch?v=YUFP0WbTK4w
//Health HUD and Player Health tutorials https://unity3d.com/learn/tutorials/projects/survival-shooter/player-health

public class deathScript : MonoBehaviour
{

    //declaration of healthBar above the enemy's head
    public Image healthBar;

    //temporary variable, only here for testing
    public int health = 10;

    //setting the health bar
    private void Start()
    {

        healthBar.fillAmount = health*0.3f;
    }
    void Update()
    {
        if (health >= 0)
        {
            healthBar.fillAmount = health * 0.3f;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {
            if(health == 1)//if this bullet will kill the enemy
            {
                Destroy(gameObject);//kill the enemy
            }
            else
            {
                health--;//decrease the enemy health

                //currently this is how the health bar decreases, does not currently work properly
                //needs to be looked into and adjusted
                //healthBar.sizeDelta = new Vector2(healthBar.sizeDelta.x-25, healthBar.sizeDelta.y);
                healthBar.fillAmount = health * 0.3333f;
            }
            
        }
    }



}
