using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlobDefense.Towers
{
    using System.Drawing;

    class SniperTower : Tower
    {
        public SniperTower()
        {
            // Assign tower properties
            this.ShootCooldown = GameSettings.AgilityTower_CoolDown;
            this.ShootRange = GameSettings.AgilityTower_ShootRange;
            this.AttackDamage = GameSettings.AgilityTower_AttackDamage;
            this.BuildPrice = GameSettings.AgilityTower_BuildPrice;
            this.UpgradePrice = GameSettings.AgilityTower_UpgradePrice;
            
            this.SpriteSheetSource = new Rectangle(144, 100, 20, 31);
            
            this.IdleAnimation = new Animation(
                fps: 6,
                animatedObject: this,
                frameCount: 4,
                firstFrame: this.SpriteSheetSource,
                tileDirection: TileDirection.Down);

            this.CurrentAnimation = this.IdleAnimation;
        }

        public override void Upgrade()
        {
            base.Upgrade();

            // Increment properties
            this.AttackDamage *= GameSettings.AgilityTower_AttackDamage_Upgrade;
            this.ShootRange *= GameSettings.AgilityTower_ShootRange_Upgrade;
            this.ShootCooldown *= GameSettings.AgilityTower_ShootCoolDown_Upgrade;
            this.UpgradePrice *= GameSettings.AgilityTower_UpgradePrice_Upgrade;
        }
        
        protected override void ShootTarget()
        {
            // Create a new projectile
            new AgilityProjectile(this.EnemyTarget, this.Position, this.AttackDamage, this);
            
            base.ShootTarget();
        }
    }
}
