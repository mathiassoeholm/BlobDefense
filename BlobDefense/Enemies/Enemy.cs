using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlobDefense
{
    using System.Drawing;

    using Extensions;

    internal abstract class Enemy : MovingGameObject, IUpdateBehaviour, IAnimated
    {
        private const int HealthBarWidth = 25;
        private const int HealthBarHeight = 3;

        private static Brush healthBarRedPen;

        private static Brush healthBarGreenPen;

        private int targetNode;

        private int healthBarWidth;

        private float currentHealth;

        private float startHealth;

        private bool isDead;

        private float defaultMoveSpeed;

        private DateTime endOfSlow;

        protected Enemy(int healthBarWidth = HealthBarWidth)
        {
            healthBarRedPen = healthBarRedPen ?? new SolidBrush(Color.Red);
            healthBarGreenPen = healthBarGreenPen ?? new SolidBrush(Color.Green);

            this.healthBarWidth = healthBarWidth;
            this.targetNode = 1;
            this.Position = GameLogic.EnemyPath[0].Position;
            this.CurrentTarget = GameLogic.EnemyPath[this.targetNode].Position;
        }

        protected float StartHealth
        {
            get
            {
                return this.startHealth;
            }
            set
            {
                this.startHealth = value;
                this.currentHealth = value;
            }
        }

        public Animation CurrentAnimation { get; protected set; }

        protected Animation WalkRightAnimation { get; set; }

        protected Animation WalkLeftAnimation { get; set; }

        protected Animation WalkUpAnimation { get; set; }

        protected Animation WalkDownAnimation { get; set; }

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

                this.isDead = true;

                return true;
            }

            // Subtract from health
            this.currentHealth -= amount;

            return false;
        }

        public void RunAnimation()
        {
            this.CurrentAnimation.RunAnimation();
        }

        public void SetDifficulity(float amount)
        {
            this.currentHealth *= amount;
        }

        // Call move each update
        protected override void OnTargetHit()
        {
            this.targetNode++;

            if (this.targetNode >= GameLogic.EnemyPath.Count)
            {
                this.ReachGoal();
                return;
            }

            this.CurrentTarget = GameLogic.EnemyPath[this.targetNode].Position;

            this.AssignCurrentAnimation();
        }

        public void Slow(float amount, float duration)
        {
            this.MoveSpeed = this.defaultMoveSpeed * amount;
            this.endOfSlow = DateTime.Now.Add(TimeSpan.FromSeconds(duration));
        }

        public void DrawHealthBar(Graphics graphics)
        {
            // Calculate width for green part of healthbar
            int greenWidth = (int)((this.currentHealth / this.startHealth) * this.healthBarWidth);

            Point healthBarPos = new Point((int)(this.Position.X - (this.healthBarWidth * 0.5f)), (int)(this.Position.Y - HealthBarHeight - (this.SpriteSheetSource.Height * 0.5f)));

            // Draw red part of health bar
            graphics.FillRectangle(healthBarRedPen, healthBarPos.X, healthBarPos.Y, this.healthBarWidth, HealthBarHeight);

            // Draw green part of health bar
            graphics.FillRectangle(healthBarGreenPen, healthBarPos.X, healthBarPos.Y, greenWidth, HealthBarHeight);
        }

        public void Update()
        {
            // Set the default move speed, if not set yet.
            if (this.defaultMoveSpeed == 0)
            {
                this.defaultMoveSpeed = this.MoveSpeed;
            }
            
            if (DateTime.Now.CompareTo(this.endOfSlow) == 1)
            {
                this.MoveSpeed = this.defaultMoveSpeed;
            }
            
            this.Move();
        }

        private void Die()
        {
            // Give player the bounty
            GameManager.Instance.GiveCurrency(this.Bounty);
            
            // Destroy the enemy
            this.Destroy();
        }

        private void ReachGoal()
        {
            // Incoke reached goal event
            EventManager.Instance.EnemyReachedGoal.SafeInvoke();
            
            // Go back to start
            this.Position = GameLogic.Instance.StartNode.Position;
            this.targetNode = 1;
            this.CurrentTarget = GameLogic.EnemyPath[this.targetNode].Position;
        }

        /// <summary>
        /// Assigns the current animation based on relative position to the EnemyTarget.
        /// </summary>
        protected void AssignCurrentAnimation()
        {
            if (this.CurrentTarget.X > this.Position.X)
            {
                this.CurrentAnimation = this.WalkRightAnimation;
            }
            else if (this.CurrentTarget.X < this.Position.X)
            {
                this.CurrentAnimation = this.WalkLeftAnimation;
            }
            else if (this.CurrentTarget.Y > this.Position.Y)
            {
                this.CurrentAnimation = this.WalkDownAnimation;
            }
            else
            {
                this.CurrentAnimation = this.WalkUpAnimation;
            }
        }
    }
}
