using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlobDefense
{
    using BlobDefense.Enemies;

    interface IEnemyWave
    {
        Enemy SpawnEnemy();
    }
}
