using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlobDefense
{
    using SettlersEngine;

    public class MapNode : IPathNode<Object>
    {

        public int TileType { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public bool IsWall { get; set; }

        public bool IsWalkable(Object unused)
        {
            return !IsWall;
        }
    }
}
