namespace BlobDefense
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Threading;
    using System.Windows.Forms;
    using System.Linq;
    using Extensions;

    using AStar;

    using BlobDefense.Towers;
    using BlobDefense.WaveSpawner;

    /// <summary>
    /// The windows form which is responsible of displaying the game.
    /// </summary>
    public partial class GameDisplay : Form
    {
        private static GameDisplay instance;

        private readonly MouseCursor mouseCursor;

        private readonly GameObject goalGraphic;

        private BufferedGraphicsContext context;

        private BufferedGraphics buffer;

        private int currentFps;

        private DateTime lastFpsUpdate;

        private Thread gameThread;

        public GameDisplay()
        {
            this.InitializeComponent();

            Cursor.Hide();

            instance = this;

            // Initialize the input manager
            InputManager.Instance.Initialize();

            // Render mouse on top of everything else
            this.mouseCursor = new MouseCursor { DepthLevel = int.MaxValue };

            TileEngine.Instance.LoadMapFromXml();

            GameLogic.Instance.TryCreateNewPath();

            // Set up goal graphic
            this.goalGraphic = new GameObject();
            this.goalGraphic.SpriteSheetSource = new RectangleF(128, 0, 72, 83);
            this.goalGraphic.DepthLevel = 1;
            this.goalGraphic.Position = new PointF(GameLogic.Instance.GoalNode.Position.X, this.goalGraphic.SpriteSheetSource.Height / 2);
            
            // Temp stuff start -------


            this.Width = (TileEngine.TilesX * TileEngine.TilesOnSpriteSize) + 50;
            this.Height = (TileEngine.TilesY * TileEngine.TilesOnSpriteSize) + 50;

            // Temp stuff end -------

            Time.SetDeltaTime();

            this.DoubleBuffered = true;
            this.ResizeRedraw = true;
        }

        private void GameDisplay_Load(object sender, EventArgs e)
        {
            //// Create a thread object, passing in the MainLoop method
            //gameThread = new Thread(this.MainLoop);
            
            //// Start the render thread
            //gameThread.Start();
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
            RenderScene(e.Graphics);
            this.Invalidate();
        }

        private void RenderScene(Graphics graphics)
        {
            // Set the frame delta time
            Time.SetDeltaTime();

            // Clear the screen with the forms back color
            //this.buffer.Graphics.Clear(this.BackColor);

            // Buffer the map tiles
            TileEngine.Instance.RenderTiles(graphics);

            // Draw the enemies path
            graphics.DrawLines(new Pen(Color.Red, 5), GameLogic.EnemyPath.Select(mapNode => mapNode.Position).ToArray());

            // Run logic
            GameLogic.Instance.RunLogic(graphics);

            // Write fps
            this.WriteFps(graphics);

            this.SetMouseCursorPosition();

            //// DrawNeighbors();
        }

        private void MainLoop()
        {
            while (true)
            {
                
            }
        }

        private void SetMouseCursorPosition()
        {
            if (this.InvokeRequired)
            {
                Action d = this.SetMouseCursorPosition;
                this.Invoke(d);
            }
            else
            {
                this.mouseCursor.SetPosition(new Point(Cursor.Position.X - this.Location.X, Cursor.Position.Y - this.Location.Y));
            }
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

        private void DrawNeighbors()
        {
            foreach (MapNode mapNode in TileEngine.Instance.NodeMap)
            {
                foreach (MapNode neighbor in mapNode.Neighbors)
                {
                    buffer.Graphics.DrawLine(new Pen(Color.Blue, 1), mapNode.Position, neighbor.Position);
                }
            }
        }

        private void GameDisplay_Click(object sender, EventArgs e)
        {
            EventManager.Instance.OnMouseClick.SafeInvoke(MousePosition);
        }
    }
}
