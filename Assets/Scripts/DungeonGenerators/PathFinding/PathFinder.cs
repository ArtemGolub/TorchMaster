using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    private Grid Grid;
    public List<Node> movedPath;
    private Vector3 endPose;

    public bool GetPath(Transform start, Transform end, Grid grid)
    {
        Grid = grid;
        bool condition = FindPath(start.position, end.position);
        
        return condition;
    }
    public Node FindClosestNodeToTarget()
    {
        Node closestNode = null;
        float closestDistance = Mathf.Infinity;

        foreach (Node node in movedPath)
        {
            float distance = Vector3.Distance(node.worldPosition, endPose);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestNode = node;
            }
        }
        return closestNode;
    }
    bool FindPath(Vector3 startPose, Vector3 targetPose)
    {
        movedPath = new List<Node>();
        endPose = targetPose;
        
        Node startNode = Grid.NodeFromWorldPoint(startPose);
        Node targetNode = Grid.NodeFromWorldPoint(targetPose);

        List<Node> openSet = new List<Node>();
        HashSet<Node> closedSet = new HashSet<Node>();

        openSet.Add(startNode);
        
        int maxAttempts = 10;
        int attempts = 0;
        
        while (attempts++ < maxAttempts)
        {        
            while (openSet.Count > 0)
            {
                Node currentNode = openSet[0];
                for (int i = 1; i < openSet.Count; i++)
                {
                    if (openSet[i].fCost < currentNode.fCost || openSet[i].fCost == currentNode.fCost && openSet[i].hCost < currentNode.hCost)
                    {
                        currentNode = openSet[i];
                    }
                }
            
                openSet.Remove(currentNode);
                closedSet.Add(currentNode);

                if (currentNode == targetNode)
                {
                    RetracePath(startNode, targetNode);
                    return true;
                }

                foreach (Node neighbor in Grid.GetNeighbors(currentNode))
                {
                    if (!neighbor.walkable || closedSet.Contains(neighbor))
                    {
                        continue;
                    }

                    int newMovementCostToNeighbor = currentNode.gCost + GetDistance(currentNode, neighbor);
                    if (newMovementCostToNeighbor < neighbor.gCost || !openSet.Contains(neighbor))
                    {
                        neighbor.gCost = newMovementCostToNeighbor;
                        neighbor.hCost = GetDistance(neighbor, targetNode);
                        neighbor.parent = currentNode;
                        
                        if (!openSet.Contains(neighbor))
                        {
                            openSet.Add(neighbor);
                            movedPath.Add(currentNode);
                        }
                    }
                }
            }
        }
        return false;
    }
    int GetDistance(Node a, Node b)
    {
        int dstX = Mathf.Abs(a.gridX - b.gridX);
        int dstY = Mathf.Abs(a.gridY - b.gridY);

        if (dstX > dstY)
        {
            return 14 * dstY + 10 * (dstX - dstY);
        }
        else
        {
            return 14 * dstX + 10 * (dstY - dstX);
        }
    }

   
    void RetracePath(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;
        
        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }
        
        path.Reverse();
    }
}