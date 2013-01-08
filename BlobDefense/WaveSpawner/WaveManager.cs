using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlobDefense.WaveSpawner
{
    using System.Threading;

    using BlobDefense.Enemies;

    using Extensions;

    internal class WaveManager : Singleton<WaveManager>
    {
        private const float EnemyDifficulityIncrease = 1.2f;
        private const int MillisBetweenEachEnemy = 1000;

        private readonly List<IEnemyWave> waves;

        private Timer enemySpawnTimer;

        private int currentWave = -1;

        private float enemyDifficulity = 1;

        // Used for random waves
        private int enemiesToSpawn = 10;
        private int enemiesSpawned;

        private int millisBetweenEachEnemy = MillisBetweenEachEnemy;

        /// <summary>
        /// Prevents a default instance of the <see cref="WaveManager"/> class from being created.
        /// </summary>
        private WaveManager()
        {            
            this.waves = new List<IEnemyWave>()
                {
                    new Wave<Boss>(1),
                    new Wave<StandardEnemy>(10),
                    new Wave<StandardEnemy, PikachuEnemy>(10, 5),
                    new Wave<StandardEnemy, PikachuEnemy>(5, 10),
                    new Wave<FastEnemy>(10),
                    new Wave<StandardEnemy, FastEnemy>(10, 10),
                    new Wave<StandardEnemy>(30),
                    new Wave<StandardEnemy, FastEnemy, PikachuEnemy>(10, 10, 10),
                    new Wave<FastEnemy>(15),
                    new Wave<StandardEnemy, FastEnemy, PikachuEnemy>(12, 12, 12),
                    new Wave<PikachuEnemy, FastEnemy>(3, 5),
                };
        }

        public int CurrentWave
        {
            get
            {
                return this.currentWave + 1;
            }
        }

        public void StartWave()
        {
            // Return if a wave is already active
            if (this.enemySpawnTimer != null)
            {
                return;
            }

            this.currentWave++;
            this.enemiesToSpawn++;
            this.enemiesSpawned = 0;

            // Make harder
            this.enemyDifficulity += EnemyDifficulityIncrease;

            EventManager.Instance.WaveStarted.SafeInvoke();

            this.enemySpawnTimer = new Timer(this.SpawnEnemy, null, 0, this.millisBetweenEachEnemy);
        }

        public void ScaleSpawnInterval(int timeScale)
        {
            this.millisBetweenEachEnemy = MillisBetweenEachEnemy / timeScale;

            if (this.enemySpawnTimer != null)
            {
                this.enemySpawnTimer.Change(0, MillisBetweenEachEnemy / timeScale);
            }
        }

        private void StopWave()
        {
            this.enemySpawnTimer.Dispose();
            this.enemySpawnTimer = null;
        }

        private void SpawnEnemy(object state)
        {
            // Check if this is the last custom wave
            if (this.currentWave >= this.waves.Count)
            {
                Random random = new Random();
                int randomNumber = random.Next(0, 100);

                this.enemiesSpawned++;

                if (randomNumber < 10)
                {
                    new FastEnemy(this.enemyDifficulity);
                }
                else if(randomNumber < 50)
                {
                    new PikachuEnemy(this.enemyDifficulity);
                }
                else
                {
                    new StandardEnemy(this.enemyDifficulity);
                }
                
                if (this.enemiesSpawned >= this.enemiesToSpawn)
                {
                    this.StopWave();
                }
            }
            else
            {
                // Stop the wave if there are no enemies left
                if (this.waves[this.currentWave].SpawnEnemy() == null)
                {
                    this.StopWave();
                }
            }
        }
    }
}
