// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Tower.cs" company="Backdoor Fun">
//   © 2013
// </copyright>
// <summary>
//   Defines the base for all towers.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace BlobDefense.Towers
{
    using System;
    using System.Linq;

    using BlobDefense.Enemies;

    using Extensions;

    /// <summary>
    /// Defines the base for all towers.
    /// </summary>
    [Serializable]
    internal abstract class Tower : GameObject, IUpdateBehaviour, IAnimated
    {
        /// <summary>
        /// The point in time where we fired a projectile last.
        /// </summary>
        private DateTime lastShotFired;

        /// <summary>
        /// The enemy to shoot.
        /// </summary>
        [NonSerialized]
        private Enemy enemyTarget;

        /// <summary>
        /// Gets or sets the amount of kills performed by this tower.
        /// </summary>
        public int Kills { get; set; }

        /// <summary>
        /// Gets the towers current level
        /// </summary>
        public int Level { get; private set; }

        /// <summary>
        /// Gets or sets the upgrade price.
        /// </summary>
        public int UpgradePrice { get; protected set; }

        /// <summary>
        /// Gets or sets the initial build price.
        /// </summary>
        public int BuildPrice { get; protected set; }

        /// <summary>
        /// Gets or sets the current animation.
        /// </summary>
        public Animation CurrentAnimation { get; protected set; }

        /// <summary>
        /// Gets or sets the shoot cool down in seconds.
        /// </summary>
        public float ShootCooldown { get; set; }

        /// <summary>
        /// Gets or sets the shoot range in pixels.
        /// </summary>
        public float ShootRange { get; set; }

        /// <summary>
        /// Gets or sets the attack damage.
        /// </summary>
        public float AttackDamage { get; set; }

        /// <summary>
        /// Gets or sets the the enemy to shoot.
        /// </summary>
        protected Enemy EnemyTarget
        {
            get
            {
                return this.enemyTarget;
            }

            set
            {
                this.enemyTarget = value;
            }
        }

        /// <summary>
        /// Gets or sets the idle animation.
        /// </summary>
        protected Animation IdleAnimation { get; set; }

        /// <summary>
        /// Called once per frame.
        /// </summary>
        public void Update()
        {
            // Check if another shot is ready
            if (DateTime.Now.Subtract(this.lastShotFired).TotalSeconds > this.ShootCooldown / Time.TimeScale)
            {
                // Check if we do have a EnemyTarget
                if (AllGameObjects.Contains(this.EnemyTarget) && this.Position.DistanceTo(this.EnemyTarget.Position) <= this.ShootRange)
                {
                    this.ShootTarget();
                }
                else
                {
                    float shortestSqrDistance = float.MaxValue;

                    // Find nearest enemy
                    foreach (Enemy enemy in AllGameObjects.OfType<Enemy>())
                    {
                        float sqrDistance = this.Position.SqrDistanceTo(enemy.Position);

                        if (sqrDistance < shortestSqrDistance)
                        {
                            this.EnemyTarget = enemy;
                            shortestSqrDistance = sqrDistance;
                        }
                    }

                    // Check actual distance to this enemy
                    if (Math.Sqrt(shortestSqrDistance) <= this.ShootRange)
                    {
                        this.ShootTarget();
                    }
                }
            }
        }

        /// <summary>
        /// Destroys the tower and frees the node.
        /// </summary>
        public override void Destroy()
        {
            // Free the node
            TileEngine.Instance.NodeMap[(int)this.Position.X / TileEngine.TilesOnSpriteSize, (int)this.Position.Y / TileEngine.TilesOnSpriteSize].IsBlocked = false;

            // Recreate path
            GameLogic.Instance.TryCreateNewPath();
            
            // Deselect tower
            EventManager.Instance.DeselectedTower.SafeInvoke();
            
            // Destroy the tower
            base.Destroy();
        }

        /// <summary>
        /// Increments the towers level.
        /// </summary>
        public virtual void Upgrade()
        {
            this.Level++;        
        }

        /// <summary>
        /// Runs the current animation.
        /// </summary>
        public void RunAnimation()
        {
            this.CurrentAnimation.RunAnimation();
        }

        /// <summary>
        /// Invokes the shoot event and resets the cool down timer.
        /// </summary>
        protected virtual void ShootTarget()
        {
            // Invoke shoot event
            EventManager.Instance.TowerShot.SafeInvoke(this);
            
            // Set time for last shot, used to check cooldown
            this.lastShotFired = DateTime.Now;
        }
    }
}
