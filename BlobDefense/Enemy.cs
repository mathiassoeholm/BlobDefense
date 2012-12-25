using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlobDefense
{
    internal class Enemy : MovingGameObject
    {
        private int targetNode;
        
        protected override void OnTargetHit()
        {
            // TODO: Select new target in the path list, and set targetNode
            throw new NotImplementedException();
        }
    }
}
