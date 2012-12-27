namespace BlobDefense
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Threading;
    using System.Windows.Forms;
    using System.Linq;
    /// <summary>
    /// The windows form which is responsible of displaying the game.
    /// </summary>
    public partial class GameDisplay : Form
    {
        BufferedGraphicsContext context;

        private BufferedGraphics buffer;

        public static GameDisplay Instance { get; private set; }

        public static BufferedGraphics Buffer
        {
            get
            {
                return Instance.buffer;
            }
        }

        public static List<MapNode> testPath;

        private Enemy testEnemy;

        public GameDisplay()
        {
            this.InitializeComponent();

            Instance = this;

            TileEngine.Instance.LoadMapFromXml();

            // Temp stuff start -------
            this.Width = TileEngine.TilesX * TileEngine.TilesOnSpriteSize + 50;
            this.Height = TileEngine.TilesY * TileEngine.TilesOnSpriteSize + 50;

            this.SetUpTestPath();

            this.testEnemy = new StandardEnemy();

            // Temp stuff end -------

            // Create a thread object, passing in the MainLoop method
            var gameThread = new Thread(this.MainLoop);

            // Start the render thread
            gameThread.Start();
        }

        private void MainLoop()
        {
            while (true)
            {
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
                this.buffer.Graphics.DrawLines(new Pen(Color.Red, 5), testPath.Select(mapNode => mapNode.Position).ToArray());

                // Run logic
                GameLogic.Instance.RunLogic();

                // Transfer buffer to display - aka back/front buffer swapping 
                this.buffer.Render();
            }
        }

        private void SetUpTestPath()
        {
            testPath = new List<MapNode>
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
