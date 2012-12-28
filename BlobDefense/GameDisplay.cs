namespace BlobDefense
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Threading;
    using System.Windows.Forms;
    using System.Linq;

    using BlobDefense.WaveSpawner;

    /// <summary>
    /// The windows form which is responsible of displaying the game.
    /// </summary>
    public partial class GameDisplay : Form
    {
        private BufferedGraphicsContext context;

        private BufferedGraphics buffer;

        public static List<MapNode> testPath;

        private int currentFps;

        private DateTime lastFpsUpdate;

        private MouseCursor mouseCursor;

        public GameDisplay()
        {
            this.InitializeComponent();

            Cursor.Hide();

            mouseCursor = new MouseCursor();

            TileEngine.Instance.LoadMapFromXml();

            // Temp stuff start -------
            this.Width = TileEngine.TilesX * TileEngine.TilesOnSpriteSize + 50;
            this.Height = TileEngine.TilesY * TileEngine.TilesOnSpriteSize + 50;

            this.SetUpTestPath();

            Time.SetDeltaTime();

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
                this.buffer.Graphics.DrawLines(new Pen(Color.Red, 5), testPath.Select(mapNode => mapNode.Position).ToArray());

                // Run logic
                GameLogic.Instance.RunLogic(this.buffer.Graphics);

                // Write fps
                this.WriteFps();

                this.SetMouseCursorPosition();
                this.mouseCursor.Render(this.buffer.Graphics);

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
                this.mouseCursor.SetPosition(new Point(Cursor.Position.X - this.Left, Cursor.Position.Y - this.Top));
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

        private void timer1_Tick(object sender, EventArgs e)
        {

        }

        
    }
}
