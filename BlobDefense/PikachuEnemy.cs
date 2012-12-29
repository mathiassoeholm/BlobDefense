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

            this.WalkRightAnimation = new Animation(
                fps: 6,
                animatedObject: this,
                frameCount: 3,
                firstFrame: new Rectangle(0, 96, 32, 32),
                tileDirection: TileDirection.Right);

            this.WalkLeftAnimation = new Animation(
                fps: 6,
                animatedObject: this,
                frameCount: 3,
                firstFrame: new Rectangle(0, 128, 32, 32),
                tileDirection: TileDirection.Right);

            this.WalkUpAnimation = new Animation(
                fps: 6,
                animatedObject: this,
                frameCount: 3,
                firstFrame: new Rectangle(96, 128, 32, 32),
                tileDirection: TileDirection.Right);

            this.WalkDownAnimation = new Animation(
                fps: 6,
                animatedObject: this,
                frameCount: 3,
                firstFrame: new Rectangle(96, 96, 32, 32),
                tileDirection: TileDirection.Right);

            this.AssignCurrentAnimation();
        }
    }
}
