using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlobDefense
{
    using System.Drawing;

    class FastEnemy : Enemy
    {
        public FastEnemy()
        {
            // Assign properties for this enemy
            this.SpriteSheetSource = new RectangleF(0, 160, 16, 32);
            this.MoveSpeed = 40;

            // TODO: Remove hard coded values, consider just writing in indexes and not sizes
            this.WalkRightAnimation = new Animation(6, this, new Rectangle(64, 160, 32, 32), new Rectangle(96, 160, 32, 32), new Rectangle(128, 160, 32, 32));

            this.WalkLeftAnimation = new Animation(6, this, new Rectangle(64, 196, 32, 32), new Rectangle(96, 196, 32, 32), new Rectangle(128, 196, 32, 32));

            this.WalkUpAnimation = new Animation(6, this, new Rectangle(0, 160, 16, 32), new Rectangle(16, 160, 16, 32), new Rectangle(32, 160, 16, 32));

            this.WalkDownAnimation = new Animation(6, this, new Rectangle(0, 196, 16, 32), new Rectangle(16, 196, 16, 32), new Rectangle(32, 196, 16, 32));

            this.AssignCurrentAnimation();
        }
    }
}
