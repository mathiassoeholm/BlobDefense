namespace BlobDefense
{
    using System.Drawing;
    using System.Threading;
    using System.Windows.Forms;
    
    /// <summary>
    /// The windows form which is responsible of displaying the game.
    /// </summary>
    public partial class GameDisplay : Form
    {
        BufferedGraphicsContext context;
        BufferedGraphics buffer;

        private TileEngine tileEngine;

        public GameDisplay()
        {
            this.InitializeComponent();

            this.Width = TileEngine.TilesX * TileEngine.TilesOnSpriteSize + 50;
            this.Height = TileEngine.TilesY * TileEngine.TilesOnSpriteSize + 50;

            tileEngine = new TileEngine();
            tileEngine.LoadMapFromXml();

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

                // Transfer buffer to display - aka back/front buffer swaping 
                buffer.Render();
            }
        }
    }
}
