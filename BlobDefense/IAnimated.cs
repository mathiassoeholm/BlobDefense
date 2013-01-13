// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IAnimated.cs" company="Backdoor Fun">
//   © 2013
// </copyright>
// <summary>
//   Implemented by game objcts which will have animation.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace BlobDefense
{
    /// <summary>
    /// Implemented by game objects which will have animation.
    /// </summary>
    internal interface IAnimated
    {
        /// <summary>
        /// Gets the current animation.
        /// </summary>
        Animation CurrentAnimation { get; }

        /// <summary>
        /// Should run the current animation.
        /// </summary>
        void RunAnimation();
    }
}
