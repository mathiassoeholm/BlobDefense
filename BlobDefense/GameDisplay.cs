// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GameDisplay.cs" company="Backdoor Fun">
//   © 2013
// </copyright>
// <summary>
//   The windows form which is responsible of displaying the game.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace BlobDefense
{
    using System;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Windows.Forms;

    using BlobDefense.Gui;

    using Extensions;

    /// <summary>
    /// The windows form which is responsible of displaying the game.
    /// </summary>
    public sealed partial class GameDisplay : Form
    {
        /// <summary>
        /// The form instance.
        /// </summary>
        private static GameDisplay instance;

        /// <summary>
        /// The current frame rate.
        /// </summary>
        private int currentFps;

        /// <summary>
        /// The point in where we updated the fps last
        /// </summary>
        private DateTime lastFpsUpdate;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameDisplay"/> class.
        /// </summary>
        public GameDisplay()
        {
            this.InitializeComponent();

            // Set the static singleton instance
            instance = this;

            // Subscribe to open menu event
            EventManager.Instance.OpenedMainMenu += this.ShowMainMenu;
            
            // Initialize sound engine
            AudioManager.Instance.InitializeSoundEngine();

            // Initialize the input manager
            InputManager.Instance.Initialize();

            // Set size of the form
            this.ClientSize = new Size((TileEngine.TilesX * TileEngine.TilesOnSpriteSize) + GuiManager.RightPanelWidth, TileEngine.TilesY * TileEngine.TilesOnSpriteSize);

            // Set visibilty of the continue button, based on if we have any save data
            this.ContinueBtn.Visible = File.Exists(GameSettings.SaveDataPath + @"\SavedGame.dat");

            // Set the initial delta time
            Time.SetDeltaTime();

            // Set some form properties
            this.DoubleBuffered = true;
            this.ResizeRedraw = true;
        }

        /// <summary>
        /// Gets the form width in pixels.
        /// </summary>
        public static int FormWidth
        {
            get
            {
                return instance.Width;
            }
        }

        /// <summary>
        /// Gets the form height in pixels.
        /// </summary>
        public static int FormHeight
        {
            get
            {
                return instance.Height;
            }
        }

        /// <summary>
        /// Gets the mouse position.
        /// </summary>
        public static new Point MousePosition
        {
            get
            {
                return instance.PointToClient(Cursor.Position);
            }
        }

        /// <summary>
        /// We override OnPaint to create our render loop.
        /// </summary>
        /// <param name="e">
        /// The paint event argument, we get our graphics context from here.
        /// </param>
        protected override void OnPaint(PaintEventArgs e)
        {
            // Check the game state, before we render anything
            switch (GameManager.Instance.CurrentGameState)
            {
                case GameState.MainMenu:
                    this.NameTxt.Update();
                    this.PlayBtn.Update();
                    this.ToggleStoryBtn.Update();
                    this.ContinueBtn.Update();
                    this.StoryLbl.Update();
                    break;
                case GameState.Playing:
                    this.RenderGame(e.Graphics);
                    GuiManager.Instance.DrawInGameGui(e.Graphics);
                    break;
                case GameState.GameOver:
                    GuiManager.Instance.DrawGameOverScreen(e.Graphics);    
                    break;
            }

            // Invalidate the form, so we repaint
            this.Invalidate();
        }

        /// <summary>
        /// The render game.
        /// </summary>
        /// <param name="graphics">
        /// The graphics.
        /// </param>
        private void RenderGame(Graphics graphics)
        {
            // Set the frame delta time
            Time.SetDeltaTime();

            // Draw the map tiles
            TileEngine.Instance.RenderTiles(graphics);

            // Draw build grid, if a tower is selected.
            if (GuiManager.Instance.SelectedTowerToBuild != -1)
            {
               this.DrawBuildGrid(graphics); 
            }

            // Draw tile selection indicator
            this.DrawSelectedTile(graphics);

            // Run logic
            GameLogic.Instance.RunLogic(graphics);

            // Update input
            InputManager.Instance.Update(MousePosition);

            // Write fps
            // this.WriteFps(graphics);
        }

        /// <summary>
        /// Draws an indication around the tile that the mouse is hovering.
        /// </summary>
        /// <param name="graphics">
        /// The graphics object used to draw the tile.
        /// </param>
        private void DrawSelectedTile(Graphics graphics)
        {
            MapNode hoveredNode = InputManager.Instance.HovederedMouseNode;

            // Return if node is null
            if (hoveredNode == null)
            {
                return;
            }

            // Draw selection indicator
            graphics.DrawRectangle(
                new Pen(Color.Red),
                new Rectangle(
                    hoveredNode.X - (TileEngine.TilesOnSpriteSize / 2),
                    hoveredNode.Y - (TileEngine.TilesOnSpriteSize / 2),
                    TileEngine.TilesOnSpriteSize,
                    TileEngine.TilesOnSpriteSize));
        }

        /// <summary>
        /// Writes and updated the frames per second, in the top left corner.
        /// </summary>
        /// <param name="graphics">
        /// The graphics object used to write the fps.
        /// </param>
        private void WriteFps(Graphics graphics)
        {
            // Only update fps every 0.5 seconds
            if (DateTime.Now.Subtract(this.lastFpsUpdate).TotalSeconds > 0.5f)
            {
                this.currentFps = (int)(1 / Time.RealDeltaTime);

                this.lastFpsUpdate = DateTime.Now;
            }

            graphics.DrawString("FPS: " + this.currentFps.ToString(), new Font("Arial", 12), new SolidBrush(Color.Green), 0, 0);
        }

        /// <summary>
        /// Draws the build grid and the enemies path.
        /// </summary>
        /// <param name="graphics">
        /// The graphics object used to draw.
        /// </param>
        private void DrawBuildGrid(Graphics graphics)
        {
            Pen gridPen = new Pen(Color.FromArgb(125, 0, 0, 0), 2);
            
            // Draw the grid
            for (int y = 0; y <= TileEngine.TilesX; y++)
            {
                for (int x = 0; x <= TileEngine.TilesY; x++)
                {
                    if (x != 0)
                    {
                        // Draw line to the left
                        graphics.DrawLine(
                            gridPen,
                            new Point(x * TileEngine.TilesOnSpriteSize, y * TileEngine.TilesOnSpriteSize),
                            new Point((x - 1) * TileEngine.TilesOnSpriteSize, y * TileEngine.TilesOnSpriteSize));
                    }

                    if (y != 0)
                    {
                        // Draw line to up
                        graphics.DrawLine(
                            gridPen,
                            new Point(x * TileEngine.TilesOnSpriteSize, y * TileEngine.TilesOnSpriteSize),
                            new Point(x * TileEngine.TilesOnSpriteSize, (y - 1) * TileEngine.TilesOnSpriteSize));
                    }
                }
            }

            // Draw the enemies path
            this.DrawEnemyPath(graphics);
        }

        /// <summary>
        /// Draws the enemies path.
        /// </summary>
        /// <param name="graphics">
        /// The graphics object used to draw the path.
        /// </param>
        private void DrawEnemyPath(Graphics graphics)
        {
            // Draw the enemies path
            graphics.DrawLines(new Pen(Color.Red, 5), GameLogic.EnemyPath.Select(mapNode => mapNode.Position).ToArray());
        }

        /// <summary>
        /// Hides all controls in the main menu
        /// </summary>
        private void HideMainMenu()
        {
            // Hide main menu controls
            this.PlayBtn.Visible = false;
            this.NameTxt.Visible = false;
            this.ContinueBtn.Visible = false;
            this.ToggleStoryBtn.Visible = false;
        }

        /// <summary>
        /// Show all controls in the main menu
        /// </summary>
        private void ShowMainMenu()
        {
            // Hide main menu controls
            this.PlayBtn.Visible = true;
            this.NameTxt.Visible = true;
            this.ContinueBtn.Visible = File.Exists(GameSettings.SaveDataPath + @"\SavedGame.dat");
            this.ToggleStoryBtn.Visible = true;
        }

        private void PlayBtn_Click(object sender, EventArgs e)
        {
            // Hide the main menu
            this.HideMainMenu();
            
            // Start a new game
            GameManager.Instance.StartNewGame();
        }

        private void GameDisplay_Load(object sender, EventArgs e)
        {
            // Fill in the name text field
            this.NameTxt.Text = GameSettings.PlayerName;
        }

        private void ContinueBtn_Click(object sender, EventArgs e)
        {
            // Hide the main menu
            this.HideMainMenu();
            
            // Continue from a save file
            GameManager.Instance.ContinueGame();
        }

        private void NameTxt_TextChanged(object sender, EventArgs e)
        {
            // Sets the player name to the text entered in the text box
            GameSettings.PlayerName = this.NameTxt.Text;
        }

        private void GameDisplay_Click(object sender, EventArgs e)
        {
            EventManager.Instance.OnMouseClick.SafeInvoke(MousePosition);
        }

        private void GameDisplay_MouseUp(object sender, MouseEventArgs e)
        {
            EventManager.Instance.OnMouseUp.SafeInvoke(MousePosition);
        }

        private void GameDisplay_MouseDown(object sender, MouseEventArgs e)
        {
            EventManager.Instance.OnMouseDown.SafeInvoke(MousePosition);
        }

        private void ToggleStoryBtn_Click(object sender, EventArgs e)
        {
            bool isShowingStory = this.ToggleStoryBtn.Text != "The Story";

            this.ToggleStoryBtn.Text = isShowingStory ? "The Story" : "Back";

            if (isShowingStory)
            {
                // Show the main menu
                this.ShowMainMenu();

                // Hide story
                this.StoryLbl.Visible = false;
            }
            else
            {
                // Hide main menu except the toggle story button
                this.HideMainMenu();
                this.ToggleStoryBtn.Visible = true;

                // Show story
                this.StoryLbl.Visible = true;
            }
        }
    }
}
