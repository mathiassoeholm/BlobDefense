﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlobDefense
{
    using System.Drawing;
    using System.Windows.Forms;

    public class TileEngine
    {
        private const int TilesX = 40;
        private const int TilesY = 40;
        
        /// <summary>
        /// Different kinds of tiles, each kind of tile is automatically added by the Tile class.
        /// </summary>
        private readonly List<Tile> tilesTypes = new List<Tile>(); 

        /// <summary>
        /// A two dimensional map of tile indexes, rendered to the screen.
        /// </summary>
        private int[,] tileMap = new int[TilesX, TilesY];

        private static TileEngine instance;
        public static TileEngine Instance
        {
            get
            {
                return instance ?? (instance = new TileEngine());
            }
        }

        private TileEngine()
        {
            Tile tempTile1 = new Tile();
            tempTile1.SpriteSheetSource = new RectangleF(0, 240, 16, 16);
            this.AddTileType(tempTile1);

            Tile tempTile2 = new Tile();
            tempTile2.SpriteSheetSource = new RectangleF(16, 240, 16, 16);
            this.AddTileType(tempTile2);

            Tile tempTile3 = new Tile();
            tempTile3.SpriteSheetSource = new RectangleF(32, 240, 16, 16);
            this.AddTileType(tempTile3);

            Tile tempTile4 = new Tile();
            tempTile4.SpriteSheetSource = new RectangleF(48, 240, 16, 16);
            this.AddTileType(tempTile4);

            Tile tempTile5 = new Tile();
            tempTile5.SpriteSheetSource = new RectangleF(64, 240, 16, 16);
            this.AddTileType(tempTile5);

            Tile tempTile6 = new Tile();
            tempTile6.SpriteSheetSource = new RectangleF(80, 240, 16, 16);
            this.AddTileType(tempTile6);

            Tile tempTile7 = new Tile();
            tempTile7.SpriteSheetSource = new RectangleF(96, 240, 16, 16);
            this.AddTileType(tempTile7);

            Tile tempTile8 = new Tile();
            tempTile8.SpriteSheetSource = new RectangleF(112, 240, 16, 16);
            this.AddTileType(tempTile8);

            Tile tempTile9 = new Tile();
            tempTile9.SpriteSheetSource = new RectangleF(128, 240, 16, 16);
            this.AddTileType(tempTile9);

        }

        public void AddTileType(Tile tile)
        {
            this.tilesTypes.Add(tile);
        }

        public void GenerateRandomMap()
        {
            Random random = new Random();
            
            for (int x = 0; x < this.tileMap.GetLength(0); x++)
            {
                for (int y = 0; y < this.tileMap.GetLength(1); y++)
                {
                    this.tileMap[x, y]++;

                    // Assign a random tile index
                    this.tileMap[x, y] = this.tileMap[x, y] % this.tilesTypes.Count;
                        //random.Next(0, this.tilesTypes.Count);
                }
            }
        }

        public void RenderTiles(Graphics context)
        {
            for (int x = 0; x < this.tileMap.GetLength(0); x++)
            {
                for (int y = 0; y < this.tileMap.GetLength(1); y++)
                {
                    // Render the tile
                    this.tilesTypes[this.tileMap[x, y]].Render(x, y, context);
                }
            }
        }
    }
}