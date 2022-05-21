using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RadialRays
{
    public float rayWeight;
    private RaycastHit2D ray; // cannot be changed once class is created
    public Vector2 rayDir;
    public bool hitObstacle;
    public RadialRays(RaycastHit2D newRay, Vector2 newDir)
    {
        ray = newRay;
        rayDir = newDir;
    }

    public float getWeight()
    {
       // Debug.Log(rayWeight);
        return rayWeight;
    }

    public void setRayHit(RaycastHit2D newRay)
    {
        ray = newRay;
    }

    public Vector2 getDir()
    {
        return rayDir;
    }

    public void assignNewWeight(float newWeight)
    {
        rayWeight = newWeight;
    }

    public bool getRay()
    {
        hitObstacle = ray;
        return hitObstacle;
    }
}
