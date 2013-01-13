// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GameManager.cs" company="Backdoor Fun">
//   © 2013
// </copyright>
// <summary>
//   Takes care of managing the games overall state.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace BlobDefense
{
    using System;
    using System.IO;
    using System.Runtime.Serialization.Formatters.Binary;

    using BlobDefense.HighScore;
    using BlobDefense.WaveSpawner;

    using Extensions;

    /// <summary>
    /// Takes care of managing the games overall state.
    /// </summary>
    internal class GameManager : Singleton<GameManager>
    {
        /// <summary>
        /// Prevents a default instance of the <see cref="GameManager"/> class from being created.
        /// </summary>
        private GameManager()
        {
            // Set initial currency and lives
            this.Currency = GameSettings.InitialCurrencyAmount;
            this.Lives = GameSettings.StartLives;

            // Subscribe to events
            EventManager.Instance.EnemyReachedGoal += this.LoseLife;
            EventManager.Instance.EnemyDied += enemy => this.TotalKills++;
            EventManager.Instance.WaveStarted += this.SaveGame;
        }

        /// <summary>
        /// Gets the amount of currency that player has.
        /// </summary>
        public int Currency { get; private set; }

        /// <summary>
        /// Gets or sets the amount of lives that player has left.
        /// </summary>
        public int Lives { get; set; }

        /// <summary>
        /// Gets or sets the amount of enemies killed.
        /// </summary>
        public int TotalKills { get; set; }

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

        /// <summary>
        /// Starts a new game from the beginning.
        /// </summary>
        public void StartNewGame()
        {
            this.Lives = GameSettings.StartLives;
            this.Currency = GameSettings.InitialCurrencyAmount;
            this.TotalKills = 0;

            // Initialize the wave manager
            WaveManager.Instance.InitializeWaveManager();

            // Load the tile map
            TileEngine.Instance.LoadMapFromXml();

            // Initialize the game manager
            GameLogic.Instance.InitializeGameLogic();

            // Create a path for the enemies
            GameLogic.Instance.TryCreateNewPath();

            this.CurrentGameState = GameState.Playing;
        }

        /// <summary>
        /// Continues the game from a save data file.
        /// </summary>
        public void ContinueGame()
        {
            // Initialize the wave manager
            WaveManager.Instance.InitializeWaveManager();

            // Reset the currency
            this.Currency = 0;

            // Load the game
            this.LoadGame();

            // Initialize the game manager
            GameLogic.Instance.InitializeGameLogic();

            // Create a path for the enemies
            GameLogic.Instance.TryCreateNewPath();

            this.CurrentGameState = GameState.Playing;
        }

        /// <summary>
        /// Changes the games state to be in the main menu.
        /// </summary>
        public void GoToMainMenu()
        {
            this.CurrentGameState = GameState.MainMenu;

            EventManager.Instance.OpenedMainMenu.SafeInvoke();
        }

        /// <summary>
        /// Saves the games state to the save data path.
        /// </summary>
        private void SaveGame()
        {
            using (var fileStream = new FileStream(GameSettings.SaveDataPath + @"\SavedGame.dat", FileMode.Create, FileAccess.Write))
            {
                var formatter = new BinaryFormatter();

                // Save the game
                formatter.Serialize(fileStream, new SaveData());
            }
        }

        /// <summary>
        /// Loads the games state from the save data path.
        /// </summary>
        private void LoadGame()
        {
            if (!Directory.Exists(GameSettings.SaveDataPath))
            {
                Directory.CreateDirectory(GameSettings.SaveDataPath);
            }

            using (var fileStream = new FileStream(GameSettings.SaveDataPath + @"\SavedGame.dat", FileMode.OpenOrCreate, FileAccess.Read))
            {
                var formatter = new BinaryFormatter();

                fileStream.Position = 0;

                if (fileStream.Length > 0)
                {
                    SaveData saveData = (SaveData)formatter.Deserialize(fileStream);
                    saveData.ApplySaveData();
                    saveData.Towers.ForEach(tower => tower.InitializeGameObject());
                }
            }
        }

        /// <summary>
        /// Subtracts a life and stops the game if necessary.
        /// </summary>
        private void LoseLife()
        {
            if (this.CurrentGameState != GameState.Playing)
            {
                return;
            }
            
            this.Lives = Math.Max(0, this.Lives - 1);

            if (this.Lives == 0)
            {
                this.EndGame();
            }
        }

        /// <summary>
        /// Stops the game and goes to the game over screen.
        /// </summary>
        private void EndGame()
        {
            // Stop the wave
            WaveManager.Instance.StopWave();

            // Add any potential new game objects
            GameObject.AddAllNewGameObjects();

            // Remove all game objects
            GameObject.AllGameObjects.RemoveAll(go => !go.NeverDestroy);
            
            // Save score
            ScoreManager.Instance.AddScore(WaveManager.Instance.CurrentWave + 1);

            // Sort score
            ScoreManager.Instance.SortScores();

            // Delete the save data
            File.Delete(GameSettings.SaveDataPath + @"\SavedGame.dat");

            EventManager.Instance.LostTheGame.SafeInvoke();

            this.CurrentGameState = GameState.GameOver;
        }
    }
}
