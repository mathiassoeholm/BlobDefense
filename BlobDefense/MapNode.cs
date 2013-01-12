using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlobDefense
{
    using System.Drawing;
    using AStar;

    [Serializable]
    public class MapNode : IPathNode<MapNode>, IComparable<MapNode>
    {
        public MapNode()
        {
            this.Position = new Point();
            this.Neighbors = new List<MapNode>(4);
        }

        public int TileType { get; set; }

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

        public bool IsClosed { get; set; }

        public bool IsBlocked { get; set; }

        public Point Position { get; set; }

        public float GScore { get; set; }

        public float HScore { get; set; }

        public List<MapNode> Neighbors { get; set; }

        public MapNode Parent { get; set; }

        public float FScore
        {
            get
            {
                return this.GScore + HScore;
            }
        }

        public void ConnectTo(MapNode node)
        {
            if (!this.Neighbors.Contains(node))
            {
                this.Neighbors.Add(node);
            }
        }

        public int CompareTo(MapNode other)
        {
            if (this.FScore < other.FScore)
            {
                return 1;
            }

            return -1;
        }

    }
}
