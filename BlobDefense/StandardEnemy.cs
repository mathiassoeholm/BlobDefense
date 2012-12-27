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
            SpriteSheetSource = new RectangleF(32, 0, 32, 32);
            MoveSpeed = 0.25f;
        }
    }
}
