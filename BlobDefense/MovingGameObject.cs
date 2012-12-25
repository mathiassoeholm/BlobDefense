using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Extensions;

namespace BlobDefense
{
    using System.Drawing;

    internal abstract class MovingGameObject : GameObject
    {
        /// <summary>
        /// The current target this game object is moving towards.
        /// </summary>
        private PointF currentTarget;

        /// <summary>
        /// Gets or sets the move speed.
        /// </summary>
        protected float MoveSpeed { get; set; }

        /// Moves the game object towards the current target.
        /// </summary>
        public void Move()
        {
            // Get the move direction
            var moveDirection = new PointF(this.currentTarget.X - this.Position.X, this.currentTarget.Y - this.Position.Y);

            // Set length of the move direction based on the move speed
            moveDirection.SetMagnitude(this.MoveSpeed * Time.DeltaTime);
            
            // Check if we pass the target in any axis
            if ((this.Position.X > this.currentTarget.X && this.Position.X + moveDirection.X < this.currentTarget.X)
                || (this.Position.X < this.currentTarget.X && this.Position.X + moveDirection.X > this.currentTarget.X)
                || (this.Position.Y < this.currentTarget.Y && this.Position.Y + moveDirection.Y > this.currentTarget.Y)
                || (this.Position.Y > this.currentTarget.Y && this.Position.Y + moveDirection.Y > this.currentTarget.Y))
            {
                
                // Set position to target
                this.Position = this.currentTarget;

                // Perform on hit action
                this.OnTargetHit();

                return;
            }

            // Add the movement to our position
            this.Position.Add(moveDirection);
        }

        protected abstract void OnTargetHit();
    }
}
