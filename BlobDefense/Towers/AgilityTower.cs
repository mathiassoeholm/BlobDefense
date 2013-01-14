// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AgilityTower.cs" company="Backdoor Fun">
//   © 2013
// </copyright>
// <summary>
//   Defines a tower with low attack damage and low cool down.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace BlobDefense.Towers
{
    using System;
    using System.Drawing;

    /// <summary>
    /// Defines a tower with low attack damage and low cool down.
    /// </summary>
    [Serializable]
    internal class AgilityTower : Tower
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AgilityTower"/> class.
        /// </summary>
        public AgilityTower()
        {
            // Assign tower properties
            this.ShootCooldown = GameSettings.AgilityTower_CoolDown;
            this.ShootRange = GameSettings.AgilityTower_ShootRange;
            this.AttackDamage = GameSettings.AgilityTower_AttackDamage;
            this.BuildPrice = GameSettings.AgilityTower_BuildPrice;
            this.UpgradePrice = GameSettings.AgilityTower_UpgradePrice;
            
            this.SpriteSheetSource = new Rectangle(115, 126, 28, 27);
            
            this.IdleAnimation = new Animation(
                fps: 6,
                animatedObject: this,
                frameCount: 4,
                firstFrame: this.SpriteSheetSource,
                tileDirection: TileDirection.Down);

            this.CurrentAnimation = this.IdleAnimation;
        }

        /// <summary>
        /// Upgrades the tower.
        /// </summary>
        public override void Upgrade()
        {
            base.Upgrade();

            // Increment properties
            this.AttackDamage *= GameSettings.AgilityTower_AttackDamage_Upgrade;
            this.ShootRange *= GameSettings.AgilityTower_ShootRange_Upgrade;
            this.ShootCooldown *= GameSettings.AgilityTower_ShootCoolDown_Upgrade;
            this.UpgradePrice *= GameSettings.AgilityTower_UpgradePrice_Upgrade;
        }

        /// <summary>
        /// Fires a projectile at the enemy target.
        /// </summary>
        protected override void ShootTarget()
        {
            // Create a new projectile
            new AgilityProjectile(this.EnemyTarget, this.Position, this.AttackDamage, this);
            
            base.ShootTarget();
        }
    }
}
