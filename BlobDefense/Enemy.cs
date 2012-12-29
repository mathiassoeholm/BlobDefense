using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlobDefense
{
    internal abstract class Enemy : MovingGameObject, IUpdateBehaviour, IAnimated
    {
        private int targetNode;

        protected Enemy()
        {
            this.targetNode = 1;
            this.Position = GameDisplay.TestPath[0].Position;
            this.CurrentTarget = GameDisplay.TestPath[this.targetNode].Position;
        }

        public Animation CurrentAnimation { get; protected set; }

        protected Animation WalkRightAnimation { get; set; }

        protected Animation WalkLeftAnimation { get; set; }

        protected Animation WalkUpAnimation { get; set; }

        protected Animation WalkDownAnimation { get; set; }

        public void RunAnimation()
        {
            this.CurrentAnimation.RunAnimation();
        }

        // Call move each update
        protected override void OnTargetHit()
        {
            this.targetNode++;

            if (this.targetNode >= GameDisplay.TestPath.Count)
            {
                this.ReachGoal();
                return;
            }

            this.CurrentTarget = GameDisplay.TestPath[this.targetNode].Position;

            this.AssignCurrentAnimation();
        }

        public void Update()
        {
            this.Move();
        }

        private void ReachGoal()
        {
            // Destroy the enemy
            this.Destroy();
        }

        /// <summary>
        /// Assigns the current animation based on relative position to the target.
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
