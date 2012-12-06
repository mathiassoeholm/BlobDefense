namespace BlobDefense
{
    using System.Collections.Generic;
    using System.Drawing;

    /// <summary>
    /// Base class for all entities in the game.
    /// </summary>
    internal abstract class GameObject
    {
        protected GameObject()
        {
            // Initialize a new list if it is null
            AllGameObjects = AllGameObjects ?? new List<GameObject>();

            // Add this game object to the game objects list
            AllGameObjects.Add(this);
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="GameObject"/> class,
        /// removing it from the game object list.
        /// </summary>
        ~GameObject()
        {
            // Remove this game object from the game objects list
            AllGameObjects.Remove(this);
        }

        /// <summary>
        /// Gets a list of all the game objects created.
        /// </summary>
        public static List<GameObject> AllGameObjects { get; private set; }

        /// <summary>
        /// Gets or sets the position of the game object in pixel coordinates.
        /// </summary>
        public PointF Position { get; protected set; }
    }
}