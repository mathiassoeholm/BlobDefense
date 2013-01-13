// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SniperProjectile.cs" company="Backdoor Fun">
//   © 2013
// </copyright>
// <summary>
//   Defines a slow projectile, used by the sniper tower.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace BlobDefense.Towers
{
    using System.Drawing;

    using BlobDefense.Enemies;

    /// <summary>
    /// Defines a slow projectile, used by the sniper tower.
    /// </summary>
    internal class SniperProjectile : Projectile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SniperProjectile"/> class.
        /// </summary>
        /// <param name="enemy"> The enemy to follow. </param>
        /// <param name="spawnPosition"> The spawn position. </param>
        /// <param name="attackDamage"> The attack damage. </param>
        /// <param name="towerSource"> The tower that spawned this projectile. </param>
        public SniperProjectile(Enemy enemy, PointF spawnPosition, float attackDamage, Tower towerSource)
            : base(enemy, attackDamage, towerSource, spawnPosition)
        {
            // Set tower properties
            this.SpriteSheetSource = new Rectangle(102, 200, 9, 5);
            this.MoveSpeed = 100;
        }
    }
}
