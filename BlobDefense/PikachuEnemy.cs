using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlobDefense
{
    using System.Drawing;

    class PikachuEnemy : Enemy
    {
        public PikachuEnemy()
        {
            // Assign properties for this enemy
            this.SpriteSheetSource = new RectangleF(96, 128, 32, 32);
            this.MoveSpeed = 25f;

            // TODO: Remove hard coded values, consider just writing in indexes and not sizes
            this.WalkRightAnimation = new Animation(6, this, new Rectangle(0, 96, 32, 32), new Rectangle(32, 96, 32, 32), new Rectangle(64, 96, 32, 32));

            this.WalkLeftAnimation = new Animation(6, this, new Rectangle(0, 128, 32, 32), new Rectangle(32, 128, 32, 32), new Rectangle(64, 128, 32, 32));

            this.WalkUpAnimation = new Animation(6, this, new Rectangle(96, 128, 32, 32), new Rectangle(128, 128, 32, 32), new Rectangle(160, 128, 32, 32));

            this.WalkDownAnimation = new Animation(6, this, new Rectangle(96, 96, 32, 32), new Rectangle(128, 96, 32, 32), new Rectangle(160, 96, 32, 32));

            this.AssignCurrentAnimation();
        }
    }
}
