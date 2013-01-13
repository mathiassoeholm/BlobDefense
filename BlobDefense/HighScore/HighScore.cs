// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HighScore.cs" company="Backdoor Fun">
//   © 2013
// </copyright>
// <summary>
//   Defines a single high score, containing the score and a name.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace BlobDefense.HighScore
{
    using System;

    /// <summary>
    /// Defines a single high score, containing the score and a name.
    /// </summary>
    [Serializable]
    internal class HighScore : IComparable<HighScore>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HighScore"/> class.
        /// </summary>
       /// <param name="name">
       /// The name of the player.
       /// </param>
       /// <param name="score">
       /// The players score.
       /// </param>
        public HighScore(string name, int score)
        {
            this.PlayerName = name;
            this.Score = score;
        }

        /// <summary>
        /// Gets or sets the players score.
        /// </summary>
        public int Score { get; set; }

        /// <summary>
        /// Gets or set the players name.
        /// </summary>
        public string PlayerName { get; private set; }

        /// <summary>
        /// Compares two high score objects, when sorted the best player comes first.
        /// </summary>
        /// <param name="otherScore">
        /// The score to compare with.
        /// </param>
        /// <returns>
        /// 1, 0 or -1 based on the score values.
        /// </returns>
        public int CompareTo(HighScore otherScore)
        {
            if (this.Score == otherScore.Score)
            {
                // Return 0 if they are equal
                return 0;
            }
            
            return this.Score > otherScore.Score ? -1 : 1;
        }
    }
}
