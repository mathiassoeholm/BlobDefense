﻿namespace BlobDefense
{
    using System.Collections.Generic;
    using System.Drawing;

    /// <summary>
    /// Base class for all entities in the game.
    /// </summary>
    public abstract class GameObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GameObject"/> class,
        /// adding it to the game objects list.
        /// </summary>
        protected GameObject()
        {
            // Initialize a new list if it is null
            AllGameObjects = AllGameObjects ?? new List<GameObject>();

            // Load image if not set yet
            SpriteSheet = SpriteSheet ?? Image.FromFile(@"Images\SpriteSheet.png");

            // Add this game object to the game objects list
            AllGameObjects.Add(this);
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="GameObject"/> class,
        /// removing it from the game objects list.
        /// </summary>
        ~GameObject()
        {
            // Remove this game object from the game objects list
            AllGameObjects.Remove(this);
        }

        /// <summary>
        /// Gets the sprite sheet used for all game objects
        /// </summary>
        public static Image SpriteSheet { get; private set; }

        /// <summary>
        /// Gets a list of all the game objects created.
        /// </summary>
        public static List<GameObject> AllGameObjects { get; private set; }

        /// <summary>
        /// Gets or sets the position of the game object in pixel coordinates.
        /// </summary>
        public PointF Position { get; protected set; }

        /// <summary>
        /// Gets or sets the source rectangle used on the sprite sheet.
        /// </summary>
        public RectangleF SpriteSheetSource { get; set; }

        /// <summary>
        /// Displays the game object in the graphics context using pixel coordinates.
        /// </summary>
        /// <param name="context">
        /// The graphics context used to display the game object.
        /// </param>
        public virtual void Render(Graphics context)
        {
            context.DrawImage(
                image: SpriteSheet,
                x: (int)this.Position.X,
                y: (int)this.Position.Y,
                srcRect: this.SpriteSheetSource,
                srcUnit: GraphicsUnit.Pixel);
        }
    }
}