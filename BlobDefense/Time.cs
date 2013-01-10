using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlobDefense
{
    using BlobDefense.WaveSpawner;

    public static class Time
    {
        private static DateTime lastFrameTime;

        private static float timeScale = 1;

        private static float deltaTime;

        public static float DeltaTime
        {
            get
            {
                return deltaTime;
            }
            private set
            {
                deltaTime = value * timeScale;
            }
        }

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

        public static void SetDeltaTime()
        {
            DeltaTime = (float)Math.Max(DateTime.Now.Subtract(lastFrameTime).TotalSeconds, 0.001);

            lastFrameTime = DateTime.Now;
        }
    }
}
