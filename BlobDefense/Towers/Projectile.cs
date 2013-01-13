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

        public Projectile(Enemy enemy, float attackDamage, Tower towerSource, PointF spawnPosition)
        {
            this.Position = spawnPosition;
            this.EnemyTarget = enemy;
            this.CurrentTarget = this.EnemyTarget.Position;
            this.attackDamage = attackDamage;
            this.towerSource = towerSource;

            this.DepthLevel = 5;
        }

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

        public void Update()
        {
            if (GameObject.AllGameObjects.Contains(this.EnemyTarget))
            {
                this.CurrentTarget = this.EnemyTarget.Position;

                this.Rotation =
                    (float)
                    Math.Atan2(
                        this.Position.Y - this.EnemyTarget.Position.Y, this.Position.X - this.EnemyTarget.Position.X)
                    * 57.324f; // 180 / Pi
            }
            //else
            //{
            //    this.Destroy();
            //}

            this.Move();
        }
    }
}
