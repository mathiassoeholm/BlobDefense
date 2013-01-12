using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlobDefense.Towers
{
    using System.Drawing;

    [Serializable]
    internal class FrostTower : Tower
    {
        /// <summary>
        /// How long the enemy is slowed in seconds.
        /// </summary>
        private float slowDuration;
        private readonly float slowAmount;
        
        public FrostTower()
        {
            // Assign tower properties
            this.ShootCooldown = GameSettings.FrostTower_CoolDown;
            this.ShootRange = GameSettings.FrostTower_ShootRange;
            this.AttackDamage = GameSettings.FrostTower_AttackDamage;
            this.BuildPrice = GameSettings.FrostTower_BuildPrice;
            this.UpgradePrice = GameSettings.FrostTower_UpgradePrice;
            this.slowDuration = GameSettings.FrostTower_SlowDuration;
            this.slowAmount = GameSettings.FrostTower_SlowAmount;
            
            this.SpriteSheetSource = new Rectangle(0, 208, 26, 26);
            
            this.IdleAnimation = new Animation(
                fps: 5,
                animatedObject: this,
                frameCount: 4,
                firstFrame: this.SpriteSheetSource,
                tileDirection: TileDirection.Right);

            this.CurrentAnimation = this.IdleAnimation;
        }

        public override void Upgrade()
        {
            base.Upgrade();

            // Increment properties
            this.AttackDamage *= GameSettings.FrostTower_AttackDamage_Upgrade;
            this.ShootRange *= GameSettings.FrostTower_ShootRange_Upgrade;
            this.ShootCooldown *= GameSettings.FrostTower_ShootCoolDown_Upgrade;
            this.UpgradePrice *= GameSettings.FrostTower_UpgradePrice_Upgrade;
            this.slowDuration *= GameSettings.FrostTower_SlowDuration_Upgrade;
        }
        
        protected override void ShootTarget()
        {
            // Create a new projectile
            new FrostProjectile(this.EnemyTarget, this.Position, this.AttackDamage, this.slowDuration, this.slowAmount, this);
            
            base.ShootTarget();
        }
    }
}
