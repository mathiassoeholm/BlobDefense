// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StandardProjectile.cs" company="Backdoor Fun">
//   © 2013
// </copyright>
// <summary>
//   Defines a projectile with normal speed, used by the standard tower.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace BlobDefense.Towers
{
    using System.Drawing;

    using BlobDefense.Enemies;

    /// <summary>
    /// Defines a projectile with normal speed, used by the standard tower.
    /// </summary>
    internal class StandardProjectile : Projectile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StandardProjectile"/> class.
        /// </summary>
        /// <param name="enemy"> The enemy to follow. </param>
        /// <param name="spawnPosition"> The spawn position. </param>
        /// <param name="attackDamage"> The attack damage. </param>
        /// <param name="towerSource"> The tower that spawned this projectile. </param>
        public StandardProjectile(Enemy enemy, PointF spawnPosition, float attackDamage, Tower towerSource)
            : base(enemy, attackDamage, towerSource, spawnPosition)
        {
            this.SpriteSheetSource = new Rectangle(100, 179, 14, 7);
            this.MoveSpeed = 200;
        }
    }
}
