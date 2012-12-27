﻿using System;
using System.Drawing;

namespace BlobDefense
{
    class Animation
    {
        /// <summary>
        /// The amount of seconds between each frame.
        /// </summary>
        private readonly float frameDeltaTime;

        /// <summary>
        /// The frame sources for the sprite sheet, describes where the animation is.
        /// </summary>
        private readonly Rectangle[] frameSources;

        /// <summary>
        /// The object which is being animated.
        /// </summary>
        private readonly GameObject animatedObject;

        /// <summary>
        /// The current animation index, used by the frame sources array.
        /// </summary>
        private int currentFrame;

        /// <summary>
        /// The point in time where we changed to the current frame.
        /// </summary>
        private DateTime lastFrameChange;

        public Animation(int fps, GameObject animatedObject, params Rectangle[] frameSources)
        {
            // Assign class members
            this.frameSources = frameSources;
            this.animatedObject = animatedObject;
            
            // Calculate the time duration between each frame.
            this.frameDeltaTime = 1f / fps;

            // We changed frame, so next frame will be in 'frame delta' seconds
            this.lastFrameChange = DateTime.Now;
        }

        public void RunAnimation()
        {
            // Check if it is time to change frame
            if (DateTime.Now.Subtract(this.lastFrameChange).Seconds >= this.frameDeltaTime)
            {
                // Go to next frame
                this.currentFrame++;

                // Make sure we don't go out of bounds
                this.currentFrame %= this.frameSources.Length;

                // Set the source rectangle for the animated game object
                this.animatedObject.SpriteSheetSource = this.frameSources[this.currentFrame];
                
                // We changed frame now, so next frame will be in 'frame delta' seconds
                this.lastFrameChange = DateTime.Now;
            }
        }
    }
}
