// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FastEnemy.cs" company="Backdoor Fun">
//   © 2013
// </copyright>
// <summary>
//   Defines the fast enemy's animations and settings.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace BlobDefense.Enemies
{
    using System;
    using System.Drawing;

    using BlobDefense.WaveSpawner;

    /// <summary>
    /// Defines the fast enemy's animations and settings.
    /// </summary>
    internal class FastEnemy : Enemy
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FastEnemy"/> class.
        /// </summary>
        public FastEnemy() 
        {
            // Assign properties for this enemy
            this.SpriteSheetSource = new Rectangle(0, 131, 15, 23);
            this.MoveSpeed = GameSettings.FastEnemy_MoveSpeed;
            this.StartHealth = GameSettings.FastEnemy_StartHealth * WaveManager.Instance.EnemyDifficulity;
            this.Bounty = (int)(GameSettings.FastEnemy_Bounty * WaveManager.Instance.EnemyDifficulity);

            this.WalkRightAnimation = new Animation(
                fps: 6,
                animatedObject: this,
                frameCount: 3,
                firstFrame: new Rectangle(45, 131, 23, 23),
                tileDirection: TileDirection.Right);

            this.WalkLeftAnimation = new Animation(
                fps: 6,
                animatedObject: this,
                frameCount: 3,
                firstFrame: new Rectangle(46, 155, 23, 23),
                tileDirection: TileDirection.Right);

            this.WalkUpAnimation = new Animation(
                fps: 6,
                animatedObject: this,
                frameCount: 3,
                firstFrame: new Rectangle(0, 131, 15, 23),
                tileDirection: TileDirection.Right);

            this.WalkDownAnimation = new Animation(
                fps: 6,
                animatedObject: this,
                frameCount: 3,
                firstFrame: new Rectangle(0, 155, 15, 23),
                tileDirection: TileDirection.Right);

            this.AssignCurrentAnimation();
        }
    }
}
