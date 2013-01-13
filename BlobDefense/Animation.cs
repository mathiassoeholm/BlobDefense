// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Animation.cs" company="Backdoor Fun">
//   © 2013
// </copyright>
// <summary>
//   Defines an 2D animation for a game object contained on a sprite sheet.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace BlobDefense
{
    using System;
    using System.Drawing;

    /// <summary>
    /// Defines an 2D animation for a game object contained on a sprite sheet.
    /// </summary>
    [Serializable]
    internal class Animation
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

        /// <summary>
        /// Initializes a new instance of the <see cref="Animation"/> class.
        /// </summary>
        /// <param name="fps"> The frames per second for the animation. </param>
        /// <param name="animatedObject"> The animated game object. </param>
        /// <param name="frameCount"> The amount of frames. </param>
        /// <param name="firstFrame"> A rectangle defining the size and position for the first frame. </param>
        /// <param name="tileDirection"> The animations tile direction on the sprite sheet. </param>
        public Animation(int fps, GameObject animatedObject, int frameCount, Rectangle firstFrame, TileDirection tileDirection)
        {
            // Assign class members
            this.animatedObject = animatedObject;

            // Calculate the time duration between each frame.
            this.frameDeltaTime = 1f / fps;

            // We changed frame, so next frame will be in 'frame delta' seconds
            this.lastFrameChange = DateTime.Now;

            // Set frame sources
            this.frameSources = new Rectangle[frameCount];

            for (int i = 0; i < frameCount; i++)
            {
                switch (tileDirection)
                {
                    case TileDirection.Down:
                        this.frameSources[i] = new Rectangle(firstFrame.X, firstFrame.Y + (firstFrame.Height * i), firstFrame.Width, firstFrame.Height);
                        break;
                    case TileDirection.Right:
                        this.frameSources[i] = new Rectangle(firstFrame.X + (firstFrame.Width * i), firstFrame.Y, firstFrame.Width, firstFrame.Height);
                        break;
                }
            }
        }

        /// <summary>
        /// Runs the animation.
        /// </summary>
        public void RunAnimation()
        {
            // Check if it is time to change frame
            if (DateTime.Now.Subtract(this.lastFrameChange).TotalSeconds >= this.frameDeltaTime / Time.TimeScale)
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
