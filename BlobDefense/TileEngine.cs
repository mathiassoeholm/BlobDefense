using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlobDefense
{
    using System.Drawing;
    using System.Windows.Forms;

    public class TileEngine : Singleton<TileEngine>
    {
        private const int TilesX = 10;
        private const int TilesY = 10;
        
        /// <summary>
        /// Different kinds of tiles, each kind of tile is automatically added by the Tile class.
        /// </summary>
        private readonly List<Tile> tilesTypes = new List<Tile>(); 

        /// <summary>
        /// A two dimensional map of tile indexes, rendered to the screen.
        /// </summary>
        private readonly int[,] tileMap = new int[TilesX, TilesY];


        private TileEngine()
        {
            Tile tempTile1 = new Tile();
            tempTile1.SpriteSheetSource = new RectangleF(0, 0, 16, 16);

            Tile tempTile2 = new Tile();
            tempTile1.SpriteSheetSource = new RectangleF(16, 0, 16, 16);
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
                    // Assign a random tile index
                    this.tileMap[x, y] = random.Next(0, this.tilesTypes.Count);
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
