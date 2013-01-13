// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TileEngine.cs" company="Backdoor Fun">
//   © 2013
// </copyright>
// <summary>
//   Contains the node map and tile information, responsible for rendering the background.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace BlobDefense
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.IO;
    using System.Xml.Serialization;

    /// <summary>
    /// Contains the node map and tile information, responsible for rendering the background.
    /// </summary>
    public class TileEngine : Singleton<TileEngine>
    {
        /// <summary>
        /// The amount of tiles on x.
        /// </summary>
        public const int TilesX = 15;

        /// <summary>
        /// The amount of tiles on y.
        /// </summary>
        public const int TilesY = 15;

        /// <summary>
        /// The amount of tiles on the sprite sheet in x.
        /// </summary>
        public const int TilesOnSpriteSheetX = 8;

        /// <summary>
        /// The amount of tiles on the sprite sheet in y.
        /// </summary>
        public const int TilesOnSpriteSheetY = 8;

        /// <summary>
        /// the width and height of a single tile on the sprite sheet in pixels.
        /// </summary>
        public const int TilesOnSpriteSize = 32;

        /// <summary>
        /// Prevents a default instance of the <see cref="TileEngine"/> class from being created.
        /// </summary>
        private TileEngine()
        {
            this.NodeMap = new MapNode[TilesX, TilesY];
            this.TilesTypes = new List<Tile>(TilesOnSpriteSheetX * TilesOnSpriteSheetY);

            // Add all possible tile types
            for (int y = 0; y < TilesOnSpriteSheetY; y++)
            {
                for (int x = 0; x < TilesOnSpriteSheetX; x++)
                {
                    // Create new tile object
                    var tile = new Tile();
                    tile.SpriteSheetSource = new Rectangle(x * TilesOnSpriteSize, y * TilesOnSpriteSize, TilesOnSpriteSize, TilesOnSpriteSize);

                    // Add the new tile type
                    this.TilesTypes.Add(tile);
                }
            }
        }

        /// <summary>
        /// Gets the different kinds of tiles, each kind of tile is automatically added.
        /// </summary>
        public List<Tile> TilesTypes { get; private set; }

        /// <summary>
        /// Gets or sets a two dimensional map of tile indexes, rendered to the screen.
        /// </summary>
        public MapNode[,] NodeMap { get; set; }

        /// <summary>
        /// Generates a completely random map from the tile types.
        /// </summary>
        public void GenerateRandomMap()
        {
            var random = new Random();

            for (int x = 0; x < this.NodeMap.GetLength(0); x++)
            {
                for (int y = 0; y < this.NodeMap.GetLength(1); y++)
                {
                    this.NodeMap[x, y] = new MapNode { TileType = random.Next(0, this.TilesTypes.Count) };
                }
            }
        }

        /// <summary>
        /// Sets the entire map to a specific kind of tile.
        /// </summary>
        /// <param name="i">
        /// The tile ID.
        /// </param>
        public void SetAllTilesTo(int i)
        {
            for (int x = 0; x < this.NodeMap.GetLength(0); x++)
            {
                for (int y = 0; y < this.NodeMap.GetLength(1); y++)
                {
                    this.NodeMap[x, y] = new MapNode { TileType = i };
                }
            }
        }

        /// <summary>
        /// Toggles the is blocked property of a specified node.
        /// </summary>
        /// <param name="x">
        /// The x node map index.
        /// </param>
        /// <param name="y">
        /// The y node map index.
        /// </param>
        public void ToggleBlockedTile(int x, int y)
        {
            this.NodeMap[x, y].IsBlocked = !this.NodeMap[x, y].IsBlocked;
        }

        /// <summary>
        /// Changes the a specified nodes tile type.
        /// </summary>
        /// <param name="x">
        /// The x node map index.
        /// </param>
        /// <param name="y">
        /// The y node map index.
        /// </param>
        /// <param name="tileType">
        /// The new tile type ID.
        /// </param>
        public void ChangeTile(int x, int y, uint tileType)
        {
            if (this.TilesTypes.Count <= tileType)
            {
                return;
            }
            
            this.NodeMap[x, y].TileType = (int)tileType;
        }

        /// <summary>
        /// Renders the node map to the screen, with each nodes tile type.
        /// </summary>
        /// <param name="graphics">
        /// The graphics object used to render the tiles.
        /// </param>
        /// <param name="offsetLeft">
        /// The optional left offset in pixels.
        /// </param>
        /// <param name="offsetTop">
        /// The optional top offset in pixels.
        /// </param>
        public void RenderTiles(Graphics graphics, int offsetLeft = 0, int offsetTop = 0)
        {
            if (this.NodeMap == null)
            {
                return;
            }
            
            for (int y = 0; y < this.NodeMap.GetLength(1); y++)
            {
                for (int x = 0; x < this.NodeMap.GetLength(0); x++)
                {
                    // Render the tile
                    this.TilesTypes[this.NodeMap[x, y].TileType].Render(x + offsetLeft, y + offsetTop, graphics);
                }
            }
        }

        /// <summary>
        /// Saves the map to a XML file.
        /// </summary>
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

        /// <summary>
        /// Loads the node map from an XML file.
        /// </summary>
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
