// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Projectile.cs" company="Backdoor Fun">
//   © 2013
// </copyright>
// <summary>
//   Defines the base for all projectiles.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace BlobDefense.Towers
{
    using System;
    using System.Drawing;

    using BlobDefense.Enemies;

    using Extensions;

    /// <summary>
    /// Defines the base for all projectiles.
    /// </summary>
    internal class Projectile : MovingGameObject, IUpdateBehaviour
    {
        /// <summary>
        /// The enemy to follow.
        /// </summary>
        protected readonly Enemy EnemyTarget;

        /// <summary>
        /// The attack damage.
        /// </summary>
        private readonly float attackDamage;

        /// <summary>
        /// The tower that spawned this projectile.
        /// </summary>
        private readonly Tower towerSource;

        /// <summary>
        /// Initializes a new instance of the <see cref="Projectile"/> class.
        /// </summary>
        /// <param name="enemy"> The enemy to follow. </param>
        /// <param name="attackDamage"> The attack damage. </param>
        /// <param name="towerSource"> The tower that spawned this projectile. </param>
        /// /// <param name="spawnPosition"> The spawn position. </param>
        public Projectile(Enemy enemy, float attackDamage, Tower towerSource, PointF spawnPosition)
        {
            // Assign members
            this.Position = spawnPosition;
            this.EnemyTarget = enemy;
            this.CurrentTarget = this.EnemyTarget.Position;
            this.attackDamage = attackDamage;
            this.towerSource = towerSource;

            // Set the depth level of projectiles
            this.DepthLevel = GameSettings.ProjectileDepthLevel;
        }

        /// <summary>
        /// Called once per frame, follows the enemy.
        /// </summary>
        public void Update()
        {
            if (AllGameObjects.Contains(this.EnemyTarget))
            {
                this.CurrentTarget = this.EnemyTarget.Position;

                this.Rotation =
                    (float)
                    Math.Atan2(
                        this.Position.Y - this.EnemyTarget.Position.Y, this.Position.X - this.EnemyTarget.Position.X)
                        .RadiansToDegrees();
            }

            this.Move();
        }

        /// <summary>
        /// Called when hitting a target, makes sure the enemy takes damage and the projectile is destroyed.
        /// </summary>
        protected override void OnTargetHit()
        {
            // Give damage to enemy
            if (this.EnemyTarget.TakeDamage(this.attackDamage))
            {
                // Plus one kill to the tower
                this.towerSource.Kills++;
            }

            // Destroy this projectile
            this.Destroy();
        }
    }
}
