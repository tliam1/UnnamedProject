using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    Grid grid;

    private void Awake()
    {
        grid = GetComponent<Grid>(); //may want to do a 
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void findPath(Vector2 startPos, Vector2 targetPos)
    {
        Node startNode = grid.NodeFromPoint(startPos);
        Node targetNode = grid.NodeFromPoint(targetPos);

        //A HashSet is basically the same as a Dictionary but without a value for each key.
        //It's just a collection of keys and you can quickly test if a value is part of the set or not
        List<Node> openNodes = new List<Node>();
        HashSet<Node> closedNodes = new HashSet<Node>();
        openNodes.Add(startNode);

        while(openNodes.Count > 0)
        {

        }
    }
}
