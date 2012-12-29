using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlobDefense.Towers
{
    using System.Drawing;

    internal class StandardProjectile : Projectile
    {
        public StandardProjectile(Enemy enemy, PointF spawnPosition)
            : base(enemy)
        {
            this.SpriteSheetSource = new RectangleF(100, 179, 14, 7);
            this.Position = spawnPosition;
            this.MoveSpeed = 100;
        }
    }
}
