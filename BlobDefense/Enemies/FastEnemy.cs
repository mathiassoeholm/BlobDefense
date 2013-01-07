﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlobDefense
{
    using System.Drawing;

    internal class FastEnemy : Enemy
    {
        public FastEnemy() : this(1)
        {
            
        }
        
        public FastEnemy(float difficulityFactor) 
        {
            // Assign properties for this enemy
            this.SpriteSheetSource = new Rectangle(0, 131, 15, 23);
            this.MoveSpeed = 65;
            this.StartHealth = 50 * difficulityFactor;
            this.Bounty = (int)(10 * difficulityFactor);

            this.WalkRightAnimation = new Animation(
                fps: 6,
                animatedObject: this,
                frameCount: 3,
                firstFrame: new Rectangle(45, 131, 23, 23),
                tileDirection: TileDirection.Right);

            this.WalkLeftAnimation = new Animation(
                fps: 6,
                animatedObject: this,
                frameCount: 3,
                firstFrame: new Rectangle(46, 155, 23, 23),
                tileDirection: TileDirection.Right);

            this.WalkUpAnimation = new Animation(
                fps: 6,
                animatedObject: this,
                frameCount: 3,
                firstFrame: new Rectangle(0, 131, 15, 23),
                tileDirection: TileDirection.Right);

            this.WalkDownAnimation = new Animation(
                fps: 6,
                animatedObject: this,
                frameCount: 3,
                firstFrame: new Rectangle(0, 155, 15, 23),
                tileDirection: TileDirection.Right);

            this.AssignCurrentAnimation();
        }
    }
}
