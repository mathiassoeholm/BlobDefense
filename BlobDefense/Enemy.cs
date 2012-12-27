using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlobDefense
{
    internal abstract class Enemy : MovingGameObject, IUpdateBehaviour
    {
        int targetNode;

        protected Enemy()
        {
            this.targetNode = 1;
            Position = GameDisplay.testPath[0].Position;
            CurrentTarget = GameDisplay.testPath[this.targetNode].Position;
        }

        protected override void OnTargetHit()
        {
            this.targetNode++;
            CurrentTarget = GameDisplay.testPath[this.targetNode].Position;
        }

        // Call move each update
        public void Update()
        {
            this.Move();
        }
    }
}
