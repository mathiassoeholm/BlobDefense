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
        BufferedGraphics buffer;

        private TileEngine tileEngine;

        public static List<MapNode> testPath;

        private Enemy testEnemy;

        public GameDisplay()
        {
            this.InitializeComponent();

            // Temp stuff start -------
            this.Width = TileEngine.TilesX * TileEngine.TilesOnSpriteSize + 50;
            this.Height = TileEngine.TilesY * TileEngine.TilesOnSpriteSize + 50;

            tileEngine = new TileEngine();
            tileEngine.LoadMapFromXml();

            this.SetUpTestPath();

            this.testEnemy = new StandardEnemy();

            // Temp stuff end -------

            // Create a thread object, passing in the RenderLoop method
            var renderThread = new Thread(this.RenderLoop);

            // Start the render thread
            renderThread.Start();
        }

        private void RenderLoop()
        {
            while (true)
            {
                // Create buffer if it don't exist already
                if (context == null)
                {
                    context = BufferedGraphicsManager.Current;
                    buffer = context.Allocate(CreateGraphics(), this.DisplayRectangle);
                }

                buffer.Graphics.Clear(Color.White);

                if (Keyboard.IsKeyDown(Keys.B))
                {
                    tileEngine.GenerateRandomMap();
                }


                // Use buffer for rendering of game
                tileEngine.RenderTiles(buffer.Graphics);

                // Render all gameobjects
                foreach (GameObject gameObject in GameObject.AllGameObjects)
                {
                    // Update behaviours
                    if (gameObject is IUpdateBehaviour)
                    {
                        (gameObject as IUpdateBehaviour).Update();
                    }

                    // Don't render tiles, they are handled elsewhere
                    if (!(gameObject is Tile))
                    {
                        gameObject.Render(buffer.Graphics);
                    }
                }

                this.buffer.Graphics.DrawLines(new Pen(Color.Red, 5), testPath.Select(mapNode => mapNode.Position).ToArray());

                // Transfer buffer to display - aka back/front buffer swaping 
                buffer.Render();
            }
        }

        private void SetUpTestPath()
        {
            testPath = new List<MapNode>
                {
                    tileEngine.NodeMap[0, TileEngine.TilesY - 1],
                    tileEngine.NodeMap[1, TileEngine.TilesY - 1],
                    tileEngine.NodeMap[1, TileEngine.TilesY - 2],
                    tileEngine.NodeMap[2, TileEngine.TilesY - 2],
                    tileEngine.NodeMap[2, TileEngine.TilesY - 3],
                    tileEngine.NodeMap[3, TileEngine.TilesY - 3],
                    tileEngine.NodeMap[3, TileEngine.TilesY - 4],
                    tileEngine.NodeMap[4, TileEngine.TilesY - 4],
                    tileEngine.NodeMap[4, TileEngine.TilesY - 5],

                };
        }
    }
}
