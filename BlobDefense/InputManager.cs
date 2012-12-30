using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlobDefense
{
    using System.Drawing;
    using System.Windows.Forms;

    using BlobDefense.Towers;

    internal class InputManager : Singleton<InputManager>
    {
        /// <summary>
        /// Prevents a default instance of the <see cref="InputManager"/> class from being created.
        /// </summary>
        private InputManager()
        { 
        }

        public void Initialize()
        {
            EventManager.Instance.OnMouseClick += this.ProcesClick;
        }

        private void ProcesClick(Point position)
        {
            Point coordinates = new Point(position.X / 32, position.Y / 32);

            // Check if the node is not blocked
            if (!TileEngine.Instance.NodeMap[coordinates.X, coordinates.Y].IsBlocked)
            {
                // Block the node
                TileEngine.Instance.NodeMap[coordinates.X, coordinates.Y].IsBlocked = true;
                
                // Try to create a new path
                if (GameLogic.Instance.TryCreateNewPath())
                {
                    // If succeeded place tower
                    StandardTower testTower = new StandardTower();
                    testTower.Position = TileEngine.Instance.NodeMap[coordinates.X, coordinates.Y].Position;
                }
                else
                {
                    // Unblock the node
                    TileEngine.Instance.NodeMap[coordinates.X, coordinates.Y].IsBlocked = false;
                }
            }
        }
    }
}
