using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlobDefense
{
    using System.Drawing;

    using BlobDefense.Towers;

    internal class EventManager : Singleton<EventManager>
    {
        /// <summary>
        /// Prevents a default instance of the <see cref="EventManager"/> class from being created.
        /// </summary>
        private EventManager()
        { 
        }

        public Action<Tower> TowerShot { get; set; }

        public Action EnemyDied { get; set; }

        public Action PlacedATower { get; set; }

        /// <summary>
        /// Gets or sets the wave started delegate.
        /// </summary>
        public Action WaveStarted { get; set; }

        /// <summary>
        /// Gets or sets the enemy reached goal delegate.
        /// </summary>
        public Action EnemyReachedGoal { get; set; } 

        /// <summary>
        /// Gets or sets the on mouse click delegate, with the mouse position.
        /// </summary>
        public Action<Point> OnMouseClick { get; set; }

        /// <summary>
        /// Gets or sets the on mouse up delegate, with the mouse position.
        /// </summary>
        public Action<Point> OnMouseUp { get; set; }

        /// <summary>
        /// Gets or sets the on mouse down delegate, with the mouse position.
        /// </summary>
        public Action<Point> OnMouseDown { get; set; }

        /// <summary>
        /// Gets or sets the tower was selected delegate, with the selected tower.
        /// </summary>
        public Action<Tower> TowerWasSelected { get; set; }

        /// <summary>
        /// Gets or sets a tower was deselected delegate.
        /// </summary>
        public Action DeselectedTower { get; set; }
    }
}
