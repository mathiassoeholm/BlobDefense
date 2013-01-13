// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IPathNode.cs" company="Backdoor Fun">
//   © 2013
// </copyright>
// <summary>
//   The PathNode interface.
//   Implemented by all nodes types that is used by the A* class.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace AStar
{
    using System.Collections.Generic;

    /// <summary>
    /// The PathNode interface.
    /// Implemented by all nodes types that is used by the A* class.
    /// </summary>
    /// <typeparam name="T">
    /// The type of node, which implements this interface.
    /// </typeparam>
    public interface IPathNode<T>
    {
        /// <summary>
        /// Gets or sets the x position.
        /// </summary>
        int X { get; set; }

        /// <summary>
        /// Gets or sets the y position.
        /// </summary>
        int Y { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the node is closed.
        /// </summary>
        bool IsClosed { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the node is blocked.
        /// </summary>
        bool IsBlocked { get; set; }

        /// <summary>
        /// Gets or sets the g score for path finding.
        /// </summary>
        float GScore { get; set; }

        /// <summary>
        /// Gets or sets the h score for path finding.
        /// </summary>
        float HScore { get; set; }

        /// <summary>
        /// Gets or sets the neighbor nodes.
        /// </summary>
        List<T> Neighbors { get; set; }

        /// <summary>
        /// Gets or sets the parent node.
        /// </summary>
        T Parent { get; set; }
    }
}
