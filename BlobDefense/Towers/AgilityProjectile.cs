// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AgilityProjectile.cs" company="Backdoor Fun">
//   © 2013
// </copyright>
// <summary>
//   Defines the AgilityProjectile type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace BlobDefense.Towers
{
    using System.Drawing;

    using BlobDefense.Enemies;

    /// <summary>
    /// Defines a very fast projectile, used by the agility tower.
    /// </summary>
    internal class AgilityProjectile : Projectile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AgilityProjectile"/> class.
        /// </summary>
        /// <param name="enemy"> The enemy to follow. </param>
        /// <param name="spawnPosition"> The spawn position. </param>
        /// <param name="attackDamage"> The attack damage. </param>
        /// <param name="towerSource"> The tower that spawned this projectile. </param>
        public AgilityProjectile(Enemy enemy, PointF spawnPosition, float attackDamage, Tower towerSource)
            : base(enemy, attackDamage, towerSource, spawnPosition)
        {
            // Set tower properties
            this.SpriteSheetSource = new Rectangle(100, 194, 14, 5);
            this.MoveSpeed = 500;
        }
    }
}
