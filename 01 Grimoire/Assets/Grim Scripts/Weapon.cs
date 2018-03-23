using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {
    //Press a button to start coroutine, then change ammunition to max
	public float fireRate = 0;
	public float Damage = 10;
    public int maxAmmunition = 6;
    public int ammunition;
	public LayerMask whatToHit;

    public Transform BulletTrailPrefab;
    public Transform MuzzleFlashPrefab;
    float timeToSpawnEffect = 0;
    public float effectSpawnRate = 10;

	float timeToFire = 0;
	Transform firePoint;
    bool reloading = false;

    // Use this for initialization
    void Awake () {
        ammunition = maxAmmunition;
        firePoint = transform.Find ("FirePoint");
		if (firePoint == null) {
			Debug.LogError ("No firePoint? WHAT?!");
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (fireRate == 0 && ammunition > 0) {
			if (Input.GetButton ("Fire1")) {
                Shoot();
                
                
            }
		}
		else {
			if (Input.GetButton ("Fire1") && Time.time > timeToFire && ammunition > 0) {
				timeToFire = Time.time + 1/fireRate;
				Shoot();
			}
		}
        
        //should change to partialReload
        if (ammunition <= 0) {
            if (reloading == false) {
                reloading = true;
                fullReload();
            }
        } 

        //button check "R"
        //fullReload
        
         
	}

  
	
	void Shoot () {
		Vector2 mousePosition = new Vector2 (Camera.main.ScreenToWorldPoint (Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
		Vector2 firePointPosition = new Vector2 (firePoint.position.x, firePoint.position.y);
		RaycastHit2D hit = Physics2D.Raycast (firePointPosition, mousePosition-firePointPosition, 100, whatToHit);
       // ammunition = ammunition - 1;
        if (Time.time >= timeToSpawnEffect)
        {
            Effect();
            timeToSpawnEffect = Time.time + 1 / effectSpawnRate;
            ammunition = ammunition - 1;
        }
		Debug.DrawLine (firePointPosition, (mousePosition-firePointPosition)*100, Color.cyan);
		if (hit.collider != null) {
			Debug.DrawLine (firePointPosition, hit.point, Color.red);
			Debug.Log ("We hit " + hit.collider.name + " and did " + Damage + " damage.");
		}
	}
    void Effect() {
        Instantiate(BulletTrailPrefab, firePoint.position, firePoint.rotation);
        Transform clone = Instantiate(MuzzleFlashPrefab, firePoint.position, firePoint.rotation) as Transform;
        clone.parent = firePoint;
        float size = Random.Range(0.6f, 0.9f);
        clone.localScale = new Vector3(size, size, size);
        Destroy(clone.gameObject, 0.02f);

    }
   
   //reload full clip
   void fullReload(){
      StartCoroutine(fullReloadDelay());
      
    }
   IEnumerator fullReloadDelay() {
        yield return new WaitForSeconds(1.5f);
        Debug.Log("Reloading...");
        ammunition = maxAmmunition;
        reloading = false;
    }
    /*
    //reload 1 / delay when not shooting
    void partialReload() {
        StartCoroutine(partReloadDelay());
    }

    IEnumerator partReloadDelay() {
        yield return new WaitForSeconds(1.5f);
        Debug.Log("Reloading...");
        ammunition = ammunition + 1;
    }
    
       */
}
