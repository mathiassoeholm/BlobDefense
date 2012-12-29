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
        }

        public void RunLogic(Graphics graphicsContext)
        {
            if (Keyboard.IsKeyDown(Keys.W))
            {
                WaveManager.Instance.StartWave();
            }

            int gameObjectCount = GameObject.AllGameObjects.Count;

            for (int i = 0; i < gameObjectCount; i++)
            {
                GameObject gameObject = GameObject.AllGameObjects[i];

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

            GameObject.SortGameObjectsByDepth();
        }
    }
}
