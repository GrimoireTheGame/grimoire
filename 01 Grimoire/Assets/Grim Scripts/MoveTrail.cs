using UnityEngine;
using System.Collections;

public class MoveTrail : MonoBehaviour {

    public int moveSpeed = 230;

    [SerializeField]
    private int damage = 1;
    
    public int getDamage
    {
        get{ return damage; }
    }



	// Update is called once per frame
	void Update () {
		transform.Translate (Vector3.right * Time.deltaTime * moveSpeed);
		Destroy (gameObject, 1);
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            
            Destroy(gameObject);
        
        }
    }
}
