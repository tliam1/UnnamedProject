using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public bool walkable;
    public Vector2 worldPos;

    public float gCost;
    public float hCost;
    public float fCost; // gCost + hCost
    public Node(bool setWalkable, Vector2 setWorldPos)
    {
        walkable = setWalkable;
        worldPos = setWorldPos;
    }
}
