// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StandardTower.cs" company="Backdoor Fun">
//   © 2013
// </copyright>
// <summary>
//   Defines a tower with normal attack damage and cool down.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace BlobDefense.Towers
{
    using System;
    using System.Drawing;

    /// <summary>
    /// Defines a tower with normal attack damage and cool down.
    /// </summary>
    [Serializable]
    internal class StandardTower : Tower
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StandardTower"/> class.
        /// </summary>
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

        /// <summary>
        /// Upgrades the tower.
        /// </summary>
        public override void Upgrade()
        {
            base.Upgrade();

            // Increment properties
            this.AttackDamage *= GameSettings.StandardTower_AttackDamage_Upgrade;
            this.ShootRange *= GameSettings.StandardTower_ShootRange_Upgrade;
            this.ShootCooldown *= GameSettings.StandardTower_ShootCoolDown_Upgrade;
            this.UpgradePrice *= GameSettings.StandardTower_UpgradePrice_Upgrade;
        }

        /// <summary>
        /// Fires a projectile at the enemy target.
        /// </summary>
        protected override void ShootTarget()
        {
            // Create a new projectile
            new StandardProjectile(this.EnemyTarget, this.Position, this.AttackDamage, this);
            
            base.ShootTarget();
        }
    }
}
