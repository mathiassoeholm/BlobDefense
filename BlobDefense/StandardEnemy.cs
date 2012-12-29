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
            this.SpriteSheetSource = new Rectangle(0, 64, 16, 32);
            this.MoveSpeed = 25f;

            // TODO: Remove hard coded values, consider just writing in indexes and not sizes
            this.WalkRightAnimation = new Animation(
                fps: 6,
                animatedObject: this,
                frameCount: 3,
                firstFrame: new Rectangle(64, 32, 32, 32),
                tileDirection: TileDirection.Right);

            this.WalkLeftAnimation = new Animation(
                fps: 6,
                animatedObject: this,
                frameCount: 3,
                firstFrame: new Rectangle(64, 64, 32, 32),
                tileDirection: TileDirection.Right);

            this.WalkUpAnimation = new Animation(
                fps: 6,
                animatedObject: this,
                frameCount: 4,
                firstFrame: new Rectangle(0, 64, 16, 32),
                tileDirection: TileDirection.Right);

            this.WalkDownAnimation = new Animation(
                fps: 6,
                animatedObject: this,
                frameCount: 4,
                firstFrame: new Rectangle(0, 32, 16, 32),
                tileDirection: TileDirection.Right);

            this.AssignCurrentAnimation();
        }
    }
}
