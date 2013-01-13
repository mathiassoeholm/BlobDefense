// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EventManager.cs" company="Backdoor Fun">
//   © 2013
// </copyright>
// <summary>
//   Contains events that can be invoked or subscribed to, helps decoupling classes.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace BlobDefense
{
    using System;
    using System.Drawing;

    using BlobDefense.Enemies;
    using BlobDefense.Towers;

    /// <summary>
    /// Contains events that can be invoked or subscribed to, helps decoupling classes.
    /// </summary>
    internal class EventManager : Singleton<EventManager>
    {
        /// <summary>
        /// Prevents a default instance of the <see cref="EventManager"/> class from being created.
        /// </summary>
        private EventManager()
        { 
        }

        /// <summary>
        /// Gets or sets the opened main menu delegate.
        /// </summary>
        public Action OpenedMainMenu { get; set; }

        /// <summary>
        /// Gets or sets the tower shot delegate.
        /// </summary>
        public Action<Tower> TowerShot { get; set; }

        /// <summary>
        /// Gets or sets the enemy died delegate.
        /// </summary>
        public Action<Enemy> EnemyDied { get; set; }

        /// <summary>
        /// Gets or sets the placed a tower delegate.
        /// </summary>
        public Action PlacedATower { get; set; }

        /// <summary>
        /// Gets or sets the lost the game delegate.
        /// </summary>
        public Action LostTheGame { get; set; }

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
