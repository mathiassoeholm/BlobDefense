using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlobDefense
{
    using BlobDefense.HighScore;
    using BlobDefense.WaveSpawner;

    internal class GameManager : Singleton<GameManager>
    {
        /// <summary>
        /// Prevents a default instance of the <see cref="GameManager"/> class from being created.
        /// </summary>
        private GameManager()
        {
            this.Currency = GameSettings.InitialCurrencyAmount;
            this.Lives = GameSettings.StartLives;
            EventManager.Instance.EnemyReachedGoal += this.LoseLife;
        }

        /// <summary>
        /// Gets the amount of currency that player has.
        /// </summary>
        public int Currency { get; private set; }

        /// <summary>
        /// Gets the amount of lives that player has left.
        /// </summary>
        public int Lives { get; private set; }


        /// <summary>
        /// Gets or sets the current game state.
        /// </summary>
        public GameState CurrentGameState { get; set; }

        /// <summary>
        /// This method is called to create the instance.
        /// </summary>
        public void InitializeManager()
        {
        }

        /// <summary>
        /// Subtracts the price from the players currency, if the player has enough.
        /// </summary>
        /// <param name="price">
        /// The price of the item being bought.
        /// </param>
        /// <returns>
        /// A value indicating whether the player has enough currency to buy the item.
        /// </returns>
        public bool TryBuy(int price)
        {
            if (this.Currency >= price)
            {
                this.Currency -= price;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Gives currency to the player.
        /// </summary>
        /// <param name="amount">
        /// The amount of currency to give.
        /// </param>
        public void GiveCurrency(int amount)
        {
            this.Currency += amount;
        }

        public void StartNewGame()
        {
            this.CurrentGameState = GameState.Playing;
        }

        private void LoseLife()
        {
            this.Lives = Math.Max(0, this.Lives - 1);

            if (this.Lives == 0)
            {
                this.EndGame();
            }
        }

        private void EndGame()
        {
            // Save score
            ScoreManager.Instance.AddScore(WaveManager.Instance.CurrentWave);

            // Sort score
            ScoreManager.Instance.SortScores();

            this.CurrentGameState = GameState.GameOver;
        }
    }
}
