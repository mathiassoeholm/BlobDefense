using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AStar
{
    public interface IPathNode<T>
    {
        int X { get; set; }

        int Y { get; set; }

        bool IsClosed { get; set; }

        bool IsBlocked { get; set; }

        float GScore { get; set; }

        float HScore { get; set; }

        List<T> Neighbors { get; set; }

        T Parent { get; set; }
    }
}
