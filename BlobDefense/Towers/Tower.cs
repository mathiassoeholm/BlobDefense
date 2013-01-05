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

        public int Kills { get; set; }

        public int Level { get; private set; }

        public int UpgradePrice { get; protected set; }

        public int BuildPrice { get; protected set; }

        public Animation CurrentAnimation { get; protected set; }

        protected Animation IdleAnimation { get; set; }

        protected Enemy EnemyTarget { get; set; }

        public float ShootCooldown { get; set; }

        public float ShootRange { get; set; }

        public float AttackDamage { get; set; }

        protected Projectile Projectile { get; set; }

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

        public override void Destroy()
        {
            // Free the node
            TileEngine.Instance.NodeMap[
                (int)this.Position.X / TileEngine.TilesOnSpriteSize, (int)this.Position.Y / TileEngine.TilesOnSpriteSize]
                .IsBlocked = false;

            // Recreate path
            GameLogic.Instance.TryCreateNewPath();
            
            // Deselect tower
            EventManager.Instance.DeselectedTower.SafeInvoke();
            
            base.Destroy();
        }

        public virtual void Upgrade()
        {
            this.Level++;

            // Reselect the tower
            EventManager.Instance.TowerWasSelected.SafeInvoke(this);
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
