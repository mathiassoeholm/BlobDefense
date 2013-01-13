// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GameLogic.cs" company="Backdoor Fun">
//   © 2013
// </copyright>
// <summary>
//   Responsible of running the games primary logic.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace BlobDefense
{
    using System.Collections.Generic;
    using System.Drawing;

    using BlobDefense.Enemies;

    /// <summary>
    /// Responsible of running the games primary logic.
    /// </summary>
    internal class GameLogic : Singleton<GameLogic>
    {
        /// <summary>
        /// Prevents a default instance of the <see cref="GameLogic"/> class from being created.
        /// </summary>
        private GameLogic()
        {
        }

        /// <summary>
        /// Gets or sets the enemy path.
        /// </summary>
        public static List<MapNode> EnemyPath { get; set; }

        /// <summary>
        /// Gets the start node.
        /// </summary>
        public MapNode StartNode { get; private set; }

        /// <summary>
        /// Gets the goal node.
        /// </summary>
        public MapNode GoalNode { get; private set; }

        /// <summary>
        /// Tries to create a new path, if unsuccessful the current path remains unchanged.
        /// </summary>
        /// <returns>
        /// A value indicating whether a new path would be blocked or not.
        /// </returns>
        public bool TryCreateNewPath()
        {
            // Unclose all nodes
            foreach (MapNode mapNode in TileEngine.Instance.NodeMap)
            {
                mapNode.IsClosed = false;
            }
            
            // Generate the new path
            List<MapNode> newPath = Astar<MapNode>.GeneratePath(this.StartNode, this.GoalNode);

            // Return false if the path was null
            if (newPath == null)
            {
                return false;
            }

            // Set the new path and return true
            EnemyPath = newPath;

            return true;
        }

        /// <summary>
        /// Initialized the games logic.
        /// </summary>
        public void InitializeGameLogic()
        {
            // Connect nodes for the AStar
            Astar<MapNode>.ConnectNodes(TileEngine.Instance.NodeMap);

            // Assign start and goal nodes
            this.StartNode = TileEngine.Instance.NodeMap[0, TileEngine.TilesY - 1];
            this.GoalNode = TileEngine.Instance.NodeMap[TileEngine.TilesX - 2, 0];

            // Set up goal graphic
            var goalGraphic = new GameObject();
            goalGraphic.SpriteSheetSource = new Rectangle(128, 0, 72, 83);
            goalGraphic.DepthLevel = 10;
            goalGraphic.Position = new PointF(GoalNode.Position.X, goalGraphic.SpriteSheetSource.Height / 2);
        }

        /// <summary>
        /// Called once per frame, runs logic for all game objects.
        /// </summary>
        /// <param name="graphicsContext">
        /// The graphics context, used to render game objects.
        /// </param>
        public void RunLogic(Graphics graphicsContext)
        {
            foreach (GameObject gameObject in GameObject.AllGameObjects)
            {
                // Update behaviours
                if (gameObject is IUpdateBehaviour)
                {
                    (gameObject as IUpdateBehaviour).Update();
                }

                // Run animations
                if (gameObject is IAnimated)
                {
                    (gameObject as IAnimated).RunAnimation();
                }

                // Don't render tiles, they are handled elsewhere
                if (!(gameObject is Tile) && !(gameObject is MouseCursor))
                {
                    gameObject.Render(graphicsContext, true);

                    if (gameObject is Enemy)
                    {
                        (gameObject as Enemy).DrawHealthBar(graphicsContext);
                    }
                }

                // Break out of the loop, if whe are not playing anymore
                if (GameManager.Instance.CurrentGameState != GameState.Playing)
                {
                    break;
                }
            }

            GameObject.EmptyDestroyQueue();

            GameObject.AddAllNewGameObjects();
        }
    }
}
