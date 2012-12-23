using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlobDefense
{
    using System.Drawing;
    using System.IO;
    using System.Windows.Forms;
    using System.Xml;
    using System.Xml.Schema;
    using System.Xml.Serialization;

    public class TileEngine 
    {
        public const int TilesX = 10;
        public const int TilesY = 10;
        
        /// <summary>
        /// Different kinds of tiles, each kind of tile is automatically added by the Tile class.
        /// </summary>
        private List<Tile> tilesTypes = new List<Tile>(); 

        /// <summary>
        /// A two dimensional map of tile indexes, rendered to the screen.
        /// </summary>
        private int[,] tileMap = new int[TilesX, TilesY];

        public TileEngine()
        {
            //Tile tempTile1 = new Tile();
            //tempTile1.SpriteSheetSource = new RectangleF(0, 0, 32, 32);
            //this.AddTileType(tempTile1);

            //Tile tempTile2 = new Tile();
            //tempTile2.SpriteSheetSource = new RectangleF(32, 0, 32, 32);
            //this.AddTileType(tempTile2);

            //Tile tempTile3 = new Tile();
            //tempTile3.SpriteSheetSource = new RectangleF(0, 0, 32, 32);
            //this.AddTileType(tempTile3);

            //this.SaveTileTypesToXml();

            this.LoadTileTypesfromXml();
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
                    this.tileMap[x, y] = random.Next(0, this.tilesTypes.Count);
                }
            }
        }

        public void ChangeTile(int x, int y, uint tileType)
        {
            if (tilesTypes.Count <= tileType)
            {
                return;
            }
            
            tileMap[y, x] = (int)tileType;
            
        }

        public void RenderTiles(Graphics context, int offsetLeft = 0, int offsetTop = 0)
        {
            for (int x = 0; x < this.tileMap.GetLength(0); x++)
            {
                for (int y = 0; y < this.tileMap.GetLength(1); y++)
                {
                    // Render the tile
                    this.tilesTypes[this.tileMap[y, x]].Render(x + offsetLeft, y + offsetTop, context);
                }
            }
        }

        private void SaveTileTypesToXml()
        {
            var serializer = new XmlSerializer(typeof(List<Tile>));

            using (TextWriter textWriter = new StreamWriter(@"C:\BlobDefense\TileTypes.xml"))
            {
                serializer.Serialize(textWriter, this.tilesTypes);
            }
        }

        private void LoadTileTypesfromXml()
        {
            var deserializer = new XmlSerializer(typeof(List<Tile>));

            using (TextReader textReader = new StreamReader(@"C:\BlobDefense\TileTypes.xml"))
            {
                this.tilesTypes = (List<Tile>)deserializer.Deserialize(textReader);
            }
        }

        private void SaveMapToXml()
        {
            var serializer = new XmlSerializer(typeof(int[,]));

            using (TextWriter textWriter = new StreamWriter(@"C:\BlobDefense\TileMap.xml"))
            {
                serializer.Serialize(textWriter, this.tileMap);
            }
        }

        private void LoadMapFromXml()
        {
            var deserializer = new XmlSerializer(typeof(int[,]));

            using (TextReader textReader = new StreamReader(@"C:\BlobDefense\TileMap.xml"))
            {
                this.tileMap = (int[,])deserializer.Deserialize(textReader);
            }
        }
    }
}
