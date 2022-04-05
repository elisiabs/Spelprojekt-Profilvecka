using System.Collections;
using UnityEngine;
using Pathfinding;

[ RequireComponent (typeof (Rigidbody2D))]
[RequireComponent (typeof (Seeker))]
public class EnemyAI : MonoBehaviour {

    // what to chase?
    public Transform target;

    // how many times a second a second we will update our path

    public float updateRate = 2f;

    // caching
    private Seeker seeker;
    private Rigidbody2D rb;

    //the calculated path
    public Path path;

    // the ai speed per second
    public float speed = 300f;
    public ForceMode2D fMode;

    [HideInInspector]
    public bool pathIsEnded = false;

    // the max distance from a ai to a waypoint for it to continue to the next waypoint
    public float nextWaypointDistance = 3;

    // The Waypoint we are currently moving towards
    private int currentWaypoint = 0;

    // Elias modifikation(sprite):
    public SpriteRenderer sprite;
    public bool spotted = false;
    
    private void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        if (target == null)
        {
            Debug.LogError("No Player found.");
            return;
        }

        // Start a new path to the target position, return the result to the onePathComplete method
        seeker.StartPath(transform.position, target.position, onPathComplete);

        StartCoroutine (UpdatePath ());
        
    }
    
    IEnumerator UpdatePath ()
    {
        if(target == null)
        {
            //TODO: search for player
            yield break;
        }
           
        seeker.StartPath(transform.position, target.position, onPathComplete);



        yield return new WaitForSeconds(1f / updateRate);
        StartCoroutine(UpdatePath());
    }

    public void onPathComplete (Path p)
    {
        //Debug.Log("We got a path. Did it have an error? " + p.error);
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }


    private void FixedUpdate()
    {
        
        {
            if (target == null)
            {
                return;
            }

            if (path == null)
            {
                return;
            }
            if (currentWaypoint >= path.vectorPath.Count - 1)
            {
                if (pathIsEnded)
                {
                    //Debug.Log("End of path reached.");
                    pathIsEnded = true;
                }

                currentWaypoint = path.vectorPath.Count - 1;
            }

            //Debug.Log($"PathEnded: {pathIsEnded}. CurrentWaypoint: {currentWaypoint}. Waypoints: {path.vectorPath.Count}");

            if (pathIsEnded == false)
            {
                float dist = Vector3.Distance(transform.position, path.vectorPath[currentWaypoint]);
                if (dist < nextWaypointDistance && currentWaypoint < path.vectorPath.Count - 1)
                {
                    currentWaypoint++;
                }
                pathIsEnded = false;

                Vector3 dir = (path.vectorPath[currentWaypoint] - transform.position).normalized;
                //Elias modifikation:

                Vector2 diff = transform.position - target.position;

                if (diff.x > 0)
                {
                    sprite.flipX = false;
                }
                else if (diff.x < 0)
                {
                    sprite.flipX = true;
                }
                //Debug.Log(diff);
                //Slut av Elias modifikation.
                dir *= speed * Time.fixedDeltaTime;

                if(spotted)
                {
                    rb.AddForce(dir, fMode);
                }
                
            }
        

        }  
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            spotted = true;
        }
    }
}   

