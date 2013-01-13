// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PikachuEnemy.cs" company="Backdoor Fun">
//   © 2013
// </copyright>
// <summary>
//   Defines the pikachu enemy's animations and settings.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace BlobDefense.Enemies
{
    using System;
    using System.Drawing;

    using BlobDefense.WaveSpawner;

    /// <summary>
    /// Defines the pikachu enemy's animations and settings.
    /// </summary>
    internal class PikachuEnemy : Enemy
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PikachuEnemy"/> class.
        /// </summary>
        public PikachuEnemy() : base(healthBarWidth: GameSettings.PikachuEnemy_HealthBarWidth)
        {
            // Assign properties for this enemy
            this.SpriteSheetSource = new Rectangle(70, 103, 24, 23);
            this.MoveSpeed = GameSettings.PikachuEnemy_MoveSpeed;
            this.StartHealth = GameSettings.PikachuEnemy_StartHealth * WaveManager.Instance.EnemyDifficulty;
            this.Bounty = (int)(GameSettings.PikachuEnemy_Bounty * WaveManager.Instance.EnemyDifficulty);

            this.WalkRightAnimation = new Animation(
                fps: 6,
                animatedObject: this,
                frameCount: 3,
                firstFrame: new Rectangle(0, 76, 23, 27),
                tileDirection: TileDirection.Right);

            this.WalkLeftAnimation = new Animation(
                fps: 6,
                animatedObject: this,
                frameCount: 3,
                firstFrame: new Rectangle(0, 103, 23, 27),
                tileDirection: TileDirection.Right);

            this.WalkUpAnimation = new Animation(
                fps: 6,
                animatedObject: this,
                frameCount: 3,
                firstFrame: new Rectangle(70, 103, 24, 23),
                tileDirection: TileDirection.Right);

            this.WalkDownAnimation = new Animation(
                fps: 6,
                animatedObject: this,
                frameCount: 3,
                firstFrame: new Rectangle(71, 75, 19, 25),
                tileDirection: TileDirection.Right);

            this.AssignCurrentAnimation();
        }
    }
}

