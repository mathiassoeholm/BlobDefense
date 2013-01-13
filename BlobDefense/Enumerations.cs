// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Enumerations.cs" company="Backdoor Fun">
//   © 2013
// </copyright>
// <summary>
//   contians public enumerations used in blob defense
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace BlobDefense
{
    /// <summary>
    /// The tile direction for animations.
    /// </summary>
    public enum TileDirection : byte
    {
        /// <summary>
        /// Tile direction going down.
        /// </summary>
        Down,

        /// <summary>
        /// Tile direction going right.
        /// </summary>
        Right
    }

    /// <summary>
    /// The possible game states.
    /// </summary>
    public enum GameState : byte
    {
        /// <summary>
        /// Game state indicating we are in the main menu.
        /// </summary>
        MainMenu,

        /// <summary>
        /// Game state indicating we are currently playing the game.
        /// </summary>
        Playing,

        /// <summary>
        /// Game state indicating that the game has ended.
        /// </summary>
        GameOver
    }
}

