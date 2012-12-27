using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlobDefense
{
    using System.Drawing;

    class StandardEnemy : Enemy
    {
        public StandardEnemy()
        {
            // Assign properties for this enemy
            this.SpriteSheetSource = new RectangleF(32, 32, 32, 32);
            this.MoveSpeed = 0.25f;

            // TODO: Remove hard coded values, consider just writing in indexes and not sizes
            this.WalkRightAnimation = new Animation(6, this, new Rectangle(128, 32, 32, 32), new Rectangle(160, 32, 32, 32), new Rectangle(192, 32, 32, 32));

            this.WalkLeftAnimation = new Animation(6, this, new Rectangle(128, 64, 32, 32), new Rectangle(160, 64, 32, 32), new Rectangle(192, 64, 32, 32));

            this.WalkUpAnimation = new Animation(6, this, new Rectangle(0, 64, 32, 32), new Rectangle(32, 64, 32, 32), new Rectangle(64, 64, 32, 32), new Rectangle(96, 64, 32, 32));

            this.WalkDownAnimation = new Animation(6, this, new Rectangle(0, 32, 32, 32), new Rectangle(32, 32, 32, 32), new Rectangle(64, 32, 32, 32), new Rectangle(96, 32, 32, 32));

            this.AssignCurrentAnimation();
        }
    }
}
