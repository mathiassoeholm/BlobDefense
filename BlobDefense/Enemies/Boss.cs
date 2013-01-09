// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Boss.cs" company="Backdoor Fun">
//   © 2013
// </copyright>
// <summary>
//   Defines the boss's animations and settings.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace BlobDefense.Enemies
{
    using System.Drawing;

    using BlobDefense.WaveSpawner;

    /// <summary>
    /// Defines the boss's animations and settings.
    /// </summary>
    internal class Boss : Enemy
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Boss"/> class.
        /// </summary>
        public Boss() : base(healthBarWidth: GameSettings.Boss_HealthBarWidth)
        {
            // Assign properties for this enemy
            this.SpriteSheetSource = new Rectangle(199, 0, 67, 71);
            this.MoveSpeed = GameSettings.Boss_MoveSpeed;
            this.StartHealth = GameSettings.Boss_StartHealth * WaveManager.Instance.EnemyDifficulity;
            this.Bounty = (int)(GameSettings.Boss_Bounty * WaveManager.Instance.EnemyDifficulity);
            this.DepthLevel = 4;

            this.WalkRightAnimation = new Animation(
                fps: 6,
                animatedObject: this,
                frameCount: 3,
                firstFrame: this.SpriteSheetSource,
                tileDirection: TileDirection.Down);

            // All walk directions use the same animation
            this.WalkLeftAnimation = this.WalkRightAnimation;
            this.WalkUpAnimation = this.WalkRightAnimation;
            this.WalkDownAnimation = this.WalkRightAnimation;

            this.AssignCurrentAnimation();
        }
    }
}
