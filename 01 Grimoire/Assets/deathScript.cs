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

    public void DamageEnemy(int damage)
    {
        health -= damage;
        if(health <= 0)
        {
            Destroy(gameObject);//kill the enemy
        }
        else
        {
            healthBar.fillAmount = health * 0.3333f;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        MoveTrail _bullet = collision.collider.GetComponent<MoveTrail>();

        if(_bullet != null)
        {
            DamageEnemy(_bullet.getDamage);            
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        MoveTrail _bullet = other.GetComponent<MoveTrail>();

        if (_bullet != null)
        {
            DamageEnemy(_bullet.getDamage);
        }
    }

}
