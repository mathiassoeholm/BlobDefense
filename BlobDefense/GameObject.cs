namespace BlobDefense
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Drawing2D;

    /// <summary>
    /// Base class for all entities in the game.
    /// </summary>
    public class GameObject : IComparable<GameObject>
    {
        private static bool aNewGameObjectWasCreated;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="GameObject"/> class,
        /// adding it to the game objects list.
        /// </summary>
        public GameObject()
        {
            // Initialize a new list if it is null
            AllGameObjects = AllGameObjects ?? new List<GameObject>();

            // Initialize a new queue if it is null
            GameObjectsToDestroy = GameObjectsToDestroy ?? new Queue<GameObject>();

            // Load image if not set yet
            SpriteSheet = SpriteSheet ?? Image.FromFile(@"Images\SpriteSheet.png");

            // Add this game object to the game objects list
            AllGameObjects.Add(this);

            aNewGameObjectWasCreated = true;
        }

        /// <summary>
        /// Gameobjects with higher values gets renderes on top of gameobjects with lower values
        /// </summary>
        public int DepthLevel { get; set; }

        /// <summary>
        /// Gets the sprite sheet used for all game objects
        /// </summary>
        public static Image SpriteSheet { get; private set; }

        /// <summary>
        /// Gets a list of all the game objects created.
        /// </summary>
        public static List<GameObject> AllGameObjects { get; private set; }

        /// <summary>
        /// Gets a queue containing game objects which will be removed from the game.
        /// </summary>
        public static Queue<GameObject> GameObjectsToDestroy { get; private set; }

        /// <summary>
        /// Gets or sets the position of the game object in pixel coordinates.
        /// </summary>
        public PointF Position { get; set; }

        /// <summary>
        /// Gets or sets the source rectangle used on the sprite sheet.
        /// </summary>
        public RectangleF SpriteSheetSource { get; set; }

        /// <summary>
        /// Gets or sets the rotation of this game object.
        /// </summary>
        public float Rotation { get; set; }


        /// <summary>
        /// Gets or sets a value indicating whether to draw from the center,
        /// no matter what else was specified to the render method.
        /// </summary>
        public bool ForceDrawFromCenter { get; set; }

        /// <summary>
        /// Gets or sets an optional image to render instead of using the sprite sheet.
        /// </summary>
        protected Image AlternativeImage { get; set; }

        /// <summary>
        /// Removes any destroyed game objects from the global game object list.
        /// </summary>
        public static void EmptyDestroyQueue()
        {
            int amount = GameObjectsToDestroy.Count;

            if (amount == 0)
            {
                return;
            }
            
            for (int i = 0; i < GameObjectsToDestroy.Count; i++)
            {
                AllGameObjects.Remove(GameObjectsToDestroy.Dequeue());
            }
        }

        public static void SortGameObjectsByDepth()
        {
            if (aNewGameObjectWasCreated)
            {
                // Sort the list
                AllGameObjects.Sort();

                aNewGameObjectWasCreated = false;
            }
        }

        /// <summary>
        /// Adds the game object to the destroy queue.
        /// </summary>
        public void Destroy()
        {
            GameObjectsToDestroy.Enqueue(this);
        }

        /// <summary>
        /// Displays the game object in the graphics context using pixel coordinates.
        /// </summary>
        /// <param name="context">
        /// The graphics context used to display the game object.
        /// </param>
        /// <param name="centerPivot">
        /// A value indicating whether to render from the middle or from the top left.
        /// </param>
        public virtual void Render(Graphics context, bool centerPivot = false)
        {
            if (this.ForceDrawFromCenter)
            {
                centerPivot = true;
            }

            // Rotate the graphics if a rotation is specified.
            if (this.Rotation != 0)
            {
                using (Matrix m = new Matrix())
                {
                    m.RotateAt(Rotation, Position);
                    context.Transform = m;
                }
            }
            
            context.DrawImage(
                image: this.AlternativeImage == null ? SpriteSheet : AlternativeImage,
                destRect: new Rectangle(
                    (int)(centerPivot ? this.Position.X - (this.SpriteSheetSource.Width / 2) : this.Position.X),
                    (int)(centerPivot ? this.Position.Y - (this.SpriteSheetSource.Height / 2) : this.Position.Y),
                    (int)this.SpriteSheetSource.Width,
                    (int)this.SpriteSheetSource.Height),
                srcX: this.SpriteSheetSource.X,
                srcY: this.SpriteSheetSource.Y,
                srcWidth: this.SpriteSheetSource.Width,
                srcHeight: this.SpriteSheetSource.Height,
                srcUnit: GraphicsUnit.Pixel);

            // Rotate back
            if (this.Rotation != 0)
            {
                context.ResetTransform();
            }
        }

        public int CompareTo(GameObject other)
        {
            if (this.DepthLevel == other.DepthLevel)
            {
                return 0;
            }

            return this.DepthLevel < other.DepthLevel ? -1 : 1;
        }
    }
}