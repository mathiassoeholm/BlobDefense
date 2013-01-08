using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlobDefense.HighScore
{
    using System.Drawing;
    using System.IO;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.Windows.Forms;

    using BlobDefense.Gui;

    class ScoreManager : Singleton<ScoreManager>
    {
        private const int AmountOfScoresToShow = 10;

        private List<HighScore> highScores;

        /// <summary>
        /// Prevents a default instance of the <see cref="ScoreManager"/> class from being created.
        /// </summary>
        private ScoreManager()
        {
            this.highScores = new List<HighScore>();
            
            this.LoadScores();
        }

        public void SortScores()
        {
            this.highScores.Sort();
        }

        /// <summary>
        /// Adds a score to the highscore list, if name exist just change score for player
        /// </summary>
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
        /// Deserializes score objects in the appdata foler, and adds them to our highscore list
        /// </summary>
        private void LoadScores()
        {
            this.FillLeaderBoardsIfEmpty();

            if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData)))
            {
                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData));
            }
                
            using (var fileStream = new FileStream(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + @"\blobscores.dat", FileMode.OpenOrCreate, FileAccess.Read))
            {
                var formatter = new BinaryFormatter();

                if (fileStream.Length > 0)
                {
                    this.highScores = (List<HighScore>)formatter.Deserialize(fileStream);
                }
            }
        }

        /// <summary>
        /// Saves the current highscore list to the app data folder
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
        public void DrawLeaderBoards(Graphics graphics, int offset = 0)
        {
            GuiManager.Instance.WriteText(graphics, "Leader Boards", GameDisplay.FormWidth / 2, offset, 16, Color.Gold, true);

            // Draw high scores
            for (int i = 0 ; i < Math.Min(AmountOfScoresToShow, this.highScores.Count); i++)
            {
                GuiManager.Instance.WriteText(
                    graphics,
                    (i + 1).ToString() + ": " + this.highScores[i].PlayerName + " -  Wave " + this.highScores[i].Score.ToString(),
                    GameDisplay.FormWidth / 2,
                    (i + 1) * 20 + offset,
                    16,
                    this.highScores[i].PlayerName == GameSettings.PlayerName ? Color.GreenYellow : Color.White,
                    true);
            }

            GuiManager.Instance.WriteText(graphics, "Press enter to exit", GameDisplay.FormWidth / 2, GameDisplay.FormHeight - 80, 16, Color.White, true);

            // Check if esc is pressed, then go to main menu
            if (Keyboard.IsKeyDown(Keys.Enter))
            {
                GameManager.Instance.GoToMainMenu();
            }
        }

        /// <summary>
        /// Fills the leaderbaords with some standard names.
        /// This method is primarily used for testing the leader boards
        /// </summary>
        private void FillLeaderBoardsIfEmpty()
        {
            if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData)))
            {
                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData));
            }

            using (var fileStream = new FileStream(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + @"\blobscores.dat", FileMode.OpenOrCreate, FileAccess.Read))
            {
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
