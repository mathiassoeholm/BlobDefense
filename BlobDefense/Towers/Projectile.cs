using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlobDefense.Towers
{
    internal class Projectile : MovingGameObject, IUpdateBehaviour
    {
        protected readonly Enemy enemyTarget;
        private readonly float attackDamage;
        private readonly Tower towerSource;

        public Projectile(Enemy enemy, float attackDamage, Tower towerSource)
        {
            this.enemyTarget = enemy;
            this.CurrentTarget = this.enemyTarget.Position;
            this.attackDamage = attackDamage;
            this.towerSource = towerSource;

            this.DepthLevel = 5;
        }

        protected override void OnTargetHit()
        {
            // Give damage to enemy
            if (this.enemyTarget.TakeDamage(this.attackDamage))
            {
                // Plus one kill to the tower
                this.towerSource.Kills++;
            }

            // Destroy this projectile
            this.Destroy();
        }

        public void Update()
        {
            if (GameObject.AllGameObjects.Contains(this.enemyTarget))
            {
                this.CurrentTarget = this.enemyTarget.Position;

                this.Rotation =
                    (float)
                    Math.Atan2(
                        this.Position.Y - this.enemyTarget.Position.Y, this.Position.X - this.enemyTarget.Position.X)
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
