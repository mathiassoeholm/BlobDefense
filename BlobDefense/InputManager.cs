using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlobDefense
{
    using System.Diagnostics;
    using System.Drawing;
    using System.Windows.Forms;

    using BlobDefense.Gui;
    using BlobDefense.Towers;
    using Extensions;

    internal class InputManager : Singleton<InputManager>
    {
        private GuiButton hoveredButton;

        /// <summary>
        /// Prevents a default instance of the <see cref="InputManager"/> class from being created.
        /// </summary>
        private InputManager()
        { 
        }

        /// <summary>
        /// Gets the node that the mouse is hovering over if any.
        /// </summary>
        public MapNode HovederedMouseNode
        {
            get
            {
                Point coordinates = new Point(GameDisplay.MousePosition.X / TileEngine.TilesOnSpriteSize, GameDisplay.MousePosition.Y / TileEngine.TilesOnSpriteSize);

                if (coordinates.X < TileEngine.Instance.NodeMap.GetLength(0)
                    && coordinates.Y < TileEngine.Instance.NodeMap.GetLength(1)
                    && coordinates.X >= 0
                    && coordinates.Y >= 0)
                {
                    // Return the hovered node
                    return TileEngine.Instance.NodeMap[coordinates.X, coordinates.Y];
                }

                // Mouse is outside the map, return null
                return null;
            }
        }

        public void Initialize()
        {
            EventManager.Instance.OnMouseClick += this.ProcesClick;
            EventManager.Instance.OnMouseUp += this.OnMouseUp;
            EventManager.Instance.OnMouseDown += this.OnMouseDown;
        }

        /// <summary>
        /// Called once per frame.
        /// </summary>
        /// <param name="position">
        /// The current mouse position.
        /// </param>
        public void Update(Point position)
        {
            bool didHoverOnAButton = false;
            
            foreach (GuiButton button in GuiButton.AllButtons)
            {
                if (button.PositionAndSize.Contains(position))
                {
                    this.hoveredButton = button;
                    didHoverOnAButton = true;

                    if (button.State == GuiButton.ButtonState.Standard)
                    {
                        button.State = GuiButton.ButtonState.Hovered;
                    }
                }
                else
                {
                    button.State = GuiButton.ButtonState.Standard;
                }
            }

            if(!didHoverOnAButton)
            {
                this.hoveredButton = null;
            }
        }

        private void OnMouseUp(Point position)
        {
            if (this.hoveredButton != null)
            {
                this.hoveredButton.State = GuiButton.ButtonState.Standard;
                this.hoveredButton.Click();
            }
        }

        private void OnMouseDown(Point position)
        {
            if (this.hoveredButton != null)
            {
                this.hoveredButton.State = GuiButton.ButtonState.Pressed;
            }
        }

        private void ProcesClick(Point position)
        {
            MapNode clickedNode = this.HovederedMouseNode;

            // Return if node is null
            if (clickedNode == null)
            {
                if (this.hoveredButton == null)
                {
                    // No tower was selected, deselect any towers
                    EventManager.Instance.DeselectedTower.SafeInvoke();
                }

                return;
            }

            // Check if the node is not blocked
            if (!clickedNode.IsBlocked)
            {
                // Build the selected tower
                switch (GuiManager.Instance.SelectedTowerToBuild)
                {
                    case 0:
                        this.PlaceTower<StandardTower>(clickedNode);
                        break;
                    case 1:
                        this.PlaceTower<FrostTower>(clickedNode);
                        break;
                }
            }
            else
            {
                // Select tower on that pos, if any
                foreach (Tower tower in GameObject.AllGameObjects.OfType<Tower>())
                {
                    if (tower.Position == clickedNode.Position)
                    {
                        EventManager.Instance.TowerWasSelected.SafeInvoke(tower);
                        return;
                    }
                }
            }

            if (this.hoveredButton == null)
            {
                // No tower was selected, deselect any towers
                EventManager.Instance.DeselectedTower.SafeInvoke();
            }
        }

        private void PlaceTower<T>(MapNode clickedNode) where T : Tower, new()
        {
            // Return if there is any enemies, we can't build under a wave
            if (GameObject.AllGameObjects.Any(g => g is Enemy))
            {
                return;
            }
            
            // Instantiate the tower
            T tower = new T { Position = clickedNode.Position };

            // Block the node
            clickedNode.IsBlocked = true;

            // Check if we cannot build the tower
            if (GameManager.Instance.Currency < tower.BuildPrice || !GameLogic.Instance.TryCreateNewPath() || !GameManager.Instance.TryBuy(tower.BuildPrice))
            {
                // Unblock the node
                clickedNode.IsBlocked = false;

                // Destroy the tower
                tower.Destroy();
            }
        }
    }
}
