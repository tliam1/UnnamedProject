using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public Transform player;

    public LayerMask unwalkableMask;
    public Vector2 gridSize;
    public float nodeRadius;
    Node[,] grid;

    float nodeDiameter;
    int gridSizeX, gridSizeY;

    // Start is called before the first frame update
    void Start()
    {
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridSize.y / nodeDiameter);

        CreateGrid();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void CreateGrid()
    {
        grid = new Node[gridSizeX, gridSizeY];
        Vector2 bottomLeftOfGrid = (Vector2)transform.position - Vector2.right * gridSize.x/2 - Vector2.up * gridSize.y/2; //transform.pos = center of world

        for(int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector2 worldPoint = bottomLeftOfGrid + Vector2.right * (x * nodeDiameter + nodeRadius)
                    + Vector2.up * (y * nodeDiameter + nodeRadius);
                bool walkable = !Physics2D.OverlapPoint(worldPoint, unwalkableMask);
                grid[x, y] = new Node(walkable, worldPoint);
            }
        }
    }

    public Node NodeFromPoint(Vector2 position)
    {
        float percentX = (position.x + gridSize.x/2) / gridSize.x;
        float percentY = (position.y + gridSize.y / 2) / gridSize.y;
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.RoundToInt((gridSizeX - 1) * percentX); //array based so -1
        int y = Mathf.RoundToInt((gridSizeY - 1) * percentY); //array based so -1
        return grid[x, y];
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, (Vector2)gridSize);

        if (grid != null)
        {
            Node playersNode = NodeFromPoint(player.position);
            foreach (Node node in grid)
            {
                Gizmos.color = node.walkable ? Color.white : Color.red; //if ? then : else
                if (playersNode == node)
                    Gizmos.color = Color.cyan;
                Gizmos.DrawCube(node.worldPos, Vector2.one * (nodeDiameter - 0.1f));
            }
        }
    }
}
