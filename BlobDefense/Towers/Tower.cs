using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Extensions;

namespace BlobDefense.Towers
{
    internal abstract class Tower : GameObject, IUpdateBehaviour, IAnimated
    {
        private DateTime lastShotFired;

        public Animation CurrentAnimation { get; protected set; }

        protected Animation IdleAnimation { get; set; }

        protected Enemy EnemyTarget { get; set; }

        protected float ShootCooldown { get; set; }

        protected float ShootRange { get; set; }

        protected Projectile Projectile { get; set; }

        public void Update()
        {
            // Check if another shot is ready
            if (DateTime.Now.Subtract(this.lastShotFired).TotalSeconds > this.ShootCooldown)
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

        protected virtual void ShootTarget()
        {
            // Set time for last shot, used to check cooldown
            this.lastShotFired = DateTime.Now;
        }

        public void RunAnimation()
        {
            this.CurrentAnimation.RunAnimation();
        }
    }
}
