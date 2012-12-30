using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlobDefense
{
    using System.Drawing;

    internal class EventManager : Singleton<EventManager>
    {
        /// <summary>
        /// Prevents a default instance of the <see cref="EventManager"/> class from being created.
        /// </summary>
        private EventManager()
        { 
        }
        
        /// <summary>
        /// Gets or sets the on mouse click delegate, with the mouse position.
        /// </summary>
        public Action<Point> OnMouseClick { get; set; }
    }
}
