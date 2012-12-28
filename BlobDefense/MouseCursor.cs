using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BlobDefense
{
    using System.Drawing;

    internal class MouseCursor : GameObject
    {
        public MouseCursor()
        {
            this.SpriteSheetSource = new Rectangle(48, 160, 16, 16);
        }

        public void SetPosition(Point position)
        {
            this.Position = position;
        }
    }
}
