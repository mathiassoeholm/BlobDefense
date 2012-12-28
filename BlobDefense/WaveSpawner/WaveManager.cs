using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlobDefense.WaveSpawner
{
    using System.Threading;

    internal class WaveManager : Singleton<WaveManager>
    {
        private const int MillisBetweenEachEnemy = 1000;

        private Timer enemySpawnTimer;

        private List<IEnemyWave> waves;

        private int currentWave;

        /// <summary>
        /// Prevents a default instance of the <see cref="WaveManager"/> class from being created.
        /// </summary>
        private WaveManager()
        {            
            this.waves = new List<IEnemyWave>()
                {
                    new Wave<StandardEnemy>(10),
                    new Wave<StandardEnemy, PikachuEnemy>(10, 5),
                    new Wave<StandardEnemy, PikachuEnemy>(5, 10),
                    new Wave<FastEnemy>(10),
                    new Wave<StandardEnemy, FastEnemy>(10, 10),
                    new Wave<StandardEnemy>(30),
                    new Wave<StandardEnemy, FastEnemy, PikachuEnemy>(10, 10, 10),
                    new Wave<FastEnemy>(15),
                    new Wave<StandardEnemy, FastEnemy, PikachuEnemy>(12, 12, 12),
                };
        }

        public void StartWave()
        {
            // Return if a wave is already active
            if (this.enemySpawnTimer != null)
            {
                return;
            }

            this.enemySpawnTimer = new Timer(this.SpawnEnemy, null, 0, MillisBetweenEachEnemy);
        }

        private void StopWave()
        {
            this.enemySpawnTimer.Dispose();
            this.enemySpawnTimer = null;
            this.currentWave++;
        }

        private void SpawnEnemy(object state)
        {
            // Spawn an enemy, but check if it is the last one
            if (this.waves[this.currentWave].SpawnEnemy() == null)
            {
                this.StopWave();
            }
        }
    }
}
