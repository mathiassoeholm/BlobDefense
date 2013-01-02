using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlobDefense.Towers
{
    using System.Drawing;

    internal class StandardTower : Tower
    {
        public StandardTower()
        {
            // Assign tower properties
            this.ShootCooldown = 0.8f;
            this.ShootRange = 200;
            this.AttackDamage = 10;
            
            this.SpriteSheetSource = new Rectangle(0, 178, 25, 29);
            
            this.IdleAnimation = new Animation(
                fps: 3,
                animatedObject: this,
                frameCount: 4,
                firstFrame: new Rectangle(0, 178, 25, 29),
                tileDirection: TileDirection.Right);

            this.CurrentAnimation = this.IdleAnimation;
        }
        
        protected override void ShootTarget()
        {
            var projectile = new StandardProjectile(this.EnemyTarget, this.Position, this.AttackDamage);
            
            base.ShootTarget();
        }
    }
}
