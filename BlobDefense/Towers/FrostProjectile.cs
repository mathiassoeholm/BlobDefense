// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FrostProjectile.cs" company="Backdoor Fun">
//   © 2013
// </copyright>
// <summary>
//   Defines a projectile with slow effect, used by the frost tower.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace BlobDefense.Towers
{
    using System.Drawing;

    using BlobDefense.Enemies;

    /// <summary>
    /// Defines a projectile with slow effect, used by the frost tower.
    /// </summary>
    internal class FrostProjectile : Projectile
    {
        /// <summary>
        /// The time in seconds to slow the enemy.
        /// </summary>
        private readonly float slowDuration;

        /// <summary>
        /// The effect of the slow, the enemies speed is multiplied by this value.
        /// </summary>
        private readonly float slowAmount;

        /// <summary>
        /// Initializes a new instance of the <see cref="FrostProjectile"/> class.
        /// </summary>
        /// <param name="enemy"> The enemy to follow. </param>
        /// <param name="spawnPosition"> The spawn position. </param>
        /// <param name="attackDamage"> The attack damage. </param>
        /// <param name="slowDuration"> The time in seconds to slow the enemy. </param>
        /// <param name="slowAmount"> The effect of the slow, the enemies speed is multiplied by this value. </param>
        /// <param name="towerSource"> The tower that spawned this projectile. </param>
        public FrostProjectile(Enemy enemy, PointF spawnPosition, float attackDamage, float slowDuration, float slowAmount, Tower towerSource)
            : base(enemy, attackDamage, towerSource, spawnPosition)
        {
            // Set tower properties
            this.slowDuration = slowDuration;
            this.slowAmount = slowAmount;
            this.SpriteSheetSource = new Rectangle(100, 187, 13, 6);
            this.MoveSpeed = 200;
        }

        /// <summary>
        /// Called when hitting a target, slows the enemy.
        /// </summary>
        protected override void OnTargetHit()
        {
            // Slow the enemy
            this.EnemyTarget.Slow(this.slowAmount, this.slowDuration);
            
            base.OnTargetHit();
        }
    }
}
