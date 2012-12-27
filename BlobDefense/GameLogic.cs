using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlobDefense
{
    using System.Windows.Forms;

    internal class GameLogic : Singleton<GameLogic>
    {
        /// <summary>
        /// Prevents a default instance of the <see cref="GameLogic"/> class from being created.
        /// </summary>
        private GameLogic()
        {
        }

        public void RunLogic()
        {
            if (Keyboard.IsKeyDown(Keys.E))
            {
                var enemy = new StandardEnemy();
            }
            
            // Update game objects
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
                    gameObject.Render(GameDisplay.Buffer.Graphics, centerPivot: true);
                }
            }

            GameObject.EmptyDestroyQueue();
        }
    }
}
