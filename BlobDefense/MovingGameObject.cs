using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Extensions;

namespace BlobDefense
{
    using System.Drawing;

    internal class MovingGameObject : GameObject
    {
        private PointF currentTarget;

        protected float moveSpeed { get; set; }

        public void Move()
        {
            // Get the move direction
            var moveDirection = new PointF(this.currentTarget.X - this.Position.X, currentTarget.Y - this.Position.Y);

            // Set length of the move direction based on the move speed
            moveDirection.SetMagnitude(this.moveSpeed * Time.DeltaTime);
            
            // Add the movement to our position
            this.Position.Add(moveDirection);
        }
    }
}
