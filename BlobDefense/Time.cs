using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlobDefense
{
    public static class Time
    {
        private static DateTime lastFrameTime;

        public static float DeltaTime { get; set; }
        
        public static void SetDeltaTime()
        {
            DeltaTime = (float)DateTime.Now.Subtract(lastFrameTime).TotalSeconds;

            lastFrameTime = DateTime.Now;
        }
    }
}
