using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlobDefense.Towers
{
    using System.Drawing;

    using BlobDefense.Enemies;

    internal class StandardProjectile : Projectile
    {
        public StandardProjectile(Enemy enemy, PointF spawnPosition, float attackDamage, Tower towerSource)
            : base(enemy, attackDamage, towerSource, spawnPosition)
        {
            this.SpriteSheetSource = new Rectangle(100, 179, 14, 7);
            this.MoveSpeed = 200;
        }
    }
}
