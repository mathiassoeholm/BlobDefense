namespace BlobDefense
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

            // Initialize a new queue if it is null
            GameObjectsToDestroy = GameObjectsToDestroy ?? new Queue<GameObject>();

            // Load image if not set yet
            SpriteSheet = SpriteSheet ?? Image.FromFile(@"Images\SpriteSheet.png");

            // Add this game object to the game objects list
            AllGameObjects.Add(this);
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
            context.DrawImage(
                image: SpriteSheet,
                destRect: new Rectangle((int)(centerPivot ? this.Position.X - (this.SpriteSheetSource.Width / 2) : this.Position.X),
                    (int)(centerPivot ? this.Position.Y - (this.SpriteSheetSource.Height / 2) : this.Position.Y),
                    (int)this.SpriteSheetSource.Width,
                    (int)this.SpriteSheetSource.Height),
                srcX: this.SpriteSheetSource.X,
                srcY: this.SpriteSheetSource.Y,
                srcWidth: this.SpriteSheetSource.Width,
                srcHeight: this.SpriteSheetSource.Height,
                srcUnit: GraphicsUnit.Pixel);
        }
    }
}