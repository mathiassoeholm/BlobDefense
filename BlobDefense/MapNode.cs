using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlobDefense
{
    using System.Drawing;


    public class MapNode
    {
        public MapNode()
        {
            this.Position = new Point();
        }

        public int TileType { get; set; }

        public bool IsWall { get; set; }

        public int X
        {
            get
            {
                return this.Position.X;
            }
            set
            {
                this.Position = new Point(value, this.Position.Y);
            }
        }

        public int Y
        {
            get
            {
                return this.Position.Y;
            }
            set
            {
                this.Position = new Point(this.Position.X, value);
            }
        }

        public Point Position { get; set; }


        public bool IsWalkable(Object unused)
        {
            return !IsWall;
        }
    }
}
