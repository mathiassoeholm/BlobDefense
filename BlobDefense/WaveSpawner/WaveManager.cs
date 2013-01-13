// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WaveManager.cs" company="Backdoor Fun">
//   © 2013
// </copyright>
// <summary>
//   Handles everything to do with wave spawning.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace BlobDefense.WaveSpawner
{
    using System;
    using System.Collections.Generic;
    using System.Threading;

    using BlobDefense.Enemies;

    using Extensions;

    /// <summary>
    /// Handles everything to do with wave spawning.
    /// </summary>
    internal class WaveManager : Singleton<WaveManager>
    {
        /// <summary>
        /// The milliseconds between each enemy spawns.
        /// </summary>
        private const int MillisBetweenEachEnemy = 1000;

        /// <summary>
        /// The list of custom waves.
        /// </summary>
        private List<IEnemyWave> waves;

        /// <summary>
        /// The timer used to spawn enemies.
        /// </summary>
        private Timer enemySpawnTimer;

        /// <summary>
        /// The amount of enemies to spawn, this is used for the random waves.
        /// </summary>
        private int enemiesToSpawn = 10;

        /// <summary>
        /// The current wave.
        /// </summary>
        private int currentWave = -1;

        /// <summary>
        /// Enemies spawned in a wave so far, this is used for the random waves.
        /// </summary>
        private int enemiesSpawned;

        /// <summary>
        /// The milliseconds between each enemy spawns.
        /// </summary>
        private int millisBetweenEachEnemy = MillisBetweenEachEnemy;

        /// <summary>
        /// Prevents a default instance of the <see cref="WaveManager"/> class from being created.
        /// </summary>
        private WaveManager()
        {
        }

        /// <summary>
        /// Gets the enemy difficulty factor.
        /// </summary>
        public float EnemyDifficulty { get; private set; }

        /// <summary>
        /// Gets or sets the current wave.
        /// </summary>
        public int CurrentWave
        {
            get
            {
                return this.currentWave;
            }

            set
            {
                this.currentWave = value;
            }
        }

        /// <summary>
        /// Initializes settings for the wave manager.
        /// </summary>
        public void InitializeWaveManager()
        {
            this.currentWave = -1;
            this.EnemyDifficulty = 1;
            this.enemiesSpawned = 0;

            // Set up custom waves
            this.waves = new List<IEnemyWave>
                {
                    new Wave<StandardEnemy>(10),
                    new Wave<StandardEnemy, PikachuEnemy>(10, 5),
                    new Wave<StandardEnemy, PikachuEnemy>(10, 10),
                    new Wave<FastEnemy>(15),
                    new Wave<StandardEnemy, FastEnemy>(10, 15),
                    new Wave<StandardEnemy>(40),
                    new Wave<StandardEnemy, FastEnemy, PikachuEnemy>(10, 20, 10),
                    new Wave<FastEnemy>(15),
                    new Wave<FastEnemy, PikachuEnemy>(25, 25),
                    new Wave<Boss>(1),
                };
        }

        /// <summary>
        /// Starts a wave of enemies, if a current wave is not already spawning.
        /// </summary>
        public void StartWave()
        {
            // Return if a wave is already active
            if (this.enemySpawnTimer != null)
            {
                return;
            }

            this.currentWave++;
            this.enemiesToSpawn = this.currentWave + 10;
            this.enemiesSpawned = 0;

            // Make harder
            this.EnemyDifficulty = GameSettings.EnemyDifficulityIncrease * (this.currentWave + 1);

            EventManager.Instance.WaveStarted.SafeInvoke();

            this.enemySpawnTimer = new Timer(this.SpawnEnemy, null, 0, this.millisBetweenEachEnemy);
        }

        /// <summary>
        /// Scales the interval between enemies, to match the time scale.
        /// </summary>
        /// <param name="timeScale">
        /// The new time scale.
        /// </param>
        public void ScaleSpawnInterval(int timeScale)
        {
            // Change the interval between enemies
            this.millisBetweenEachEnemy = MillisBetweenEachEnemy / timeScale;

            // Change time for the timer if it is not null
            if (this.enemySpawnTimer != null)
            {
                this.enemySpawnTimer.Change(0, MillisBetweenEachEnemy / timeScale);
            }
        }

        /// <summary>
        /// Stops the current wave from spawning.
        /// </summary>
        public void StopWave()
        {
            if (this.enemySpawnTimer != null)
            {
                this.enemySpawnTimer.Dispose();
                this.enemySpawnTimer = null;
            }
        }

        /// <summary>
        /// Spawns a single enemy from the current wave.
        /// </summary>
        /// <param name="state">
        /// The state of the timer.
        /// </param>
        private void SpawnEnemy(object state)
        {
            // Check if this is the last custom wave
            if (this.currentWave >= this.waves.Count)
            {
                var random = new Random();
                int randomNumber = random.Next(0, 100);

                this.enemiesSpawned++;

                if (randomNumber < 5)
                {
                    new Boss();
                }
                else if (randomNumber < 30)
                {
                    new FastEnemy();
                }
                else if (randomNumber < 65)
                {
                    new PikachuEnemy();
                }
                else
                {
                    new StandardEnemy();
                }
                
                // Check if this is the last enemy
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
