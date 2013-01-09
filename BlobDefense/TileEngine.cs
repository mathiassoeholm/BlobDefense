using System;
using System.Collections.Generic;

namespace BlobDefense
{
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Xml.Serialization;
    using System.Linq;

    public class TileEngine : Singleton<TileEngine>
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
        public MapNode[,] NodeMap { get; private set; }

        /// <summary>
        /// Prevents a default instance of the <see cref="TileEngine"/> class from being created.
        /// </summary>
        private TileEngine()
        {
            this.NodeMap = new MapNode[TilesX, TilesY];
            this.tilesTypes = new List<Tile>(TilesOnSpriteSheetX * TilesOnSpriteSheetY);

            for (int y = 0; y < TilesOnSpriteSheetY; y++)
            {
                for (int x = 0; x < TilesOnSpriteSheetX; x++)
                {
                    // Create new tile object
                    var tile = new Tile();
                    tile.SpriteSheetSource = new Rectangle(x * TilesOnSpriteSize, y * TilesOnSpriteSize, TilesOnSpriteSize, TilesOnSpriteSize);

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

            for (int x = 0; x < this.NodeMap.GetLength(0); x++)
            {
                for (int y = 0; y < this.NodeMap.GetLength(1); y++)
                {
                    this.NodeMap[x, y] = new MapNode { TileType = random.Next(0, this.tilesTypes.Count) };
                }
            }
        }

        public void SetAllTilesTo(int i)
        {
            var random = new Random();

            for (int x = 0; x < this.NodeMap.GetLength(0); x++)
            {
                for (int y = 0; y < this.NodeMap.GetLength(1); y++)
                {
                    this.NodeMap[x, y] = new MapNode { TileType = i };
                }
            }
        }

        public void ToggleBlockedTile(int x, int y)
        {
            this.NodeMap[x, y].IsBlocked = !this.NodeMap[x, y].IsBlocked;
        }

        public void ChangeTile(int x, int y, uint tileType)
        {
            if (tilesTypes.Count <= tileType)
            {
                return;
            }
            
            this.NodeMap[x, y].TileType = (int)tileType;
            
        }

        public void RenderTiles(Graphics context, int offsetLeft = 0, int offsetTop = 0)
        {
            for (int y = 0; y < this.NodeMap.GetLength(1); y++)
            {
                for (int x = 0; x < this.NodeMap.GetLength(0); x++)
                {
                    // Render the tile
                    this.tilesTypes[this.NodeMap[x, y].TileType].Render(x + offsetLeft, y + offsetTop, context);
                }
            }
        }

        public void SaveMapToXml()
        {
            // If the file does not exist, create one
            if (!Directory.Exists(@"BlobDefense"))
            {
                Directory.CreateDirectory(@"\BlobDefense");
            }
            
            // Set node positions 
            for (int y = 0; y < this.NodeMap.GetLength(1); y++)
            {
                for (int x = 0; x < this.NodeMap.GetLength(0); x++)
                {
                    this.NodeMap[x, y].X = (x * TilesOnSpriteSize) + (TilesOnSpriteSize / 2);
                    this.NodeMap[x, y].Y = (y * TilesOnSpriteSize) + (TilesOnSpriteSize / 2);
                }
            }
            
            // Flatten 2d array
            var map = new MapNode[this.NodeMap.Length];
            int i = 0;
            foreach (MapNode node in this.NodeMap)
            {
                map[i] = node;
                i++;
            }

            var serializer = new XmlSerializer(typeof(MapNode[]));

            using (TextWriter textWriter = new StreamWriter(@"TileMap.xml"))
            {
                serializer.Serialize(textWriter, map);
            }
        }

        public void LoadMapFromXml()
        {
            // If the file does not exist, generate a random map and save it
            if (!File.Exists(@"TileMap.xml"))
            {
                this.GenerateRandomMap();
                this.SaveMapToXml();
            }

            MapNode[] map;

            var deserializer = new XmlSerializer(typeof(MapNode[]));

            using (TextReader textReader = new StreamReader(@"TileMap.xml"))
            {
                map = (MapNode[])deserializer.Deserialize(textReader);
            }

            // Convert array to 2D array
            int i = 0;
            for (int y = 0; y < this.NodeMap.GetLength(1); y++)
            {
                for (int x = 0; x < this.NodeMap.GetLength(0); x++)
                {
                    if (i >= map.Length)
                    {
                        return;
                    }
                    
                    this.NodeMap[y, x] = map[i];
                    i++;
                }
            }
        }
    }
}
