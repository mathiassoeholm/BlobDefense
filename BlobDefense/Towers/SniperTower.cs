using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlobDefense.Towers
{
    using System.Drawing;

    [Serializable]
    class SniperTower : Tower
    {
        public SniperTower()
        {
            // Assign tower properties
            this.ShootCooldown = 3;
            this.ShootRange = 200;
            this.AttackDamage = GameSettings.SniperTower_AttackDamage;
            this.BuildPrice = GameSettings.SniperTower_BuildPrice;
            this.UpgradePrice = GameSettings.SniperTower_UpgradePrice;
            
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
            this.AttackDamage *= GameSettings.SniperTower_AttackDamage_Upgrade;
            this.ShootRange *= GameSettings.SniperTower_ShootRange_Upgrade;
            this.ShootCooldown *= GameSettings.SniperTower_ShootCoolDown_Upgrade;
            this.UpgradePrice *= GameSettings.SniperTower_UpgradePrice_Upgrade;
        }
        
        protected override void ShootTarget()
        {
            // Create a new projectile
            new SniperProjectile(this.EnemyTarget, this.Position, this.AttackDamage, this);
            
            base.ShootTarget();
        }
    }
}
