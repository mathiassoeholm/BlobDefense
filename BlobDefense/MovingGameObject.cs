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
        /// Gets or sets the current target this game object is moving towards.
        /// </summary>
        protected PointF CurrentTarget { get; set; }

        /// <summary>
        /// Gets or sets the move speed.
        /// </summary>
        protected float MoveSpeed { get; set; }

        /// <summary>
        /// Moves the game object towards the current target.
        /// </summary>
        public void Move()
        {
            // Get the move direction
            var moveDirection = new PointF(this.CurrentTarget.X - this.Position.X, this.CurrentTarget.Y - this.Position.Y);

            // Set length of the move direction based on the move speed
            ExtensionMethods.SetMagnitude(ref moveDirection, this.MoveSpeed * Time.DeltaTime);

            PointF newPosition = this.Position;
            ExtensionMethods.Add(ref newPosition, moveDirection);

            // Check if we pass the target in any axis
            if ((this.Position.X > this.CurrentTarget.X && newPosition.X < this.CurrentTarget.X)
                || (this.Position.X < this.CurrentTarget.X && newPosition.X > this.CurrentTarget.X)
                || (this.Position.Y < this.CurrentTarget.Y && newPosition.Y > this.CurrentTarget.Y)
                || (this.Position.Y > this.CurrentTarget.Y && newPosition.Y < this.CurrentTarget.Y)
                || (moveDirection.X == 0 && moveDirection.Y == 0))
            {
                // Set position to target
                this.Position = this.CurrentTarget;

                // Perform on hit action
                this.OnTargetHit();

                return;
            }

            // Move our positon
            this.Position = newPosition;
        }

        protected abstract void OnTargetHit();
    }
}
