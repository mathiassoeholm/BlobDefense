using System;
using System.Collections.Generic;

namespace BlobDefense
{
    using System.Drawing;
    using System.IO;
    using System.Xml.Serialization;
    using System.Linq;

    public class TileEngine 
    {
        public const int TilesX = 15;
        public const int TilesY = 15;

        public const int TilesOnSpriteSheetX = 8;
        public const int TilesOnSpriteSheetY = 8;

        public const int TilesOnSpriteSize = 32;
        
        /// <summary>
        /// Gets the fifferent kinds of tiles, each kind of tile is automatically added by the Tile class.
        /// </summary>
        public List<Tile> tilesTypes { get; private set; } 

        /// <summary>
        /// A two dimensional map of tile indexes, rendered to the screen.
        /// </summary>
        private MapNode[,] nodeMap = new MapNode[TilesX, TilesY];

        public TileEngine()
        {
            this.tilesTypes = new List<Tile>(TilesOnSpriteSheetX * TilesOnSpriteSheetY);
            
            for (int y = 0; y < TilesOnSpriteSheetY; y++)
            {
                for (int x = 0; x < TilesOnSpriteSheetX; x++)
                {
                    // Create new tile object
                    var tile = new Tile();
                    tile.SpriteSheetSource = new RectangleF(x * TilesOnSpriteSize, y * TilesOnSpriteSize, TilesOnSpriteSize, TilesOnSpriteSize);

                    // Add the new tile type
                    this.AddTileType(tile);
                }
            }
        }

        public void AddTileType(Tile tile)
        {
            this.tilesTypes.Add(tile);
        }

        public void GenerateRandomMap()
        {
            var random = new Random();

            for (int x = 0; x < this.nodeMap.GetLength(0); x++)
            {
                for (int y = 0; y < this.nodeMap.GetLength(1); y++)
                {
                    this.nodeMap[x, y] = new MapNode { TileType = random.Next(0, this.tilesTypes.Count) };
                }
            }
        }

        public void ChangeTile(int x, int y, uint tileType)
        {
            if (tilesTypes.Count <= tileType)
            {
                return;
            }
            
            this.nodeMap[y, x].TileType = (int)tileType;
            
        }

        public void RenderTiles(Graphics context, int offsetLeft = 0, int offsetTop = 0)
        {
            for (int x = 0; x < this.nodeMap.GetLength(0); x++)
            {
                for (int y = 0; y < this.nodeMap.GetLength(1); y++)
                {
                    // Render the tile
                    this.tilesTypes[this.nodeMap[y, x].TileType].Render(x + offsetLeft, y + offsetTop, context);
                }
            }
        }

        public void SaveMapToXml()
        {
            // Set node positions 
            for (int y = 0; y < this.nodeMap.GetLength(1); y++)
            {
                for (int x = 0; x < this.nodeMap.GetLength(0); x++)
                {
                    this.nodeMap[x, y].X = (x * TilesOnSpriteSize) + (TilesOnSpriteSize / 2);
                    this.nodeMap[x, y].Y = (y * TilesOnSpriteSize) + (TilesOnSpriteSize / 2);
                }
            }
            
            // Flatten 2d array
            var map = new MapNode[this.nodeMap.Length];
            int i = 0;
            foreach (MapNode node in this.nodeMap)
            {
                map[i] = node;
                i++;
            }

            var serializer = new XmlSerializer(typeof(MapNode[]));

            using (TextWriter textWriter = new StreamWriter(@"C:\BlobDefense\TileMap.xml"))
            {
                serializer.Serialize(textWriter, map);
            }
        }

        public void LoadMapFromXml()
        {
            MapNode[] map;

            var deserializer = new XmlSerializer(typeof(MapNode[]));

            using (TextReader textReader = new StreamReader(@"C:\BlobDefense\TileMap.xml"))
            {
                map = (MapNode[])deserializer.Deserialize(textReader);
            }

            // Convert array to 2D array
            int i = 0;
            for (int y = 0; y < this.nodeMap.GetLength(1); y++)
            {
                for (int x = 0; x < this.nodeMap.GetLength(0); x++)
                {
                    if (i >= map.Length)
                    {
                        return;
                    }
                    
                    this.nodeMap[y, x] = map[i];
                    i++;
                }
            }
        }
    }
}
