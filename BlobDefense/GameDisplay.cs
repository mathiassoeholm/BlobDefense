namespace BlobDefense
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
        private const int GuiLeftOffset = 10;
        private const int SpeedBtnTopOffset = 53;
        private const int SpaceBetweenSpeedButtons = 65;
        private const int RightPanelWidth = 205;
        
        private static GameDisplay instance;

        private readonly GameObject goalGraphic;

        private int currentFps;

        private DateTime lastFpsUpdate;

        public GameDisplay()
        {
            this.InitializeComponent();

            Cursor.Hide();

            instance = this;

            // Load button images
            Image nextWaveBtnStandard = Image.FromFile(@"Images/NewWaveBtn.png");
            Image speed100BtnStandard = Image.FromFile(@"Images/Speed100Btn.png");
            Image speed200BtnStandard = Image.FromFile(@"Images/Speed200Btn.png");
            Image speed400BtnStandard = Image.FromFile(@"Images/Speed400Btn.png");

            // Set up next wave button
            new GuiButton(
                new Rectangle((TileEngine.TilesX * TileEngine.TilesOnSpriteSize) + GuiLeftOffset, GuiLeftOffset, nextWaveBtnStandard.Width, nextWaveBtnStandard.Height),
                nextWaveBtnStandard,
                Image.FromFile(@"Images/NewWaveBtn_Hovered.png"),
                Image.FromFile(@"Images/NewWaveBtn_Pressed.png"),
                WaveManager.Instance.StartWave);

            // Set up speed 100% button
            new GuiButton(
                new Rectangle((TileEngine.TilesX * TileEngine.TilesOnSpriteSize) + GuiLeftOffset, SpeedBtnTopOffset, speed100BtnStandard.Width, speed100BtnStandard.Height),
                speed100BtnStandard,
                Image.FromFile(@"Images/Speed100Btn_Hovered.png"),
                Image.FromFile(@"Images/Speed100Btn_Pressed.png"),
                () => Time.TimeScale = 1);

            // Set up speed 200% button
            new GuiButton(
                new Rectangle((TileEngine.TilesX * TileEngine.TilesOnSpriteSize) + GuiLeftOffset + SpaceBetweenSpeedButtons, SpeedBtnTopOffset, speed200BtnStandard.Width, speed200BtnStandard.Height),
                speed200BtnStandard,
                Image.FromFile(@"Images/Speed200Btn_Hovered.png"),
                Image.FromFile(@"Images/Speed200Btn_Pressed.png"),
                () => Time.TimeScale = 5);

            // Set up speed 400% button
            new GuiButton(
                new Rectangle((TileEngine.TilesX * TileEngine.TilesOnSpriteSize) + GuiLeftOffset + (SpaceBetweenSpeedButtons * 2), SpeedBtnTopOffset, speed400BtnStandard.Width, speed400BtnStandard.Height),
                speed400BtnStandard,
                Image.FromFile(@"Images/Speed400Btn_Hovered.png"),
                Image.FromFile(@"Images/Speed400Btn_Pressed.png"),
                () => Time.TimeScale = 10);

            // Initialize the input manager
            InputManager.Instance.Initialize();

            // Craete and render mouse on top of everything else
            new MouseCursor { DepthLevel = int.MaxValue };

            // Load the tile map
            TileEngine.Instance.LoadMapFromXml();

            // Create a path for the enemies
            GameLogic.Instance.TryCreateNewPath();

            // Set up goal graphic
            this.goalGraphic = new GameObject();
            this.goalGraphic.SpriteSheetSource = new RectangleF(128, 0, 72, 83);
            this.goalGraphic.DepthLevel = 10;
            this.goalGraphic.Position = new PointF(GameLogic.Instance.GoalNode.Position.X, this.goalGraphic.SpriteSheetSource.Height / 2);
            
            // Set size of the form
            this.ClientSize = new Size((TileEngine.TilesX * TileEngine.TilesOnSpriteSize) + RightPanelWidth, (TileEngine.TilesY * TileEngine.TilesOnSpriteSize));

            Time.SetDeltaTime();

            this.DoubleBuffered = true;
            this.ResizeRedraw = true;

            Time.TimeScale = 4;
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
            this.RenderGame(e.Graphics);
            this.Invalidate();
        }

        private void RenderGame(Graphics graphics)
        {
            // Set the frame delta time
            Time.SetDeltaTime();

            InputManager.Instance.Update(MousePosition);

            // Draw the map tiles
            TileEngine.Instance.RenderTiles(graphics);

            this.DrawSelectedTile(graphics);

            // Draw the enemies path
            graphics.DrawLines(new Pen(Color.Red, 5), GameLogic.EnemyPath.Select(mapNode => mapNode.Position).ToArray());

            // Draw gui buttons
            GuiButton.DrawAllButtons(graphics);

            // Run logic
            GameLogic.Instance.RunLogic(graphics);

            // Write fps
            this.WriteFps(graphics);

            //// DrawNeighbors(graphics);
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
    }
}
