using System.Collections.Generic;
using UnityEngine;

public class Grid: MonoBehaviour
{
    public LayerMask unwalkableMask;
    public float nodeRadius;
    public Node[,] grid;

    private Transform StartPose;
    private Vector2 gridWorldSize;
    private float nodeDiametr;
    private int gridSizeX, gridSizeY;
    private Vector3 CellSize;
    
    public void SetGrid(int x, int y, Transform startPose, Vector3 cellSize)
    {
        CellSize = cellSize;
        gridWorldSize = new Vector2(x * CellSize.x, y * CellSize.z);
        
        StartPose = startPose;
        
        nodeDiametr = nodeRadius * 2;
        gridSizeX = Mathf.FloorToInt(gridWorldSize.x / nodeDiametr);
        gridSizeY = Mathf.FloorToInt(gridWorldSize.y / nodeDiametr);
        CreateGrid();
    }

    public void UpdateGrid()
    {
        CreateGrid();
    }
    
    private void CreateGrid()
    {
        grid = new Node[gridSizeX,gridSizeY];
        Vector3 worldBottomLeft = StartPose.transform.position - 
                                  Vector3.right * gridWorldSize.x / 2 -
                                  Vector3.forward * gridWorldSize.y / 2;
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector3 worldPoint = worldBottomLeft + 
                                     Vector3.right * (x * nodeDiametr + nodeRadius) +
                                     Vector3.forward * (y * nodeDiametr + nodeRadius);
                bool walkable = !(Physics.CheckSphere(worldPoint, nodeRadius, unwalkableMask));
                grid[x, y] = new Node(walkable, worldPoint, x, y);
            }
        }
    }
    public Node NodeFromWorldPoint(Vector3 worldPosition)
    {
        float percentX = ((worldPosition.x  + gridWorldSize.x / 2) - StartPose.transform.position.x) / gridWorldSize.x;
        float percentY = ((worldPosition.z + gridWorldSize.y / 2)- StartPose.transform.position.z) / gridWorldSize.y;
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);
        
        int x =Mathf.RoundToInt((gridSizeX - 1) * percentX);
        int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);
        
        return grid[x, y];
    }
    public List<Node> GetNeighbors(Node node)
    {
        List<Node> neighbors = new List<Node>();

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if(x == 0 && y == 0) continue;
                int checkX = node.gridX + x;
                int checkY = node.gridY + y;
                if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)
                {
                    neighbors.Add(grid[checkX, checkY]);
                }
            }
        }
        return neighbors;
    }
    
    
    private void OnDrawGizmos()
    {
        if(StartPose == null) return;
        Gizmos.DrawWireCube(StartPose.transform.position, new Vector3(gridWorldSize.x ,1, gridWorldSize.y));
        if (grid != null)
        {
            foreach (var node in grid)
            {
                if(node == null) continue;
                Gizmos.color = (node.walkable) ? Color.green : Color.red;
                if (GetComponent<PathFinder>().movedPath != null)
                {
                    if (GetComponent<PathFinder>().movedPath.Contains(node))
                    {
                        Gizmos.color = Color.blue;
                    }
                }
                Gizmos.DrawCube(node.worldPosition, Vector3.one * (nodeDiametr-0.1f));
            }
        }
    }


}