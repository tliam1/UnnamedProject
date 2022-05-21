using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    Rigidbody2D rb;
    [Header("Raycasting")]
    public RadialRays[] rayCasts = new RadialRays[8];
    public Vector2[] rayDirections; // only for assignment in start()
    public LayerMask aiObstacle;

    [Header("Pathing")]
    public GameObject target;
    public Vector3 desiredPath;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        desiredPath = Vector3.down; //change later
        for (int i = 0; i < rayCasts.Length; i++)
        {
            rayCasts[i] = new RadialRays(Physics2D.Raycast(transform.position, rayDirections[i], rayCasts[i].getWeight(), aiObstacle), rayDirections[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //feels very robotic, but works for now. Fix later (LERP!)
        if (Vector2.Distance(target.transform.position, transform.position) >= 1)
            NormalPathFinding();
        else if (rb.velocity != Vector2.zero)
            rb.velocity = Vector2.zero;
    }

    void NormalPathFinding()
    {
        for (int i = 0; i < rayCasts.Length; i++)
        {
            //Debug.Log(target.transform.position.normalized + "\t" + rayCasts[i].getDir().normalized);
            //issue when they perfectly match up with a wall and target, they jitter back and forth
            //create a favoring system and commit to a direction, unless completely lost
            bool previousRaycast = rayCasts[i].getRay();
            Vector2 distanceVector = target.transform.position - transform.position;
            rayCasts[i].assignNewWeight((Vector2.Dot(distanceVector.normalized, rayCasts[i].getDir().normalized) + 1) /2);
            rayCasts[i].setRayHit(Physics2D.Raycast(transform.position, rayDirections[i], rayCasts[i].getWeight(), aiObstacle));

            //change to see if the one with the highest priority changes
            if (rayCasts[i].getRay() != previousRaycast)
            {
                //we need to update our pathfinding 
                //Debug.Log("UPDATED PATH... current path: " + desiredPath);
                desiredPath = Vector3.zero;
                for (int j = 0; j < rayCasts.Length; j++)
                {
                    //distanceVector = target.transform.position - transform.position;
                    //rayCasts[j].assignNewWeight((Vector2.Dot(distanceVector.normalized, rayCasts[j].getDir().normalized) + 1) / 2);
                    //rayCasts[j].setRayHit(Physics2D.Raycast(transform.position, rayDirections[j], rayCasts[j].getWeight(), aiObstacle));
                    if (!rayCasts[j].getRay() && rayCasts[j].getWeight() > desiredPath.z && desiredPath == Vector3.zero)
                    {
                        desiredPath = new Vector3(rayCasts[j].getDir().x, rayCasts[j].getDir().y, rayCasts[j].getWeight());
                        continue;
                    }

                    if (!rayCasts[j].getRay() && rayCasts[j].getWeight() > desiredPath.z 
                        && Vector2.Distance(rayCasts[j].getDir().normalized, rb.velocity.normalized) < Vector2.Distance(desiredPath.normalized, rb.velocity.normalized)) //I am using the Z to store the weight value, pretty dumb but sorta smart
                    {
                        //this will be used to update navmesh Direction
                        desiredPath = new Vector3(rayCasts[j].getDir().x, rayCasts[j].getDir().y, rayCasts[j].getWeight()); 
                    }
                }
                desiredPath = new Vector3(desiredPath.x, desiredPath.y, 0);  //removing z component
                //Debug.Log("New desired path is: " + desiredPath);
                continue;
            }
            int collisionCount = 0;
            for (int k = 0; k < rayCasts.Length; k++)
            {
                if (rayCasts[k].getRay()) // if any of them are true we want to ignore the next bit
                {
                    collisionCount++;
                }
            }
            if (collisionCount > 0)
                continue;

            float velocityWeight = 10;
            int bestIndex = -1;
            for (int j = 0; j < rayCasts.Length; j++)
            {
                float tempWeight = Vector2.Distance(rayCasts[j].getDir().normalized, distanceVector.normalized);
                if(tempWeight < velocityWeight)
                {
                    velocityWeight = tempWeight;
                    bestIndex = j;
                }
            }
            //this if statement is bad, needs a better condition that runs once until new path is needed
            /*if (rayCasts[i].getWeight() > desiredPath.z) //I am using the Z to store the weight value, pretty dumb but sorta smart
            {
                //this will be used to update navmesh Direction
                Debug.Log("new Open Path: " + rayCasts[i].getDir() + "\t" + "old desired path: " + desiredPath);
                desiredPath = new Vector3(rayCasts[i].getDir().x, rayCasts[i].getDir().y, rayCasts[i].getWeight());
            }
            */
            desiredPath = rayCasts[bestIndex].getDir();

            //Debug.Log(rayCasts[i].getWeight());
        }
        //we only want to update desired path when a new interaction is involved
        //desiredPath = new Vector3(desiredPath.x, desiredPath.y, 0); //removing z component

        // start moving toward the target with the desired path
        if (rb.velocity != (Vector2)desiredPath)
        {
            rb.velocity = Vector2.Lerp(rb.velocity,desiredPath * 3, Time.deltaTime * 5);
            desiredPath = new Vector3(desiredPath.x, desiredPath.y, 0);  //removing z component
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        for (int i = 0; i < rayCasts.Length; i++)
        {
            Gizmos.DrawLine(transform.position, (Vector2)transform.position + rayCasts[i].getDir() * rayCasts[i].getWeight());
        }
    }
}
