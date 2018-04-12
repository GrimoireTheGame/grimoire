using System.Collections;
using UnityEngine;
using Pathfinding;

[RequireComponent (typeof (Rigidbody2D))]
[RequireComponent(typeof (Seeker))]

public class BasicFlyingAI : MonoBehaviour {

    //What to chase?
    public Transform target;

    //How many times each second we will update our path
    public float updateRate = 2f;

    private Seeker seeker;
    private Rigidbody2D rb;

    //The calculated path
    public Path path;

    //The AI's speed per second
    public float speed = 300f;
    public ForceMode2D fMode;

    //Variables to give the player and enemy some space ;)
    public float offsetY;
    public float offsetX;

    [HideInInspector]
    public bool pathIsEnded = false;

    //The max distance from the AI to the waypoint for it to continue to the next waypoint
    public float nextWaypointDistance = 3;

    // The waypoint we are currently moving towards
    private int currentWaypoint = 0;
    private int offsetCounter = 0;

    //This will be used for iteration of the searchingForPlayer function
    private bool searchingForPlayer = false;

    void Start()
    {
        offsetY = Random.Range(4f, 8f);//Give our enemy a natural flow of distance
        

        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        if(target == null)
        {
            if (!searchingForPlayer)//Where'd the player go? :/
            {
                searchingForPlayer = true;
                StartCoroutine(searchForPlayer());
            }
            return;
        }
        
        //Start a new path to the target position, return to the OnPathComplete Method
        seeker.StartPath(transform.position, new Vector3(target.position.x + offsetX, target.position.y + offsetY, target.position.z), OnPathComplete);

        StartCoroutine(UpdatePath ());

        //WRITE SOME MORE
    }

    IEnumerator searchForPlayer()
    {
        GameObject sResult = GameObject.FindGameObjectWithTag("Player");
        if(sResult == null)
        {
            //try searching for the player again after half a second
            yield return new WaitForSeconds(0.5f);
            StartCoroutine(searchForPlayer());
        } else //player has been found, set the target and stop searching
        {
            target = sResult.transform;
            searchingForPlayer = false;
            StartCoroutine(UpdatePath());
            yield return false;
        }
    }

    IEnumerator UpdatePath ()
    {
        if (offsetCounter == 6)//reset the offset occassionally to look more natural, rather than have sporadic movement
        {
            offsetY = Random.Range(4f, 8f);
            offsetX = Random.Range(-5f, 5f);
            offsetCounter = 0;
        }
        else
        {
            offsetCounter++;
        }

        if (target == null)
        {
            if (!searchingForPlayer)//Where'd the player go? :/
            {
                searchingForPlayer = true;
                StartCoroutine(searchForPlayer());
            }
            yield return false;
        }

        seeker.StartPath(transform.position, new Vector3(target.position.x + offsetX, target.position.y + offsetY, target.position.z), OnPathComplete);

        yield return new WaitForSeconds(1f / updateRate);
        StartCoroutine(UpdatePath());
    }

    public void OnPathComplete(Path p)
    {
        Debug.Log("We got a path. Did it have an error?" + p.error);
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    private void FixedUpdate()
    {

        if (target == null)
        {
            if (!searchingForPlayer)//Where'd the player go? :/
            {
                searchingForPlayer = true;
                StartCoroutine(searchForPlayer());
            }
            return;
        }

        //TODO: Always look at player?

        if (path == null)
        {
            return;
        }

        if(currentWaypoint >= path.vectorPath.Count)
        {
            if (pathIsEnded)
            {
                return;
            }
            Debug.Log("End of path reached.");
            pathIsEnded = true;
            return;
        }

        pathIsEnded = false;

        //Direction to the next waypoint
        Vector3 dir = (path.vectorPath[currentWaypoint] - transform.position).normalized;
        dir *= speed * Time.fixedDeltaTime;

        //Move the AI
        rb.AddForce(dir, fMode);

        float dist = Vector3.Distance(transform.position, path.vectorPath[currentWaypoint]);

        Debug.Log("This is the path location" + path.vectorPath[currentWaypoint]);

        // path.vectorPath is a list of nodes being incremented to the enemy path
        // path.vectorPath[CurrentWayPoint] is the current node that is being accessed
        if (dist < nextWaypointDistance)
        {
            currentWaypoint++;
            return;
        }


    }
}
