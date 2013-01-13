// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SaveData.cs" company="Backdoor Fun">
//   © 2013
// </copyright>
// <summary>
//   Contains all the data which will be saved.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace BlobDefense
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using BlobDefense.Towers;
    using BlobDefense.WaveSpawner;

    /// <summary>
    /// Contains all the data which will be saved.
    /// </summary>
    [Serializable]
    internal class SaveData
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SaveData"/> class.
        /// </summary>
        public SaveData()
        {
            // Sets all the data to save
            this.Towers = GameObject.AllGameObjects.OfType<Tower>().ToList();
            this.NodeMap = TileEngine.Instance.NodeMap;
            this.Currency = GameManager.Instance.Currency;
            this.CurrentWave = WaveManager.Instance.CurrentWave - 1;
            this.TotalKills = GameManager.Instance.TotalKills;
            this.Lives = GameManager.Instance.Lives;
            this.PlayerName = GameSettings.PlayerName;
        }

        /// <summary>
        /// Gets the towers to save.
        /// </summary>
        public List<Tower> Towers { get; private set; }

        /// <summary>
        /// Gets the node map to save.
        /// </summary>
        public MapNode[,] NodeMap { get; private set; }

        /// <summary>
        /// Gets the currency amount to save.
        /// </summary>
        public int Currency { get; private set; }

        /// <summary>
        /// Gets the current wave to save.
        /// </summary>
        public int CurrentWave { get; private set; }

        /// <summary>
        /// Gets the total kills to save.
        /// </summary>
        public int TotalKills { get; private set; }

        /// <summary>
        /// Gets the lives to save.
        /// </summary>
        public int Lives { get; private set; }

        /// <summary>
        /// Gets the player name the save.
        /// </summary>
        public string PlayerName { get; private set; }

        /// <summary>
        /// Applies all the save data to the games settings.
        /// </summary>
        public void ApplySaveData()
        {
            WaveManager.Instance.CurrentWave = this.CurrentWave;
            GameManager.Instance.GiveCurrency(this.Currency);
            TileEngine.Instance.NodeMap = this.NodeMap;
            GameManager.Instance.TotalKills = this.TotalKills;
            GameManager.Instance.Lives = this.Lives;
            GameSettings.PlayerName = this.PlayerName;
        }
    }
}
