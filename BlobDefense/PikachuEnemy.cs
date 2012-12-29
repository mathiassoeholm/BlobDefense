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
            this.SpriteSheetSource = new Rectangle(70, 103, 24, 23);
            this.MoveSpeed = 25f;

            this.WalkRightAnimation = new Animation(
                fps: 6,
                animatedObject: this,
                frameCount: 3,
                firstFrame: new Rectangle(0, 76, 23, 27),
                tileDirection: TileDirection.Right);

            this.WalkLeftAnimation = new Animation(
                fps: 6,
                animatedObject: this,
                frameCount: 3,
                firstFrame: new Rectangle(0, 103, 23, 27),
                tileDirection: TileDirection.Right);

            this.WalkUpAnimation = new Animation(
                fps: 6,
                animatedObject: this,
                frameCount: 3,
                firstFrame: new Rectangle(70, 103, 24, 23),
                tileDirection: TileDirection.Right);

            this.WalkDownAnimation = new Animation(
                fps: 6,
                animatedObject: this,
                frameCount: 3,
                firstFrame: new Rectangle(71, 75, 19, 25),
                tileDirection: TileDirection.Right);

            this.AssignCurrentAnimation();
        }
    }
}
