using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlobDefense.HighScore
{
    [Serializable]
    internal class HighScore : IComparable<HighScore>
    {
        // Constructor
        public HighScore(string name, int score)
        {
            this.PlayerName = name;
            this.Score = score;
        }

        // Properties
        public int Score { get; set; }

        public string PlayerName { get; private set; }

        public int CompareTo(HighScore otherScore)
        {
            return this.Score > otherScore.Score ? -1 : 1;
        }
    }
}
