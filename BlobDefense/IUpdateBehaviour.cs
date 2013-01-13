// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IUpdateBehaviour.cs" company="Backdoor Fun">
//   © 2013
// </copyright>
// <summary>
//   The Update Behavior interface.
//   Implemented by all game objects which performs an action each frame.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace BlobDefense
{
    /// <summary>
    /// The Update Behavior interface.
    /// Implemented by all game objects which performs an action each frame.
    /// </summary>
    internal interface IUpdateBehaviour
    {
        /// <summary>
        /// The update method, called once per frame.
        /// </summary>
        void Update();
    }
}
