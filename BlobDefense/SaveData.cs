using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlobDefense
{
    using BlobDefense.Enemies;
    using BlobDefense.Towers;
    using BlobDefense.WaveSpawner;

    /// <summary>
    /// Contains all the data which will be saved.
    /// </summary>
    [Serializable]
    internal class SaveData
    {
        public SaveData()
        {
            this.Towers = GameObject.AllGameObjects.OfType<Tower>().ToList();
            this.NodeMap = TileEngine.Instance.NodeMap;
            this.Currency = GameManager.Instance.Currency;
            this.CurrentWave = WaveManager.Instance.CurrentWave - 1;
            this.TotalKills = GameManager.Instance.TotalKills;
        }
        
        public List<Tower> Towers { get; set; }

        public MapNode[,] NodeMap { get; set; }

        public int Currency { get; set; }

        public int CurrentWave { get; set; }

        public int TotalKills { get; set; }

        public void ApplySaveData()
        {
            WaveManager.Instance.CurrentWave = this.CurrentWave;
            GameManager.Instance.GiveCurrency(this.Currency);
            TileEngine.Instance.NodeMap = this.NodeMap;
            GameManager.Instance.TotalKills = this.TotalKills;
        }
    }
}
