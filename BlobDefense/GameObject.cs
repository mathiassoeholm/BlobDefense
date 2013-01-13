// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GameObject.cs" company="Backdoor Fun">
//   © 2013
// </copyright>
// <summary>
//   Base class for all visible objects in the game.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace BlobDefense
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Drawing2D;

    using Extensions;

    /// <summary>
    /// Base class for all visible objects in the game.
    /// </summary>
    [Serializable]
    public class GameObject : IComparable<GameObject>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GameObject"/> class,
        /// adding it to the game objects list.
        /// </summary>
        public GameObject()
        {
           this.InitializeGameObject();
        }

        /// <summary>
        /// Gets the sprite sheet used for all game objects.
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
        /// Gets a list containing game objects which will be added to the game.
        /// </summary>
        public static List<GameObject> NewGameObjects { get; private set; }

        /// <summary>
        /// Gets or sets the depth level.
        /// Game objects with higher values gets rendered on top of game objects with lower values.
        /// </summary>
        public int DepthLevel { get; set; }

        /// <summary>
        /// Gets or sets the position of the game object in pixel coordinates.
        /// </summary>
        public PointF Position { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this game object can be destroyed or not.
        /// </summary>
        public bool NeverDestroy { get; set; }

        /// <summary>
        /// Gets or sets the source rectangle used on the sprite sheet.
        /// </summary>
        public Rectangle SpriteSheetSource { get; set; }

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
            int amount = GameObjectsToDestroy == null ? 0 : GameObjectsToDestroy.Count;

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
        /// Adds all new game objects to the game objects list.
        /// </summary>
        public static void AddAllNewGameObjects()
        {
            if (NewGameObjects.Count == 0)
            {
                return;
            }
   
            while (NewGameObjects.Count > 0)
            {
                // Add this game object to the game objects list
                AllGameObjects.Add(NewGameObjects[0]);

                NewGameObjects.RemoveAt(0);
            }

            // Resort the game objects by depth
            AllGameObjects.Sort();
        }

        /// <summary>
        /// This is automatically called by the constructor, if for some reason it isn't
        /// you can call it manually.
        /// </summary>
        public void InitializeGameObject()
        {
            // Initialize a new list if it is null
            AllGameObjects = AllGameObjects ?? new List<GameObject>();

            // Initialize a new queue if it is null
            GameObjectsToDestroy = GameObjectsToDestroy ?? new Queue<GameObject>();

            // Initialize a new queue if it is null
            NewGameObjects = NewGameObjects ?? new List<GameObject>();

            // Load image if not set yet
            SpriteSheet = SpriteSheet ?? Image.FromFile(@"Images\SpriteSheet.png");

            // This game object will be added to the game after the current fram
            NewGameObjects.Add(this);
        }

        /// <summary>
        /// Adds the game object to the destroy queue.
        /// </summary>
        public virtual void Destroy()
        {
            if (this.NeverDestroy)
            {
                return;
            }
            
            NewGameObjects.Remove(this);
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
                using (var m = new Matrix())
                {
                    m.RotateAt(this.Rotation, this.Position);
                    context.Transform = m;
                }
            }
            
            context.DrawImage(
                image: this.AlternativeImage == null ? SpriteSheet : AlternativeImage,
                destRect: new Rectangle(
                    (int)(centerPivot ? this.Position.X - this.SpriteSheetSource.Width.DividedByTwo() : this.Position.X),
                    (int)(centerPivot ? this.Position.Y - this.SpriteSheetSource.Height.DividedByTwo() : this.Position.Y),
                    this.SpriteSheetSource.Width,
                    this.SpriteSheetSource.Height),
                srcX: this.SpriteSheetSource.X,
                srcY: this.SpriteSheetSource.Y,
                srcWidth: this.SpriteSheetSource.Width,
                srcHeight: this.SpriteSheetSource.Height,
                srcUnit: GraphicsUnit.Pixel);

            // Rotate back to 0
            if (this.Rotation != 0)
            {
                context.ResetTransform();
            }
        }

        /// <summary>
        /// Compares to game objects depth level, to decide which one is rendered on top.
        /// </summary>
        /// <param name="other">
        /// The other game object to compare with.
        /// </param>
        /// <returns>
        /// The -1, 0 or 1 based on the depth levels.
        /// </returns>
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