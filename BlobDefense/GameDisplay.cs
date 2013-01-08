﻿namespace BlobDefense
{
    using System;
    using System.Drawing;
    using System.Linq;
    using System.Windows.Forms;

    using BlobDefense.Gui;
    using BlobDefense.WaveSpawner;

    using Extensions;

    /// <summary>
    /// The windows form which is responsible of displaying the game.
    /// </summary>
    public sealed partial class GameDisplay : Form
    {
        private static GameDisplay instance;

        private readonly GameObject goalGraphic;

        private readonly GameObject mouseCursor;

        private int currentFps;

        private DateTime lastFpsUpdate;

        public GameDisplay()
        {
            this.InitializeComponent();

            instance = this;

            // Initialize the input manager
            InputManager.Instance.Initialize();

            // Initialize the game manager
            GameManager.Instance.InitializeManager();

            // Craete and render mouse on top of everything else
            this.mouseCursor = new MouseCursor { DepthLevel = int.MaxValue };

            // Load the tile map
            TileEngine.Instance.LoadMapFromXml();

            // Create a path for the enemies
            GameLogic.Instance.TryCreateNewPath();

            // Set up goal graphic
            this.goalGraphic = new GameObject();
            this.goalGraphic.SpriteSheetSource = new Rectangle(128, 0, 72, 83);
            this.goalGraphic.DepthLevel = 10;
            this.goalGraphic.Position = new PointF(GameLogic.Instance.GoalNode.Position.X, this.goalGraphic.SpriteSheetSource.Height / 2);
            
            // Set size of the form
            this.ClientSize = new Size((TileEngine.TilesX * TileEngine.TilesOnSpriteSize) + GuiManager.RightPanelWidth, (TileEngine.TilesY * TileEngine.TilesOnSpriteSize));

            Time.SetDeltaTime();

            this.DoubleBuffered = true;
            this.ResizeRedraw = true;
        }

        public static new Point MousePosition
        {
            get
            {
                return instance.PointToClient(Cursor.Position);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (!GameManager.Instance.IsPlaying)
            {
                return;
            }
            
            this.RenderGame(e.Graphics);
            this.Invalidate();
        }

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

            this.DrawSelectedTile(graphics);

            // Run logic
            GameLogic.Instance.RunLogic(graphics);

            // Draw gui
            GuiManager.Instance.DrawInGameGui(graphics);

            InputManager.Instance.Update(MousePosition);

            // Write fps
            this.WriteFps(graphics);

            // Render mouse cursor
            this.mouseCursor.Render(graphics);
        }

        private void DrawSelectedTile(Graphics graphics)
        {
            MapNode hoveredNode = InputManager.Instance.HovederedMouseNode;

            // Return if node is null
            if (hoveredNode == null)
            {
                return;
            }

            // Draw selection
            graphics.DrawRectangle(
                new Pen(Color.Red),
                new Rectangle(
                    hoveredNode.X - (TileEngine.TilesOnSpriteSize / 2),
                    hoveredNode.Y - (TileEngine.TilesOnSpriteSize / 2),
                    TileEngine.TilesOnSpriteSize,
                    TileEngine.TilesOnSpriteSize));
        }

        private void WriteFps(Graphics graphics)
        {
            // Only update fps every 0.5 seconds
            if (DateTime.Now.Subtract(this.lastFpsUpdate).TotalSeconds > 0.5f)
            {
                this.currentFps = (int)(1 / Time.DeltaTime);

                this.lastFpsUpdate = DateTime.Now;
            }

            graphics.DrawString("FPS: " + this.currentFps.ToString(), new Font("Arial", 12), new SolidBrush(Color.Green), 0, 0);
        }

        private void DrawNeighbors(Graphics graphics)
        {
            foreach (MapNode mapNode in TileEngine.Instance.NodeMap)
            {
                foreach (MapNode neighbor in mapNode.Neighbors)
                {
                    graphics.DrawLine(new Pen(Color.Blue, 1), mapNode.Position, neighbor.Position);
                }
            }
        }

        private void DrawBuildGrid(Graphics graphics)
        {
            Pen gridPen = new Pen(Color.FromArgb(125, 0, 0, 0), 2);
            
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
        }

        private void DrawEnemyPath(Graphics graphics)
        {
            // Draw the enemies path
            graphics.DrawLines(new Pen(Color.Red, 5), GameLogic.EnemyPath.Select(mapNode => mapNode.Position).ToArray());
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

        private void PlayBtn_Click(object sender, EventArgs e)
        {
            // Hide the cursor
            Cursor.Hide();

            // Hide main menu controls
            this.PlayBtn.Visible = false;
            this.NameTxt.Visible = false;
            
            // Start a new game
            GameManager.Instance.StartNewGame();
        }

        private void NameTxt_TextChanged(object sender, EventArgs e)
        {
            GameSettings.PlayerName = this.NameTxt.Text;
        }
    }
}
