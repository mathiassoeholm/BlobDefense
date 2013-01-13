// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AudioManager.cs" company="ClearCut Games/Mathias">
//   © 2012
// </copyright>
// <summary>
//   Keeps track of all audio files and is responsible for playing them.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace BlobDefense
{
    using System;

    using BlobDefense.Enemies;
    using BlobDefense.Towers;

    using IrrKlang;

    /// <summary>
    /// Keeps track of all audio files and is responsible for playing them.
    /// </summary>
    internal class AudioManager : Singleton<AudioManager>
    {
        #region Sounds
        /// <summary>
        /// The sound played when a boss dies.
        /// </summary>
        public const string BossDeathSound = @"Audio/BossDeath.wav";
        
        /// <summary>
        /// The sound played when a enemy dies.
        /// </summary>
        public const string DeathSound = @"Audio/DeathSound.wav";

        /// <summary>
        /// The sound played when a enemy dies.
        /// </summary>
        public const string DeathSound2 = @"Audio/DeathSound2.wav";

        /// <summary>
        /// The sound played when a enemy dies.
        /// </summary>
        public const string DeathSound3 = @"Audio/DeathSound3.wav";

        /// <summary>
        /// The sound played when a tower shoots.
        /// </summary>
        public const string PewShootSound = @"Audio/PewShoot.wav";

        /// <summary>
        /// The sound played when a tower shoots.
        /// </summary>
        public const string PewShootSound2 = @"Audio/PewShoot2.wav";

        /// <summary>
        /// The sound played when a tower shoots.
        /// </summary>
        public const string PewShootSound3 = @"Audio/PewShoot3.wav";

        /// <summary>
        /// The sound played when a tower shoots.
        /// </summary>
        public const string BlubShootSound = @"Audio/BlubShoot.wav";

        /// <summary>
        /// The sound played when a tower shoots.
        /// </summary>
        public const string SniperShootSound = @"Audio/SniperShoot.wav";

        /// <summary>
        /// The sound played when a tower shoots.
        /// </summary>
        public const string BlubShootSound2 = @"Audio/BlubShoot2.wav";

        /// <summary>
        /// The sound played when placing a tower
        /// </summary>
        public const string PlaceTowerSound = @"Audio/PlaceTowerSound.wav";
        #endregion

        /// <summary>
        /// The sound engine instance, used to play all audio.
        /// </summary>
        private ISoundEngine soundEngine;


        /// <summary>
        /// Prevents a default instance of the <see cref="AudioManager"/> class from being created.
        /// </summary>
        private AudioManager()
        {
            // Subscribe to some events with the play sound method
            EventManager.Instance.EnemyDied += this.PlayEnemyDeathSound;
            EventManager.Instance.TowerShot += this.PlayTowerShotSound;
            EventManager.Instance.PlacedATower += () => this.PlaySoundOnce(PlaceTowerSound);
        }

        /// <summary>
        /// Initializes a new instance of the sound engine, and loads the music.
        /// </summary>
        public void InitializeSoundEngine()
        {
            this.soundEngine = new ISoundEngine();
        }

        private void PlayTowerShotSound(Tower tower)
        {
           if (tower is AgilityTower)
           {
               this.PlayRandomSoundOnce(BlubShootSound, BlubShootSound2);
           }
           else if (tower is StandardTower)
           {
               this.PlayRandomSoundOnce(PewShootSound, PewShootSound2, PewShootSound3);
           }
           else
           {
               this.PlayRandomSoundOnce(SniperShootSound);
           }
        }

        private void PlayEnemyDeathSound(Enemy enemy)
        {
            if (enemy is Boss)
            {
                this.PlaySoundOnce(BossDeathSound);
            }
            else
            {
                this.PlayRandomSoundOnce(DeathSound, DeathSound2, DeathSound3);
            }
        }

        /// <summary>
        /// Plays a 2D sound once.
        /// </summary>
        /// <param name="sound">
        /// The path of the sound to be played.
        /// </param>
        public void PlaySoundOnce(string sound)
        {
            this.soundEngine.Play2D(sound);
        }

        public void PlayRandomSoundOnce(params string[] sounds)
        {
            Random random = new Random();
            this.soundEngine.Play2D(sounds[random.Next(0, sounds.Length)]);
        }

    }
}
