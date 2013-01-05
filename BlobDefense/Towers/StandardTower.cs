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
            this.ShootCooldown = GameSettings.StandardTower_CoolDown;
            this.ShootRange = GameSettings.StandardTower_ShootRange;
            this.AttackDamage = GameSettings.StandardTower_AttackDamage;
            this.BuildPrice = GameSettings.StandardTower_BuildPrice;
            this.UpgradePrice = GameSettings.StandardTower_UpgradePrice;
            
            this.SpriteSheetSource = new Rectangle(0, 178, 25, 29);
            
            this.IdleAnimation = new Animation(
                fps: 3,
                animatedObject: this,
                frameCount: 4,
                firstFrame: new Rectangle(0, 178, 25, 29),
                tileDirection: TileDirection.Right);

            this.CurrentAnimation = this.IdleAnimation;
        }

        public override void Upgrade()
        {
            base.Upgrade();

            // Increment properties
            this.AttackDamage *= GameSettings.StandardTower_AttackDamage_Upgrade;
            this.ShootRange *= GameSettings.StandardTower_ShootRange_Upgrade;
            this.ShootCooldown *= GameSettings.StandardTower_ShootCoolDown_Upgrade;
            this.UpgradePrice *= GameSettings.StandardTower_UpgradePrice_Upgrade;
        }
        
        protected override void ShootTarget()
        {
            // Create a new projectile
            new StandardProjectile(this.EnemyTarget, this.Position, this.AttackDamage, this);
            
            base.ShootTarget();
        }
    }
}
