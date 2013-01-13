// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Astar.cs" company="Backdoor Fun">
//   © 2013
// </copyright>
// <summary>
//   AStart class, containing static methods to connects nodes and find a path using the A* algorithm.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace AStar
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// AStart class, containing static methods to connects nodes and find a path using the A* algorithm.
    /// </summary>
    /// <typeparam name="TNode">
    /// The node type used.
    /// </typeparam>
    public static class Astar<TNode> where TNode : class, IPathNode<TNode>, IComparable<TNode>
    {
        /// <summary>
        /// Connects the nodes in a specified 2d grid.
        /// </summary>
        /// <param name="grid">
        /// The grid of nodes, implemented as a 2d array.
        /// </param>
        public static void ConnectNodes(TNode[,] grid)
        {
            for (int y = 0; y < grid.GetLength(1); y++)
            {
                for (int x = 0; x < grid.GetLength(0); x++)
                {
                    // Connect nodes
                    if (x > 0)
                    {
                        // Connect to the node to the left
                        ConnectTwoNodes(grid[x, y], grid[x - 1, y]);
                    }

                    if (y > 0)
                    {
                        // Connect to the node above
                        ConnectTwoNodes(grid[x, y], grid[x, y - 1]);
                    }
                }
            }
        }

        /// <summary>
        /// Generates a path between the start and goal node.
        /// </summary>
        /// <param name="start">
        /// The start of the path.
        /// </param>
        /// <param name="goal">
        /// The end of the path.
        /// </param>
        /// <returns>
        /// The shortest path between the two nodes.
        /// </returns>
        public static List<TNode> GeneratePath(TNode start, TNode goal)
        {
            // Create the open list of nodes, initially containing only our starting node
            var openList = new PriorityQueue<TNode>();
            openList.Enqueue(start);

            while (openList.Count() > 0)
            {
                // Consider the best node in the open list (the node with the lowest f value)
                TNode currentNode = openList.Dequeue();

                // If this node is the goal then we're done
                if (currentNode == goal)
                {
                    return BuildPath(currentNode);
                }

                // Close the node
                currentNode.IsClosed = true;

                // Consider all of its neighbors
                foreach (TNode neighbor in currentNode.Neighbors)
                {
                    // Continue if the node is closed or blocked
                    if (neighbor.IsClosed || neighbor.IsBlocked)
                    {
                        continue;
                    }

                    // Calculate the neighbors g score
                    float neighborGScore = currentNode.GScore + 1;

                    // Check if the neighbor is not on the open list already or if it has a lower g score than previously.
                    if (!openList.Contains(neighbor) || neighborGScore <= neighbor.GScore)
                    {
                        // Assign the neigbors parent to be the current node
                        neighbor.Parent = currentNode;
                        neighbor.GScore = neighborGScore;
                        neighbor.HScore = Manhattan(neighbor, goal);

                        // Add the neighbor to the open list if it was not contained already
                        if (!openList.Contains(neighbor))
                        {
                            openList.Enqueue(neighbor);
                        }
                    }
                }
            }

            // No path was found :-(
            return null;
        }

        /// <summary>
        /// The manhattan heuristic.
        /// </summary>
        /// <param name="nodeA">
        /// The node to check from.
        /// </param>
        /// <param name="goal">
        /// The node to check to.
        /// </param>
        /// <returns>
        /// The approximated distance, the h score.
        /// </returns>
        private static float Manhattan(TNode nodeA, TNode goal)
        {
            return Math.Abs(nodeA.X - goal.X) + Math.Abs(nodeA.Y - goal.Y);
        }

        /// <summary>
        /// Assembles the final path.
        /// </summary>
        /// <param name="goal">
        /// The goal node.
        /// </param>
        /// <returns>
        /// The list of nodes to follow.
        /// </returns>
        private static List<TNode> BuildPath(TNode goal)
        {
            var path = new List<TNode>();
            TNode node = goal;

            while (node != null)
            {
                path.Add(node);
                node = node.Parent;
            }

            path.Reverse();

            return path;
        }

        /// <summary>
        /// Connects two nodes, makes them neighbors.
        /// </summary>
        /// <param name="nodeA">
        /// The first node.
        /// </param>
        /// <param name="nodeB">
        /// The second node.
        /// </param>
        private static void ConnectTwoNodes(TNode nodeA, TNode nodeB)
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
}