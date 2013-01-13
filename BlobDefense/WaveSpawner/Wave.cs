// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Wave.cs" company="Backdoor Fun">
//   © 2013
// </copyright>
// <summary>
//   Defines three types of generic waves.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace BlobDefense.WaveSpawner
{
    using BlobDefense.Enemies;

    /// <summary>
    /// Defines a wave, with a single type of enemy.
    /// </summary>
    /// <typeparam name="T1">
    /// The type of enemy in this wave.
    /// </typeparam>
    internal class Wave<T1> : IEnemyWave
        where T1 : Enemy, new()
    {
        /// <summary>
        /// The amount of enemies to spawn.
        /// </summary>
        private int amountOfEnemyOne;

        /// <summary>
        /// Initializes a new instance of the <see cref="Wave{T1}"/> class.
        /// </summary>
        /// <param name="amountOfEnemyOne">
        /// The amount of enemies to spawn.
        /// </param>
        public Wave(int amountOfEnemyOne = 0)
        {
            this.amountOfEnemyOne = amountOfEnemyOne;
        }

        /// <summary>
        /// Spawns a single enemy.
        /// </summary>
        /// <returns>
        /// The enemy which was spawned, null if there are no enemies left.
        /// </returns>
        public Enemy SpawnEnemy()
        {
            if (this.amountOfEnemyOne > 0)
            {
                this.amountOfEnemyOne--;
                return new T1();
            }

            // End of wave return null
            return null;
        }
    }

    /// <summary>
    /// Defines a wave, with two types of enemies.
    /// </summary>
    /// <typeparam name="T1">
    /// The first type of enemy to spawn.
    /// </typeparam>
    /// <typeparam name="T2">
    /// The second type of enemy to spawn.
    /// </typeparam>
    internal class Wave<T1, T2> : IEnemyWave
        where T1 : Enemy, new()
        where T2 : Enemy, new()
    {
        /// <summary>
        /// The amount of enemy one to spawn.
        /// </summary>
        private int amountOfEnemyOne;

        /// <summary>
        /// The amount of enemy two to spawn.
        /// </summary>
        private int amountOfEnemyTwo;

        /// <summary>
        /// Initializes a new instance of the <see cref="Wave{T1,T2}"/> class.
        /// </summary>
        /// <param name="amountOfEnemyOne">
        /// The amount of enemy one.
        /// </param>
        /// <param name="amountOfEnemyTwo">
        /// The amount of enemy two.
        /// </param>
        public Wave(int amountOfEnemyOne = 0, int amountOfEnemyTwo = 0)
        {
            this.amountOfEnemyOne = amountOfEnemyOne;
            this.amountOfEnemyTwo = amountOfEnemyTwo;
        }

        /// <summary>
        /// Spawns a single enemy.
        /// </summary>
        /// <returns>
        /// The enemy which was spawned, null if there are no enemies left.
        /// </returns>
        public Enemy SpawnEnemy()
        {
            if (this.amountOfEnemyOne > 0)
            {
                this.amountOfEnemyOne--;
                return new T1();
            }

            if (this.amountOfEnemyTwo > 0)
            {
                this.amountOfEnemyTwo--;
                return new T2();
            }

            // End of wave return null
            return null;
        }
    }

    /// <summary>
    /// Defines a wave, with three types of enemies.
    /// </summary>
    /// <typeparam name="T1">
    /// The first type of enemy to spawn.
    /// </typeparam>
    /// <typeparam name="T2">
    /// The second type of enemy to spawn.
    /// </typeparam>
    /// <typeparam name="T3">
    /// The third type of enemy to spawn.
    /// </typeparam>
    internal class Wave<T1, T2, T3> : IEnemyWave
        where T1 : Enemy, new()
        where T2 : Enemy, new()
        where T3 : Enemy, new()
    {
        /// <summary>
        /// The amount of enemy one.
        /// </summary>
        private int amountOfEnemyOne;

        /// <summary>
        /// The amount of enemy two.
        /// </summary>
        private int amountOfEnemyTwo;

        /// <summary>
        /// The amount of enemy three.
        /// </summary>
        private int amountOfEnemyThree;

        /// <summary>
        /// Initializes a new instance of the <see cref="Wave{T1,T2,T3}"/> class.
        /// </summary>
        /// <param name="amountOfEnemyOne">
        /// The amount of enemy one.
        /// </param>
        /// <param name="amountOfEnemyTwo">
        /// The amount of enemy two.
        /// </param>
        /// <param name="amountOfEnemyThree">
        /// The amount of enemy three.
        /// </param>
        public Wave(int amountOfEnemyOne = 0, int amountOfEnemyTwo = 0, int amountOfEnemyThree = 0)
        {
            this.amountOfEnemyOne = amountOfEnemyOne;
            this.amountOfEnemyTwo = amountOfEnemyTwo;
            this.amountOfEnemyThree = amountOfEnemyThree;
        }

        /// <summary>
        /// Spawns a single enemy.
        /// </summary>
        /// <returns>
        /// The enemy which was spawned, null if there are no enemies left.
        /// </returns>
        public Enemy SpawnEnemy()
        {
            if (this.amountOfEnemyOne > 0)
            {
                this.amountOfEnemyOne--;
                return new T1();
            }

            if (this.amountOfEnemyTwo > 0)
            {
                this.amountOfEnemyTwo--;
                return new T2();
            }

            if (this.amountOfEnemyThree > 0)
            {
                this.amountOfEnemyThree--;
                return new T3();
            }

            // End of wave return null
            return null;
        }
    }
}