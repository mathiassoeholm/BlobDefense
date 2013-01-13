// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Time.cs" company="Backdoor Fun">
//   © 2013
// </copyright>
// <summary>
//   Contains static properties and methods related to time.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace BlobDefense
{
    using System;

    using BlobDefense.WaveSpawner;

    /// <summary>
    /// Contains static properties and methods related to time.
    /// </summary>
    public static class Time
    {
        /// <summary>
        /// The point in time that the last frame was run.
        /// </summary>
        private static DateTime lastFrameTime;

        /// <summary>
        /// The time scale, used to control game speed.
        /// </summary>
        private static float timeScale = 1;

        /// <summary>
        /// The time in seconds between each frame.
        /// </summary>
        private static float deltaTime;

        /// <summary>
        /// Gets the time in seconds between each frame.
        /// </summary>
        public static float DeltaTime
        {
            get
            {
                return deltaTime;
            }

            private set
            {
                // Multiply with time scale
                deltaTime = value * timeScale;
            }
        }

        /// <summary>
        /// Gets the time in seconds between each frame unaffected by time scale.
        /// </summary>
        public static float RealDeltaTime
        {
            get
            {
                return deltaTime / timeScale;
            }
        }

        /// <summary>
        /// Gets or sets the time scale, used to control game speed.
        /// </summary>
        public static float TimeScale
        {
            get
            {
                return timeScale;
            }

            set
            {
                timeScale = value;
                WaveManager.Instance.ScaleSpawnInterval((int)value);
            }
        }

        /// <summary>
        /// Sets the delta time, this should be called once each frame.
        /// </summary>
        public static void SetDeltaTime()
        {
            DeltaTime = (float)Math.Max(DateTime.Now.Subtract(lastFrameTime).TotalSeconds, 0.001);

            lastFrameTime = DateTime.Now;
        }
    }
}
