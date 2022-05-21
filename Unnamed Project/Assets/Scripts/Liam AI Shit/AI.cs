using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        desiredPath = Vector3.zero;
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
            Debug.Log(target.transform.position.normalized + "\t" + rayCasts[i].getDir().normalized);
            Vector2 distanceVector = target.transform.position - transform.position;
            rayCasts[i].assignNewWeight(Vector2.Dot(distanceVector.normalized, rayCasts[i].getDir().normalized));
            rayCasts[i].setRayHit(Physics2D.Raycast(transform.position, rayDirections[i], rayCasts[i].getWeight(), aiObstacle));
            if (rayCasts[i].getWeight() > desiredPath.z) //I am using the Z to store the weight value, pretty dumb but sorta smart
            {
                desiredPath = new Vector3(rayCasts[i].getDir().x, rayCasts[i].getDir().y, rayCasts[i].getWeight());
            }
            //Debug.Log(rayCasts[i].getWeight());
        }
        desiredPath = new Vector3(desiredPath.x, desiredPath.y, 0); //removing z component

        // start moving toward the target with the desired path
        if (rb.velocity != (Vector2)desiredPath)
            rb.velocity = desiredPath * 5;
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
