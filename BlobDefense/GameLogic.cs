using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlobDefense
{
    using System.Drawing;
    using System.Windows.Forms;

    using BlobDefense.WaveSpawner;

    internal class GameLogic : Singleton<GameLogic>
    {
        /// <summary>
        /// Prevents a default instance of the <see cref="GameLogic"/> class from being created.
        /// </summary>
        private GameLogic()
        {
            // Connect nodes for the AStar
            Astar<MapNode>.ConnectNodes(TileEngine.Instance.NodeMap);

            // Assign start and goal nodes
            this.StartNode = TileEngine.Instance.NodeMap[0, TileEngine.TilesY - 1];
            this.GoalNode = TileEngine.Instance.NodeMap[TileEngine.TilesX - 2, 0];
        }

        public static List<MapNode> EnemyPath { get; set; } 

        public MapNode StartNode { get; private set; }

        public MapNode GoalNode { get; private set; }

        /// <summary>
        /// Tries to create a new path, if unsucessful the current path remains unchanged.
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

        public void RunLogic(Graphics graphicsContext)
        {
            if (Keyboard.IsKeyDown(Keys.W))
            {
                WaveManager.Instance.StartWave();
            }

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
                if (!(gameObject is Tile))
                {
                    gameObject.Render(graphicsContext, centerPivot: true);
                }
            }

            GameObject.EmptyDestroyQueue();

            GameObject.AddAllNewGameObjects();
        }
    }
}
