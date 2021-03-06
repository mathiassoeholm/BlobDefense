﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MapMaker
{
    using System.Threading;

    using BlobDefense;

    public partial class Form1 : Form
    {
        BufferedGraphicsContext context;
        BufferedGraphics buffer;

        private Point currentSelection;

        private Image selectionOverlay;
        private Image closedOverlay;

        private bool lockSelection;

        private int selectedTileType;

        public Form1()
        {
            InitializeComponent();

            this.selectionOverlay = new Bitmap(@"Images/SelectionOverlay.png");
            this.closedOverlay = new Bitmap(@"Images/ClosedOverlay.png");

            TileEngine.Instance.LoadMapFromXml();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
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

                buffer.Graphics.Clear(this.BackColor);

                // Use buffer for rendering of game
                TileEngine.Instance.RenderTiles(buffer.Graphics, 1, 2);

                TileEngine.Instance.TilesTypes[selectedTileType].Render(17, 11, buffer.Graphics);

                foreach (MapNode mapNode in TileEngine.Instance.NodeMap)
                {
                    if (mapNode.IsBlocked)
                    {
                        buffer.Graphics.DrawImage(closedOverlay, mapNode.X + 16, mapNode.Y + 16 + 32, 32, 32);
                    }
                }

                buffer.Graphics.DrawImage(selectionOverlay, 32 + currentSelection.X * 32, 64 + currentSelection.Y * 32, 32, 32);

                if (Keyboard.IsKeyDown(Keys.Right))
                {
                    if (!lockSelection)
                    {
                        this.currentSelection = new Point(
                            Math.Min(TileEngine.TilesX - 1, this.currentSelection.X + 1), currentSelection.Y);
                    }

                    lockSelection = true;
                }
                else if (Keyboard.IsKeyDown(Keys.Left))
                {
                    if (!lockSelection)
                    {
                        this.currentSelection = new Point(Math.Max(0, this.currentSelection.X - 1), currentSelection.Y);
                    }

                    lockSelection = true;
                }
                else if (Keyboard.IsKeyDown(Keys.Up))
                {
                    if (!lockSelection)
                    {
                        this.currentSelection = new Point(this.currentSelection.X, Math.Max(0, currentSelection.Y - 1));
                    }

                    lockSelection = true;
                }
                else if (Keyboard.IsKeyDown(Keys.Down))
                {
                    if (!lockSelection)
                    {
                        this.currentSelection = new Point(this.currentSelection.X, Math.Min(TileEngine.TilesY - 1, this.currentSelection.Y + 1));
                    }

                    lockSelection = true;
                }
                else if (Keyboard.IsKeyDown(Keys.W))
                {
                    if (!lockSelection)
                    {
                        // Change tile type
                        this.selectedTileType++;
                        this.selectedTileType %= TileEngine.Instance.TilesTypes.Count;
                    }

                    lockSelection = true;
                }
                else if (Keyboard.IsKeyDown(Keys.Q))
                {
                    if (!lockSelection)
                    {
                        // Change tile type
                        this.selectedTileType--;

                        if (this.selectedTileType < 0)
                        {
                            this.selectedTileType = TileEngine.Instance.TilesTypes.Count - 1;
                        }
                    }

                    lockSelection = true;
                }
                else if (Keyboard.IsKeyDown(Keys.Space))
                {
                    if (!lockSelection)
                    {
                        // Assign tile
                        TileEngine.Instance.ChangeTile(currentSelection.X, currentSelection.Y, (uint)selectedTileType);
                    }

                    lockSelection = true;
                }
                else if (Keyboard.IsKeyDown(Keys.C))
                {
                    if (!lockSelection)
                    {
                        // Assign tile
                        TileEngine.Instance.ToggleBlockedTile(currentSelection.X, currentSelection.Y);
                    }

                    lockSelection = true;
                }
                else
                {
                    lockSelection = false;
                }

                // Transfer buffer to display - aka back/front buffer swaping 
                buffer.Render();
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TileEngine.Instance.SaveMapToXml();
        }

        
    }
}
