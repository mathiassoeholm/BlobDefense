using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AStar;


public static class Astar<TNode> where TNode : class, IPathNode<TNode>, IComparable<TNode> 
{
    public static void ConnectNodes(TNode[,] grid)
    {
        for (int y = 0; y < grid.GetLength(1); y++)
        {
            for (int x = 0; x < grid.GetLength(0); x++)
            {
                // Connect nodes
                if (x > 0)
                {
                    BidiConnect(grid[x, y], grid[x - 1, y]); // left
                }
                if (y > 0)
                {
                    BidiConnect(grid[x, y], grid[x, y - 1]); // up
                }
            }
        }
    }

    public static List<TNode> GeneratePath(TNode start, TNode goal)
    {
        // Create the open list of nodes, initially containing only our starting node
        PriorityQueue<TNode> openList = new PriorityQueue<TNode>();
        openList.Enqueue(start);

        // Create the closed list of nodes, initially empty
        List<TNode> closedList = new List<TNode>();

        while (openList.Count() > 0)
        {
            // Consider the best node in the open list (the node with the lowest f value)
            TNode currentNode = openList.Dequeue();

            // If this node is the goal then we're done
            if (currentNode == goal)
            {
                return BuildPath(currentNode);
            }
            
            // Move the current node to the closed list 
            closedList.Add(currentNode);

            // Consider all of its neighbors
            foreach (TNode neighbor in currentNode.Neighbors)
            {
                if (neighbor.IsClosed)
                {
                    continue;
                }

                float neighborGScore = currentNode.GScore + 1;

                if (!openList.Contains(neighbor))
                {
                    openList.Enqueue(neighbor);
                    neighbor.Parent = currentNode;
                    neighbor.GScore = neighborGScore;
                    neighbor.HScore = 0; // TODO: Implement heuristic
                }
                else if (neighborGScore < neighbor.GScore)
                {
                    neighbor.Parent = currentNode;
                    neighbor.GScore = neighborGScore;
                }
            }
        }
        
        // No path
        return null;
    }

    private static List<TNode> BuildPath(TNode goal)
    {
        List<TNode> path = new List<TNode>();
        TNode node = goal;

        while (node != null)
        {
            path.Add(node);
            node = (TNode)node.Parent;
        }

        path.Reverse();

        return path;
    }

    private static void BidiConnect(TNode nodeA, TNode nodeB)
    {
        if (!nodeA.Neighbors.Contains(nodeB))
        {
            nodeA.Neighbors.Add(nodeB);
        }

        if (!nodeB.Neighbors.Contains(nodeA))
        {
            nodeB.Neighbors.Add(nodeA);
        }
    }
}

