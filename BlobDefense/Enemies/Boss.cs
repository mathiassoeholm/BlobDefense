using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlobDefense.Enemies
{
    using System.Drawing;

    internal class Boss : Enemy
    {
        public Boss()
            : this(1)
        {
            
        }

        public Boss(float difficulityFactor = 1) : base(healthBarWidth: 50)
        {
            // Assign properties for this enemy
            this.SpriteSheetSource = new Rectangle(199, 0, 67, 71);
            this.MoveSpeed = 10;
            this.StartHealth = 10000 * difficulityFactor;
            this.Bounty = (int)(100 * difficulityFactor);
            this.DepthLevel = 4;

            this.WalkRightAnimation = new Animation(
                fps: 6,
                animatedObject: this,
                frameCount: 3,
                firstFrame: new Rectangle(199, 0, 67, 71),
                tileDirection: TileDirection.Down);

            this.WalkLeftAnimation = this.WalkRightAnimation;

            this.WalkUpAnimation = this.WalkRightAnimation;

            this.WalkDownAnimation = this.WalkRightAnimation;

            this.AssignCurrentAnimation();
        }
    }
}
