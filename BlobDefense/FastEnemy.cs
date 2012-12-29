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

            this.WalkRightAnimation = new Animation(
                fps: 6,
                animatedObject: this,
                frameCount: 3,
                firstFrame: new Rectangle(64, 160, 32, 32),
                tileDirection: TileDirection.Right);

            this.WalkLeftAnimation = new Animation(
                fps: 6,
                animatedObject: this,
                frameCount: 3,
                firstFrame: new Rectangle(64, 192, 32, 32),
                tileDirection: TileDirection.Right);

            this.WalkUpAnimation = new Animation(
                fps: 6,
                animatedObject: this,
                frameCount: 3,
                firstFrame: new Rectangle(0, 160, 16, 32),
                tileDirection: TileDirection.Right);

            this.WalkDownAnimation = new Animation(
                fps: 6,
                animatedObject: this,
                frameCount: 3,
                firstFrame: new Rectangle(0, 192, 16, 32),
                tileDirection: TileDirection.Right);

            this.AssignCurrentAnimation();
        }
    }
}
