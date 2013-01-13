// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IEnemyWave.cs" company="Backdoor Fun">
//   © 2013
// </copyright>
// <summary>
//   The EnemyWave interface, implemented by all wave types.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace BlobDefense.WaveSpawner
{
    using BlobDefense.Enemies;

    /// <summary>
    /// The EnemyWave interface, implemented by all wave types.
    /// </summary>
    internal interface IEnemyWave
    {
        Enemy SpawnEnemy();
    }
}
