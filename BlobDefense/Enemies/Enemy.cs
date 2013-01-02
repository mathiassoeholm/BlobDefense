using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlobDefense
{
    using System.Drawing;

    internal abstract class Enemy : MovingGameObject, IUpdateBehaviour, IAnimated
    {
        private const int HealthBarWidth = 25;
        private const int HealthBarHeight = 3;

        private static Brush healthBarRedPen;

        private static Brush healthBarGreenPen;

        private int targetNode;

        private float currentHealth;

        private float startHealth;

        protected Enemy()
        {
            healthBarRedPen = healthBarRedPen ?? new SolidBrush(Color.Red);
            healthBarGreenPen = healthBarGreenPen ?? new SolidBrush(Color.Green);
            
            this.targetNode = 1;
            this.Position = GameLogic.EnemyPath[0].Position;
            this.CurrentTarget = GameLogic.EnemyPath[this.targetNode].Position;

            this.StartHealth = 100;
            this.currentHealth = 25;
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

        public void TakeDamage(float amount)
        {
            // Check if we are dead
            if (this.currentHealth - amount <= 0)
            {
                this.currentHealth = 0;

                this.Die();
            }

            // Subtract from health
            this.currentHealth -= amount;
        }

        public void RunAnimation()
        {
            this.CurrentAnimation.RunAnimation();
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

        public void DrawHealthBar(Graphics graphics)
        {
            // Calculate width for green part of healthbar
            int greenWidth = (int)((this.currentHealth / this.startHealth) * HealthBarWidth);
            
            Point healthBarPos = new Point((int)(this.Position.X - (HealthBarWidth * 0.5f)), (int)(this.Position.Y - HealthBarHeight - (this.SpriteSheetSource.Height * 0.5f)));

            // Draw red part of health bar
            graphics.FillRectangle(healthBarRedPen, healthBarPos.X, healthBarPos.Y, HealthBarWidth, HealthBarHeight);

            // Draw green part of health bar
            graphics.FillRectangle(healthBarGreenPen, healthBarPos.X, healthBarPos.Y, greenWidth, HealthBarHeight);
        }

        public void Update()
        {
            this.Move();
        }

        private void Die()
        {
            // Destroy the enemy
            this.Destroy();
        }

        private void ReachGoal()
        {
            // Destroy the enemy
            this.Destroy();
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
