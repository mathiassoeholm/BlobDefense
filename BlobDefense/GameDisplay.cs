namespace BlobDefense
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Threading;
    using System.Windows.Forms;
    using System.Linq;

    using AStar;

    using BlobDefense.Towers;
    using BlobDefense.WaveSpawner;

    /// <summary>
    /// The windows form which is responsible of displaying the game.
    /// </summary>
    public partial class GameDisplay : Form
    {
        public static List<MapNode> TestPath;

        private static GameDisplay instance;

        private readonly MouseCursor mouseCursor;

        private readonly GameObject goalGraphic;

        private readonly MapNode goalNode;

        private readonly MapNode startNode;

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

            // Render mouse on top of everything else
            this.mouseCursor = new MouseCursor { DepthLevel = int.MaxValue };

            TileEngine.Instance.LoadMapFromXml();

            // Assign start and goal nodes
            this.startNode = TileEngine.Instance.NodeMap[0, TileEngine.TilesY - 1];
            this.goalNode = TileEngine.Instance.NodeMap[TileEngine.TilesX - 2, 0];

            // Set up goal graphic
            this.goalGraphic = new GameObject();
            this.goalGraphic.SpriteSheetSource = new RectangleF(128, 0, 72, 83);
            this.goalGraphic.DepthLevel = 1;
            this.goalGraphic.Position = new PointF(this.goalNode.Position.X, this.goalGraphic.SpriteSheetSource.Height / 2);
            
            // Temp stuff start -------
            Astar<MapNode>.ConnectNodes(TileEngine.Instance.NodeMap);

            TestPath = Astar<MapNode>.GeneratePath(this.startNode, this.goalNode);

            StandardTower testTower = new StandardTower();
            testTower.Position = TileEngine.Instance.NodeMap[2, TileEngine.TilesY - 1].Position;

            this.Width = (TileEngine.TilesX * TileEngine.TilesOnSpriteSize) + 50;
            this.Height = (TileEngine.TilesY * TileEngine.TilesOnSpriteSize) + 50;

            // Temp stuff end -------

            Time.SetDeltaTime();
        }

        private void GameDisplay_Load(object sender, EventArgs e)
        {
            // Create a thread object, passing in the MainLoop method
            gameThread = new Thread(this.MainLoop);
            
            // Start the render thread
            gameThread.Start();
        }

        public static new Point MousePosition
        {
            get
            {
                return instance.PointToClient(Cursor.Position);
            }
        }

        private void MainLoop()
        {
            while (true)
            {
                // Set the frame delta time
                Time.SetDeltaTime();
                
                // Create buffer if it don't exist already
                if (context == null)
                {
                    context = BufferedGraphicsManager.Current;
                    this.buffer = context.Allocate(CreateGraphics(), this.DisplayRectangle);
                }

                // Clear the screen with the forms back color
                this.buffer.Graphics.Clear(this.BackColor);

                // Buffer the map tiles
                TileEngine.Instance.RenderTiles(this.buffer.Graphics);

                // Draw the enemies path
                this.buffer.Graphics.DrawLines(new Pen(Color.Red, 5), TestPath.Select(mapNode => mapNode.Position).ToArray());

                // Run logic
                GameLogic.Instance.RunLogic(this.buffer.Graphics);

                // Write fps
                this.WriteFps();

                this.SetMouseCursorPosition();

                //// DrawNeighbors();

                // Transfer buffer to display - aka back/front buffer swapping 
                this.buffer.Render();
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

        private void WriteFps()
        {
            // Only update fps every 0.5 seconds
            if (DateTime.Now.Subtract(this.lastFpsUpdate).TotalSeconds > 0.5f)
            {
                this.currentFps = (int)(1 / Time.DeltaTime);

                this.lastFpsUpdate = DateTime.Now;
            }


            this.buffer.Graphics.DrawString("FPS: " + this.currentFps.ToString(), new Font("Arial", 12), new SolidBrush(Color.Green), 0, 0);
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

        private void SetUpTestPath()
        {
            TestPath = new List<MapNode>
                {
                    TileEngine.Instance.NodeMap[0, TileEngine.TilesY - 1],
                    TileEngine.Instance.NodeMap[1, TileEngine.TilesY - 1],
                    TileEngine.Instance.NodeMap[1, TileEngine.TilesY - 2],
                    TileEngine.Instance.NodeMap[2, TileEngine.TilesY - 2],
                    TileEngine.Instance.NodeMap[2, TileEngine.TilesY - 3],
                    TileEngine.Instance.NodeMap[3, TileEngine.TilesY - 3],
                    TileEngine.Instance.NodeMap[3, TileEngine.TilesY - 4],
                    TileEngine.Instance.NodeMap[4, TileEngine.TilesY - 4],
                    TileEngine.Instance.NodeMap[4, TileEngine.TilesY - 5],
                };
        }

        
    }
}
