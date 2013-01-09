// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Enemy.cs" company="Backdoor Fun">
//   © 2013
// </copyright>
// <summary>
//   Defines the base for all enemies in the game.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace BlobDefense.Enemies
{
    using System;
    using System.Drawing;

    using Extensions;

    /// <summary>
    /// Defines the base for all enemies in the game.
    /// </summary>
    internal abstract class Enemy : MovingGameObject, IUpdateBehaviour, IAnimated
    {
        /// <summary>
        /// The brush used to draw the red part of the health bars.
        /// </summary>
        private static Brush healthBarRedPen;

        /// <summary>
        /// The brush used to draw the green part of the health bars.
        /// </summary>
        private static Brush healthBarGreenPen;

        /// <summary>
        /// The width of the enemies health bar, in pixels.
        /// </summary>
        private readonly int healthBarWidth;

        /// <summary>
        /// The node index that this enemy will go to.
        /// </summary>
        private int targetNode;

        /// <summary>
        /// The current health.
        /// </summary>
        private float currentHealth;

        /// <summary>
        /// The initial health.
        /// </summary>
        private float startHealth;

        /// <summary>
        /// A value indicating whether the enemy is dead or not.
        /// </summary>
        private bool isDead;

        /// <summary>
        /// The default move speed, used to go back to normal after getting slowed.
        /// </summary>
        private float defaultMoveSpeed;

        /// <summary>
        /// The point in time, where the slow effect from a frost tower will stop.
        /// </summary>
        private DateTime endOfSlow;

        /// <summary>
        /// Initializes a new instance of the <see cref="Enemy"/> class.
        /// </summary>
        /// <param name="healthBarWidth">
        /// An optional new health bar width for this enemy.
        /// </param>
        protected Enemy(int healthBarWidth = GameSettings.DefaultEnemyHealthBarWidth)
        {
            // Initialize the static brushes, if they aren't already
            healthBarRedPen = healthBarRedPen ?? new SolidBrush(Color.Red);
            healthBarGreenPen = healthBarGreenPen ?? new SolidBrush(Color.Green);

            // Assign initial values
            this.healthBarWidth = healthBarWidth;
            this.targetNode = 1;
            this.Position = GameLogic.EnemyPath[0].Position;
            this.CurrentTarget = GameLogic.EnemyPath[this.targetNode].Position;
        }

        /// <summary>
        /// Gets or sets the current animation.
        /// </summary>
        public Animation CurrentAnimation { get; protected set; }

        /// <summary>
        /// Gets or sets the initial health for this enemy.
        /// </summary>
        protected float StartHealth
        {
            get
            {
                return this.startHealth;
            }

            set
            {
                // Assigns the start health field, but also the current health
                this.startHealth = value;
                this.currentHealth = value;
            }
        }

        /// <summary>
        /// Gets or sets the walk right animation.
        /// </summary>
        protected Animation WalkRightAnimation { get; set; }

        /// <summary>
        /// Gets or sets the walk left animation.
        /// </summary>
        protected Animation WalkLeftAnimation { get; set; }

        /// <summary>
        /// Gets or sets the walk up animation.
        /// </summary>
        protected Animation WalkUpAnimation { get; set; }

        /// <summary>
        /// Gets or sets the walk down animation.
        /// </summary>
        protected Animation WalkDownAnimation { get; set; }

        /// <summary>
        /// Gets or sets the amount of money given to the player on death.
        /// </summary>
        protected int Bounty { get; set; }

        /// <summary>
        /// Makes the enemy lose damage, and kills it if it reaches zero.
        /// </summary>
        /// <param name="amount">
        /// The amount of damage dealt to the enemy.
        /// </param>
        /// <returns>
        /// A value indicating whether the enemy was killed by the attack.
        /// </returns>
        public bool TakeDamage(float amount)
        {
            if (this.isDead)
            {
                return false;
            }
            
            // Check if we are dead
            if (this.currentHealth - amount <= 0)
            {
                this.currentHealth = 0;

                this.Die();

                // Return true if the enemy died
                return true;
            }

            // Subtract from health
            this.currentHealth -= amount;

            // Return false if the enemy did not die
            return false;
        }

        /// <summary>
        /// Plays the current animation.
        /// </summary>
        public void RunAnimation()
        {
            this.CurrentAnimation.RunAnimation();
        }

        /// <summary>
        /// Slows the enemies move speed, by a specified amount, for a given duration.
        /// </summary>
        /// <param name="amount">
        /// The amount to slow with.
        /// </param>
        /// <param name="duration">
        /// The duration of the slow effect.
        /// </param>
        public void Slow(float amount, float duration)
        {
            this.MoveSpeed = this.defaultMoveSpeed * amount;
            this.endOfSlow = DateTime.Now.Add(TimeSpan.FromSeconds(duration));
        }

        /// <summary>
        /// Draws this enemy's health bar.
        /// </summary>
        /// <param name="graphics">
        /// The graphics context, used to draw the health bar.
        /// </param>
        public void DrawHealthBar(Graphics graphics)
        {
            // Calculate width for green part of healthbar
            int greenWidth = (int)((this.currentHealth / this.startHealth) * this.healthBarWidth);

            // Calculate the position for the health bar
            Point healthBarPos = new Point(
                x: (int)(this.Position.X - (this.healthBarWidth * 0.5f)),
                y: (int)(this.Position.Y - GameSettings.EnemyHealthBarHeight - (this.SpriteSheetSource.Height * 0.5f)));

            // Draw red part of health bar
            graphics.FillRectangle(healthBarRedPen, healthBarPos.X, healthBarPos.Y, this.healthBarWidth, GameSettings.EnemyHealthBarHeight);

            // Draw green part of health bar
            graphics.FillRectangle(healthBarGreenPen, healthBarPos.X, healthBarPos.Y, greenWidth, GameSettings.EnemyHealthBarHeight);
        }

        /// <summary>
        /// The update method, called once per frame.
        /// </summary>
        public void Update()
        {
            // Set the default move speed, if not set yet.
            if (this.defaultMoveSpeed <= 0)
            {
                this.defaultMoveSpeed = this.MoveSpeed;
            }
            
            // Check if slow effect has passed
            if (DateTime.Now.CompareTo(this.endOfSlow) == 1)
            {
                this.MoveSpeed = this.defaultMoveSpeed;
            }
            
            // Move the enemy
            this.Move();
        }

        /// <summary>
        /// Selects the next node, or calls the reach goal method.
        /// </summary>
        protected override void OnTargetHit()
        {
            this.targetNode++;

            // Check if we reached the last node
            if (this.targetNode >= GameLogic.EnemyPath.Count)
            {
                this.ReachGoal();
                return;
            }

            // Give the enemy a new target
            this.CurrentTarget = GameLogic.EnemyPath[this.targetNode].Position;

            this.AssignCurrentAnimation();
        }

        /// <summary>
        /// Assigns the current animation based on relative position to the EnemyTarget.
        /// </summary>
        protected void AssignCurrentAnimation()
        {
            if (this.CurrentTarget.X > this.Position.X)
            {
                // The enemy is walking right
                this.CurrentAnimation = this.WalkRightAnimation;
            }
            else if (this.CurrentTarget.X < this.Position.X)
            {
                // The enemy is walking left
                this.CurrentAnimation = this.WalkLeftAnimation;
            }
            else if (this.CurrentTarget.Y > this.Position.Y)
            {
                // The enemy is walking down
                this.CurrentAnimation = this.WalkDownAnimation;
            }
            else
            {
                // The enemy is walking up
                this.CurrentAnimation = this.WalkUpAnimation;
            }
        }

        /// <summary>
        /// The die method, called once when killed.
        /// </summary>
        private void Die()
        {
            // Set the is dead bool flag
            this.isDead = true;
            
            // Invoke death event
            EventManager.Instance.EnemyDied.SafeInvoke(this);

            // Give the player bounty
            GameManager.Instance.GiveCurrency(this.Bounty);
            
            // Destroy this enemy
            this.Destroy();
        }

        /// <summary>
        /// Called when the enemy reaches the last node of the path.
        /// </summary>
        private void ReachGoal()
        {
            // Incoke reached goal event
            EventManager.Instance.EnemyReachedGoal.SafeInvoke();
            
            // Go back to start
            this.Position = GameLogic.Instance.StartNode.Position;
            this.targetNode = 1;
            this.CurrentTarget = GameLogic.EnemyPath[this.targetNode].Position;
        }
    }
}
