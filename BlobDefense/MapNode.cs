// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MapNode.cs" company="Backdoor Fun">
//   © 2013
// </copyright>
// <summary>
//   Defines a map node, used in a 2D array by the tile engine.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace BlobDefense
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;

    using AStar;

    /// <summary>
    /// Defines a map node, used in a 2D array by the tile engine.
    /// </summary>
    [Serializable]
    public class MapNode : IPathNode<MapNode>, IComparable<MapNode>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MapNode"/> class.
        /// </summary>
        public MapNode()
        {
            this.Position = new Point();
            this.Neighbors = new List<MapNode>(4);
        }

        /// <summary>
        /// Gets or sets the tile type.
        /// </summary>
        public int TileType { get; set; }

        /// <summary>
        /// Gets or sets the x position.
        /// </summary>
        public int X
        {
            get
            {
                return this.Position.X;
            }

            set
            {
                this.Position = new Point(value, this.Position.Y);
            }
        }

        /// <summary>
        /// Gets or sets the y position.
        /// </summary>
        public int Y
        {
            get
            {
                return this.Position.Y;
            }

            set
            {
                this.Position = new Point(this.Position.X, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the node is closed.
        /// </summary>
        public bool IsClosed { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the node is blocked.
        /// </summary>
        public bool IsBlocked { get; set; }

        /// <summary>
        /// Gets or sets the position.
        /// </summary>
        public Point Position { get; set; }

        /// <summary>
        /// Gets or sets the neighbor nodes.
        /// </summary>
        public List<MapNode> Neighbors { get; set; }

        /// <summary>
        /// Gets or sets the parent node.
        /// </summary>
        public MapNode Parent { get; set; }

        /// <summary>
        /// Gets or sets the g score for path finding.
        /// </summary>
        public float GScore { get; set; }

        /// <summary>
        /// Gets or sets the h score for path finding.
        /// </summary>
        public float HScore { get; set; }

        /// <summary>
        /// Gets the f score for path finding.
        /// </summary>
        public float FScore
        {
            get
            {
                return this.GScore + this.HScore;
            }
        }

        /// <summary>
        /// Connects to a node, adding it to the neighbor list.
        /// </summary>
        /// <param name="node">
        /// The node to connect to.
        /// </param>
        public void ConnectTo(MapNode node)
        {
            if (!this.Neighbors.Contains(node))
            {
                this.Neighbors.Add(node);
            }
        }

        /// <summary>
        /// Compares the nodes f score with another node, used for path finding.
        /// </summary>
        /// <param name="other">
        /// The other node to compare with.
        /// </param>
        /// <returns>
        /// -1, 0 or 1 based on their f scores.
        /// </returns>
        public int CompareTo(MapNode other)
        {
            if (this.FScore < other.FScore)
            {
                return 1;
            }

            return -1;
        }
    }
}
