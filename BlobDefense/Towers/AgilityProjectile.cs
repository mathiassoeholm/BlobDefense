using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlobDefense.Towers
{
    using System.Drawing;

    internal class AgilityProjectile : Projectile
    {
        public AgilityProjectile(Enemy enemy, PointF spawnPosition, float attackDamage, Tower towerSource)
            : base(enemy, attackDamage, towerSource)
        {
            this.SpriteSheetSource = new Rectangle(100, 194, 14, 5);
            this.Position = spawnPosition;
            this.MoveSpeed = 500;
        }
    }
}
