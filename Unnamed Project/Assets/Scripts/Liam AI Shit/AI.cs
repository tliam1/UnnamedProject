using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    [Header("Raycasting")]
    public RadialRays[] rayCasts = new RadialRays[8];
    public Vector2[] rayDirections; // only for assignment in start()
    public LayerMask aiObstacle;

    [Header("Pathing")]
    public GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < rayCasts.Length; i++)
        {
            rayCasts[i] = new RadialRays(Physics2D.Raycast(transform.position, rayDirections[i], rayCasts[i].getWeight(), aiObstacle), rayDirections[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < rayCasts.Length; i++)
        {  
            Debug.Log(target.transform.position.normalized + "\t" + rayCasts[i].getDir().normalized);
            rayCasts[i].assignNewWeight((Vector2.Dot(target.transform.position.normalized, rayCasts[i].getDir().normalized) + 1)/2);
            rayCasts[i].setRayHit(Physics2D.Raycast(transform.position, rayDirections[i], rayCasts[i].getWeight(), aiObstacle));
            Debug.Log(rayCasts[i].getWeight());
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        for (int i = 0; i < rayCasts.Length; i++)
        {
            Gizmos.DrawLine(transform.position, rayCasts[i].getDir() * rayCasts[i].getWeight());
        }
    }
}
