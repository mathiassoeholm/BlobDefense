using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlobDefense.Towers
{
    internal class Projectile : MovingGameObject, IUpdateBehaviour
    {
        private readonly Enemy enemyTarget;
        
        public Projectile(Enemy enemy)
        {
            this.enemyTarget = enemy;
        }

        protected override void OnTargetHit()
        {
            // TODO: Implement give enemy damage
            
            // Destroy enemy (TEMP)
            this.enemyTarget.Destroy();

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

            this.Move();
        }
    }
}
