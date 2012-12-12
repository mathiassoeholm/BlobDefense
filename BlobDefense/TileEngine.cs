using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlobDefense
{
    using System.Windows.Forms;

    public class TileEngine : Singleton<TileEngine>
    {
        private const int TilesX = 10;
        private const int TilesY = 10;
        
        /// <summary>
        /// Different kinds of tiles, each kind of tile is automatically added by the Tile class.
        /// </summary>
        private List<Tile> tilesTypes = new List<Tile>(); 

        /// <summary>
        /// A two dimensional map of tiles, rendered to the screen.
        /// </summary>
        private int[,] tileMap = new int[TilesX, TilesY]; 
        
        public void RenderTiles()
        {
           
        }
    }
}
