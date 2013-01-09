// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StandardEnemy.cs" company="Backdoor Fun">
//   © 2013
// </copyright>
// <summary>
//   Defines the standard enemies animations and settings.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace BlobDefense.Enemies
{
    using System.Drawing;

    using BlobDefense.WaveSpawner;

    /// <summary>
    /// Defines the standard enemy's animations and settings.
    /// </summary>
    internal class StandardEnemy : Enemy
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StandardEnemy"/> class.
        /// </summary>
        public StandardEnemy()
        {
            // Assign properties for this enemy
            this.SpriteSheetSource = new Rectangle(0, 54, 16, 22);
            this.MoveSpeed = GameSettings.StandardEnemy_MoveSpeed;
            this.StartHealth = GameSettings.StandardEnemy_StartHealth * WaveManager.Instance.EnemyDifficulity;
            this.Bounty = (int)(GameSettings.StandardEnemy_Bounty * WaveManager.Instance.EnemyDifficulity);

            this.WalkRightAnimation = new Animation(
                fps: 6,
                animatedObject: this,
                frameCount: 3,
                firstFrame: new Rectangle(64, 32, 17, 22),
                tileDirection: TileDirection.Right);

            this.WalkLeftAnimation = new Animation(
                fps: 6,
                animatedObject: this,
                frameCount: 3,
                firstFrame: new Rectangle(64, 54, 17, 22),
                tileDirection: TileDirection.Right);

            this.WalkUpAnimation = new Animation(
                fps: 6,
                animatedObject: this,
                frameCount: 4,
                firstFrame: new Rectangle(0, 54, 16, 22),
                tileDirection: TileDirection.Right);

            this.WalkDownAnimation = new Animation(
                fps: 6,
                animatedObject: this,
                frameCount: 4,
                firstFrame: new Rectangle(0, 32, 16, 22),
                tileDirection: TileDirection.Right);

            this.AssignCurrentAnimation();
        }
    }
}
