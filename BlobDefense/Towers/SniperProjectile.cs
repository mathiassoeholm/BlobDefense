using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlobDefense.Towers
{
    using System.Drawing;

    using BlobDefense.Enemies;

    class SniperProjectile : Projectile
    {
        public SniperProjectile(Enemy enemy, PointF spawnPosition, float attackDamage, Tower towerSource)
            : base(enemy, attackDamage, towerSource)
        {
            this.SpriteSheetSource = new Rectangle(102, 200, 9, 5);
            this.Position = spawnPosition;
            this.MoveSpeed = 100;
        }
    }
}
