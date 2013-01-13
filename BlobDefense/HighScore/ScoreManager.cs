// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ScoreManager.cs" company="Backdoor Fun">
//   © 2013
// </copyright>
// <summary>
//   Handles everything to do with scores and leaderboards.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace BlobDefense.HighScore
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.Windows.Forms;

    using BlobDefense.Gui;

    /// <summary>
    /// Handles everything to do with scores and leaderboards.
    /// </summary>
    internal class ScoreManager : Singleton<ScoreManager>
    {
        /// <summary>
        /// The amount of scores to show on the screen.
        /// </summary>
        private const int AmountOfScoresToShow = 10;

        /// <summary>
        /// The list of all high scores.
        /// </summary>
        private List<HighScore> highScores;

        /// <summary>
        /// Prevents a default instance of the <see cref="ScoreManager"/> class from being created.
        /// </summary>
        private ScoreManager()
        {
            this.highScores = new List<HighScore>();
            
            // Load high score save data.
            this.LoadScores();
        }

        /// <summary>
        /// Sorts the list of high scores.
        /// </summary>
        public void SortScores()
        {
            this.highScores.Sort();
        }

        /// <summary>
        /// Adds a score to the high score list,
        /// if name exists just change score for player.
        /// </summary>
        /// <param name="newScore">
        /// The score gained.
        /// </param>
        public void AddScore(int newScore)
        {
            HighScore currentPlayerHighScore = this.highScores.FirstOrDefault(highScore => highScore.PlayerName == GameSettings.PlayerName);

            // If player already has a score
            if (currentPlayerHighScore != null)
            {
                // Only set score for player if higher than current highscore
                if (newScore > currentPlayerHighScore.Score)
                {
                    currentPlayerHighScore.Score = newScore;
                }
            }
            else
            {
                // Add new player to high scores
                this.highScores.Add(new HighScore(GameSettings.PlayerName, newScore));
            }

            // Save scores
            this.SaveScores();
        }

        /// <summary>
        /// Saves the current high score list.
        /// </summary>
        public void SaveScores()
        {
            using (var fileStream = new FileStream(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + @"\blobscores.dat", FileMode.Create, FileAccess.Write))
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(fileStream, this.highScores);
            }
        }

        /// <summary>
        /// Draws the leader boards current page to the console and page navigation
        /// </summary>
        /// <param name="graphics">
        /// The graphics object used to draw the leaderboards.
        /// </param>
        /// <param name="offset">
        /// An optional top offset.
        /// </param>
        public void DrawLeaderBoards(Graphics graphics, int offset = 0)
        {
            // Write the leader boards title
            GuiManager.Instance.WriteText(graphics, "Leader Boards", GameDisplay.FormWidth / 2, offset, 16, Color.Gold, true);

            // Draw high scores
            for (int i = 0; i < Math.Min(AmountOfScoresToShow, this.highScores.Count); i++)
            {
                GuiManager.Instance.WriteText(
                    graphics,
                    (i + 1).ToString() + ": " + this.highScores[i].PlayerName + " -  Wave " + this.highScores[i].Score.ToString(),
                    GameDisplay.FormWidth / 2,
                    ((i + 1) * 20) + offset,
                    16,
                    this.highScores[i].PlayerName == GameSettings.PlayerName ? Color.GreenYellow : Color.White,
                    true);
            }

            // Draw press enter text
            GuiManager.Instance.WriteText(graphics, "Press enter to exit", GameDisplay.FormWidth / 2, GameDisplay.FormHeight - 80, 16, Color.White, true);

            // Check if enter is pressed, then go to main menu
            if (Keyboard.IsKeyDown(Keys.Enter))
            {
                GameManager.Instance.GoToMainMenu();
            }
        }

        /// <summary>
        /// Deserializes score objects, and adds them to our high score list.
        /// </summary>
        private void LoadScores()
        {
            // Fill in the leader boards if they are empty
            this.FillLeaderBoardsIfEmpty();

            // Check if the directory exists
            if (!Directory.Exists(GameSettings.SaveDataPath))
            {
                Directory.CreateDirectory(GameSettings.SaveDataPath);
            }

            using (var fileStream = new FileStream(GameSettings.SaveDataPath + @"\blobscores.dat", FileMode.OpenOrCreate, FileAccess.Read))
            {
                var formatter = new BinaryFormatter();

                if (fileStream.Length > 0)
                {
                    // Load data to the high scores list
                    this.highScores = (List<HighScore>)formatter.Deserialize(fileStream);
                }
            }
        }

        /// <summary>
        /// Fills the leader boards with some standard names.
        /// This method is primarily used for testing the leader boards
        /// </summary>
        private void FillLeaderBoardsIfEmpty()
        {
            // Check if the directory exists
            if (!Directory.Exists(GameSettings.SaveDataPath))
            {
                Directory.CreateDirectory(GameSettings.SaveDataPath);
            }

            using (var fileStream = new FileStream(GameSettings.SaveDataPath + @"\blobscores.dat", FileMode.OpenOrCreate, FileAccess.Read))
            {
                // Add standard players if leader boards are empty
                if (fileStream.Length == 0)
                {
                    this.highScores.Add(new HighScore("Mathias", 100));
                    this.highScores.Add(new HighScore("Player1", 0));
                    this.highScores.Add(new HighScore("Player2", 0));
                    this.highScores.Add(new HighScore("Player3", 0));
                    this.highScores.Add(new HighScore("Player4", 0));
                    this.highScores.Add(new HighScore("Player5", 0));
                    this.highScores.Add(new HighScore("Player6", 0));
                    this.highScores.Add(new HighScore("Player7", 0));
                    this.highScores.Add(new HighScore("Player8", 0));
                    this.highScores.Add(new HighScore("Player9", 0));
                    this.highScores.Add(new HighScore("Player10", 0));
                    this.highScores.Add(new HighScore("Player11", 0));
                    this.highScores.Add(new HighScore("Player12", 0));
                    this.highScores.Add(new HighScore("Player13", 0));
                    this.highScores.Add(new HighScore("Player14", 0));

                    fileStream.Dispose();

                    // Save scores
                    this.SaveScores();
                }
            }
        }
    }
}
