// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InputManager.cs" company="Backdoor Fun">
//   © 2013
// </copyright>
// <summary>
//   Processes input from the player.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace BlobDefense
{
    using System.Drawing;
    using System.Linq;

    using BlobDefense.Enemies;
    using BlobDefense.Gui;
    using BlobDefense.Towers;

    using Extensions;

    /// <summary>
    /// Processes input from the player.
    /// </summary>
    internal class InputManager : Singleton<InputManager>
    {
        /// <summary>
        /// The currently hovered button.
        /// </summary>
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
                // Get the coordinates
                var coordinates = new Point(GameDisplay.MousePosition.X / TileEngine.TilesOnSpriteSize, GameDisplay.MousePosition.Y / TileEngine.TilesOnSpriteSize);

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

        /// <summary>
        /// Initializes the input manager, by subscribing to various events.
        /// </summary>
        public void Initialize()
        {
            EventManager.Instance.OnMouseClick += this.ProcesClick;
            EventManager.Instance.OnMouseUp += this.OnMouseUp;
            EventManager.Instance.OnMouseDown += this.OnMouseDown;
        }

        /// <summary>
        /// Called once per frame.
        /// Sets buttons state.
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

            if (!didHoverOnAButton)
            {
                this.hoveredButton = null;
            }
        }

        /// <summary>
        /// Called when mouse button is released.
        /// If a button is hovered, its click event is called.
        /// </summary>
        /// <param name="position">
        /// The position.
        /// </param>
        private void OnMouseUp(Point position)
        {
            if (this.hoveredButton != null)
            {
                this.hoveredButton.State = GuiButton.ButtonState.Standard;
                this.hoveredButton.Click();
            }
        }

        /// <summary>
        /// Called once when the mouse button is pressed down.
        /// Sets the state of a potential hovered button to pressed.
        /// </summary>
        /// <param name="position">
        /// The position.
        /// </param>
        private void OnMouseDown(Point position)
        {
            if (this.hoveredButton != null)
            {
                this.hoveredButton.State = GuiButton.ButtonState.Pressed;
            }
        }

        /// <summary>
        /// Called when clicked, performs appropriate action based on mouse position.
        /// </summary>
        /// <param name="position">
        /// The mouse position.
        /// </param>
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
            if (!clickedNode.IsBlocked && clickedNode != GameLogic.Instance.StartNode)
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
                    case 2:
                        this.PlaceTower<SniperTower>(clickedNode);
                        break;
                    case 3:
                        this.PlaceTower<AgilityTower>(clickedNode);
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

        /// <summary>
        /// Places a tower on the specified map node if legal.
        /// </summary>
        /// <param name="clickedNode">
        /// The clicked map node.
        /// </param>
        /// <typeparam name="TTower">
        /// The type of tower to build
        /// </typeparam>
        private void PlaceTower<TTower>(MapNode clickedNode) where TTower : Tower, new()
        {
            // Return if there is any enemies, we can't build under a wave
            if (GameObject.AllGameObjects.Any(g => g is Enemy))
            {
                return;
            }
            
            // Instantiate the tower
            var tower = new TTower { Position = clickedNode.Position };

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
            else
            {
                // Invoke placed a tower
                EventManager.Instance.PlacedATower.SafeInvoke();
            }
        }
    }
}
