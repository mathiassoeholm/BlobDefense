using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BlobDefense
{
    using System.Drawing;

    internal class MouseCursor : GameObject, IUpdateBehaviour
    {
        public MouseCursor()
        {
            this.SpriteSheetSource = new Rectangle(128, 84, 16, 16);
        }

        public void Update()
        {
            this.Position = GameDisplay.MousePosition;
        }
    }
}
