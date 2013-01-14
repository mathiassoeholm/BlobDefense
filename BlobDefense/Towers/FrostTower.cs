// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FrostTower.cs" company="Backdoor Fun">
//   © 2013
// </copyright>
// <summary>
//   Defines a tower that slows enemies on hit.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace BlobDefense.Towers
{
    using System;
    using System.Drawing;

    /// <summary>
    /// Defines a tower that slows enemies on hit.
    /// </summary>
    [Serializable]
    internal class FrostTower : Tower
    {
        /// <summary>
        /// This value is multiplied with the enemies move speed.
        /// </summary>
        private readonly float slowAmount;

        /// <summary>
        /// How long the enemy is slowed in seconds.
        /// </summary>
        private float slowDuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="FrostTower"/> class.
        /// </summary>
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

        /// <summary>
        /// Upgrades the tower.
        /// </summary>
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

        /// <summary>
        /// Fires a projectile at the enemy target.
        /// </summary>
        protected override void ShootTarget()
        {
            // Create a new projectile
            new FrostProjectile(this.EnemyTarget, this.Position, this.AttackDamage, this.slowDuration, this.slowAmount, this);
            
            base.ShootTarget();
        }
    }
}
