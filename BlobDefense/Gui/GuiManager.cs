// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GuiManager.cs" company="Backdoor Fun">
//   © 2013
// </copyright>
// <summary>
//   Defines the graphical user interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace BlobDefense.Gui
{
    using System.Drawing;
    using System.Linq;

    using BlobDefense.Enemies;
    using BlobDefense.HighScore;
    using BlobDefense.Towers;
    using BlobDefense.WaveSpawner;

    using Extensions;

    /// <summary>
    /// Defines the graphical user interface.
    /// </summary>
    internal class GuiManager : Singleton<GuiManager>
    {
        /// <summary>
        /// The pixel width of the GUI panel, positioned to the right.
        /// </summary>
        public const int RightPanelWidth = 215;

        /// <summary>
        /// The pixel size of the yellow selection rectangles border.
        /// </summary>
        private const int SelectionRectangleSize = 4;

        /// <summary>
        /// The universal offset from the left side of the panel.
        /// </summary>
        private const int GuiLeftOffset = 10;

        /// <summary>
        /// The offset from the top for all speed buttons.
        /// </summary>
        private const int SpeedBtnTopOffset = 53;

        /// <summary>
        /// The space between speed buttons.
        /// </summary>
        private const int SpaceBetweenSpeedButtons = 65;

        /// <summary>
        /// The tower options top offset.
        /// </summary>
        private const int TowerOptionsTopOffset = 200;

        /// <summary>
        /// The upgrade button top offset.
        /// </summary>
        private const int UpgradeButtonTopOffset = 315;

        /// <summary>
        /// The tower selection buttons top offset.
        /// </summary>
        private const int TowerButtonsTopOffset = 150;

        /// <summary>
        /// The spacing between the tower buttons.
        /// </summary>
        private const int TowerButtonsSpacing = 15;

        /// <summary>
        /// The button to start the next wave of enemies.
        /// </summary>
        private GuiButton nextWaveBtn;

        /// <summary>
        /// The button to set the speed to 100%.
        /// </summary>
        private GuiButton speed100Btn;

        /// <summary>
        /// The button to set the speed to 200%.
        /// </summary>
        private GuiButton speed200Btn;

        /// <summary>
        /// The button to set the speed to 400%.
        /// </summary>
        private GuiButton speed400Btn;

        /// <summary>
        /// The button to upgrade a selected tower.
        /// </summary>
        private GuiButton upgradeBtn;

        /// <summary>
        /// The button to destroy a selected tower.
        /// </summary>
        private GuiButton destroyBtn;

        /// <summary>
        /// The button to start building type one towers.
        /// </summary>
        private GuiButton towerOneBtn;

        /// <summary>
        /// The button to start building type two towers.
        /// </summary>
        private GuiButton towerTwoBtn;

        /// <summary>
        /// The button to start building type three towers.
        /// </summary>
        private GuiButton towerThreeBtn;

        /// <summary>
        /// The button to start building type four towers.
        /// </summary>
        private GuiButton towerFourBtn;

        /// <summary>
        /// The currently selected tower.
        /// </summary>
        private Tower selectedTower;

        /// <summary>
        /// Prevents a default instance of the <see cref="GuiManager"/> class from being created.
        /// </summary>
        private GuiManager()
        {
            // Select no tower as a start
            this.SelectedTowerToBuild = -1;
            
            // Initialize the ingame gui
            this.SetUpInGameGui();

            // Hide destroy and upgrade buttons
            this.destroyBtn.IsVisible = false;
            this.upgradeBtn.IsVisible = false;

            // Subscribe to selection events
            EventManager.Instance.TowerWasSelected += this.OnTowerSelected;
            EventManager.Instance.DeselectedTower += this.OnTowerDeselected;

            // Stop building a tower, if a tower was selected or a wave started
            EventManager.Instance.TowerWasSelected += notUsing => this.SelectedTowerToBuild = -1;
            EventManager.Instance.WaveStarted += () => this.SelectedTowerToBuild = -1;
        }

        /// <summary>
        /// Gets or set the ID for the tower to build.
        /// When clicking on a tower icon, this gets set to the appropriate value.
        /// -1 Means no tower.
        /// </summary>
        public int SelectedTowerToBuild { get; private set; }

        /// <summary>
        /// Draws a piece of text.
        /// </summary>
        /// <param name="graphics"> The graphics context used to draw the text. </param>
        /// <param name="text"> The text to write. </param>
        /// <param name="posX"> The x position of the text. </param>
        /// <param name="posY"> The y position of the text. </param>
        ///  <param name="size"> The font size. </param>
        /// <param name="color"> The color of the text. </param>
        /// <param name="centerText"> A value indicating whether to center the text or not. </param>
        public void WriteText(Graphics graphics, string text, int posX, int posY, int size, Color color, bool centerText = false)
        {
            if (centerText)
            {
                var stringFormat = new StringFormat { Alignment = StringAlignment.Center };

                // Draw centered text
                graphics.DrawString(
                text,
                new Font("Arial", size),
                new SolidBrush(color),
                posX,
                posY,
                stringFormat);
            }
            else
            {
                // Draw left aligned text
                graphics.DrawString(
                text,
                new Font("Arial", size),
                new SolidBrush(color),
                posX,
                posY);
            }
        }

        /// <summary>
        /// Draws the game over screen.
        /// </summary>
        /// <param name="graphics">
        /// The graphics context used to draw the game over screen.
        /// </param>
        public void DrawGameOverScreen(Graphics graphics)
        {
            // Write title
            this.WriteText(graphics, "Game Over", (int)(GameDisplay.FormWidth * 0.5f), 0, 28, Color.Gold, true);
            
            // Write stats
            this.WriteText(graphics, "Total kills: " + GameManager.Instance.TotalKills, (int)(GameDisplay.FormWidth * 0.5f), 35, 16, Color.White, true);
            this.WriteText(graphics, "Last wave: " + WaveManager.Instance.CurrentWave + 1, (int)(GameDisplay.FormWidth * 0.5f), 54, 16, Color.White, true);

            // Draw leader boards
            ScoreManager.Instance.DrawLeaderBoards(graphics, 85);
        }

        /// <summary>
        /// Draws the GUI used while playing.
        /// </summary>
        /// <param name="graphics">
        /// The graphics context used to draw the GUI.
        /// </param>
        public void DrawInGameGui(Graphics graphics)
        {
            // Draw all buttons
            GuiButton.AllButtons.ForEach(button => button.Draw(graphics));

            // Initalize the pen used to draw selection rectangle around speed buttons
            var selectedSpeedPen = new Pen(Color.Yellow, SelectionRectangleSize);

            // Draw a rectangle around selected speed button
            switch ((int)Time.TimeScale)
            {
                case 1:
                    graphics.DrawRectangle(selectedSpeedPen, this.speed100Btn.PositionAndSize);
                    break;
                case 2:
                    graphics.DrawRectangle(selectedSpeedPen, this.speed200Btn.PositionAndSize);
                    break;
                case 5:
                    graphics.DrawRectangle(selectedSpeedPen, this.speed400Btn.PositionAndSize);
                    break;
            }
            
            // Draw a selction rectangle around the selected type of tower
            this.DrawTowerButtonSelection(graphics);
            
            if (this.selectedTower != null)
            {
                // Draw selection rectangle around selected tower
                graphics.DrawRectangle(
                    new Pen(Color.Yellow),
                    new Rectangle(
                        (int)this.selectedTower.Position.X - (TileEngine.TilesOnSpriteSize / 2),
                        (int)this.selectedTower.Position.Y - (TileEngine.TilesOnSpriteSize / 2),
                        TileEngine.TilesOnSpriteSize,
                        TileEngine.TilesOnSpriteSize));

                // Draw range circle around the tower
                graphics.DrawEllipse(
                    Pens.Black,
                    new RectangleF(
                        this.selectedTower.Position.X - this.selectedTower.ShootRange,
                        this.selectedTower.Position.Y - this.selectedTower.ShootRange,
                        this.selectedTower.ShootRange * 2,
                        this.selectedTower.ShootRange * 2));

                int posY = TowerOptionsTopOffset;

                // Write selected tower
                graphics.DrawString(
                    "Selected tower:",
                    new Font("Arial", 16),
                    new SolidBrush(Color.White),
                    this.nextWaveBtn.PositionAndSize.X,
                    posY);

                // Write level and kills
                graphics.DrawString(
                    "Kills " + this.selectedTower.Kills.ToString(),
                    new Font("Arial", 16),
                    new SolidBrush(Color.White),
                    this.nextWaveBtn.PositionAndSize.X,
                    posY += 20);

                // Write damage
                graphics.DrawString(
                    "Damage " + ((int)this.selectedTower.AttackDamage).ToString(),
                    new Font("Arial", 16),
                    new SolidBrush(Color.White),
                    this.nextWaveBtn.PositionAndSize.X,
                    posY += 20);

                // Write range
                graphics.DrawString(
                    "Range " + ((int)this.selectedTower.ShootRange).ToString(),
                    new Font("Arial", 16),
                    new SolidBrush(Color.White),
                    this.nextWaveBtn.PositionAndSize.X,
                    posY += 20);

                // Write cooldown
                graphics.DrawString(
                    "Cooldown " + this.selectedTower.ShootCooldown.ToString() + " s",
                    new Font("Arial", 16),
                    new SolidBrush(Color.White),
                    this.nextWaveBtn.PositionAndSize.X,
                    posY += 20);

                // Write upgrade price
                graphics.DrawString(
                    "$" + this.selectedTower.UpgradePrice.ToString(),
                    new Font("Arial", 16),
                    new SolidBrush(Color.White),
                    this.upgradeBtn.PositionAndSize.X + this.upgradeBtn.PositionAndSize.Width,
                    this.upgradeBtn.PositionAndSize.Y + 10);
            }
            
            // Draw wave number
            graphics.DrawString(
                "Wave " + (WaveManager.Instance.CurrentWave + 1).ToString(),
                new Font("Arial", 16),
                new SolidBrush(Color.White),
                this.nextWaveBtn.PositionAndSize.X + this.nextWaveBtn.PositionAndSize.Width + 5,
                this.nextWaveBtn.PositionAndSize.Y);

            // Draw currency amount
            graphics.DrawString(
                "$" + GameManager.Instance.Currency.ToString(),
                    new Font("Arial", 16),
                    new SolidBrush(Color.White),
                    this.nextWaveBtn.PositionAndSize.X,
                    this.speed100Btn.PositionAndSize.Y + this.speed100Btn.PositionAndSize.Height + 5);

            // Draw amount of lives
            graphics.DrawString(
                "lives " + GameManager.Instance.Lives.ToString(),
                    new Font("Arial", 16),
                    new SolidBrush(Color.White),
                    this.nextWaveBtn.PositionAndSize.X,
                    this.speed100Btn.PositionAndSize.Y + this.speed100Btn.PositionAndSize.Height + 25);
        }

        /// <summary>
        /// Called once when selecting a tower.
        /// </summary>
        /// <param name="newSelectedTower">
        /// The tower which was selected.
        /// </param>
        private void OnTowerSelected(Tower newSelectedTower)
        {
            // Set selected tower field
            this.selectedTower = newSelectedTower;

            // Enable buttons
            this.upgradeBtn.IsVisible = true;
            this.destroyBtn.IsVisible = true;
        }

        /// <summary>
        /// Called once when deselecting a tower.
        /// </summary>
        private void OnTowerDeselected()
        {
            // Remove current selection
            this.selectedTower = null;

            // Enable buttons
            this.upgradeBtn.IsVisible = false;
            this.destroyBtn.IsVisible = false;
        }

        private void SetUpInGameGui()
        {
            // Load button images
            Image nextWaveBtnStandard = Image.FromFile(@"Images/NewWaveBtn.png");
            Image speed100BtnStandard = Image.FromFile(@"Images/Speed100Btn.png");
            Image speed200BtnStandard = Image.FromFile(@"Images/Speed200Btn.png");
            Image speed400BtnStandard = Image.FromFile(@"Images/Speed400Btn.png");
            Image upgradeBtnStandard  = Image.FromFile(@"Images/UpgradeBtn.png");
            Image destroyBtnStandard  = Image.FromFile(@"Images/DestroyBtn.png");
            Image towerOneBtnStandard = Image.FromFile(@"Images/TowerOneBtn.png");
            Image towerTwoBtnStandard = Image.FromFile(@"Images/TowerTwoBtn.png");
            Image towerThreeBtnStandard = Image.FromFile(@"Images/TowerThreeBtn.png");
            Image towerFourBtnStandard = Image.FromFile(@"Images/TowerFourBtn.png");

            // Set up next wave button
            this.nextWaveBtn = new GuiButton(
                positionAndSize: new Rectangle((TileEngine.TilesX * TileEngine.TilesOnSpriteSize) + GuiLeftOffset, GuiLeftOffset, nextWaveBtnStandard.Width, nextWaveBtnStandard.Height),
                standardImage: nextWaveBtnStandard,
                hoverImage: Image.FromFile(@"Images/NewWaveBtn_Hovered.png"),
                pressedImage: Image.FromFile(@"Images/NewWaveBtn_Pressed.png"),
                clickAction: WaveManager.Instance.StartWave);

            // Set up speed 100% button
            this.speed100Btn = new GuiButton(
                positionAndSize: new Rectangle((TileEngine.TilesX * TileEngine.TilesOnSpriteSize) + GuiLeftOffset, SpeedBtnTopOffset, speed100BtnStandard.Width, speed100BtnStandard.Height),
                standardImage: speed100BtnStandard,
                hoverImage: Image.FromFile(@"Images/Speed100Btn_Hovered.png"),
                pressedImage: Image.FromFile(@"Images/Speed100Btn_Pressed.png"),
                clickAction: () => Time.TimeScale = 1);

            // Set up speed 200% button
            this.speed200Btn = new GuiButton(
                positionAndSize: new Rectangle((TileEngine.TilesX * TileEngine.TilesOnSpriteSize) + GuiLeftOffset + SpaceBetweenSpeedButtons, SpeedBtnTopOffset, speed200BtnStandard.Width, speed200BtnStandard.Height),
                standardImage: speed200BtnStandard,
                hoverImage: Image.FromFile(@"Images/Speed200Btn_Hovered.png"),
                pressedImage: Image.FromFile(@"Images/Speed200Btn_Pressed.png"),
                clickAction: () => Time.TimeScale = 2.5f);

            // Set up speed 400% button
            this.speed400Btn = new GuiButton(
                positionAndSize: new Rectangle((TileEngine.TilesX * TileEngine.TilesOnSpriteSize) + GuiLeftOffset + (SpaceBetweenSpeedButtons * 2), SpeedBtnTopOffset, speed400BtnStandard.Width, speed400BtnStandard.Height),
                standardImage: speed400BtnStandard,
                hoverImage: Image.FromFile(@"Images/Speed400Btn_Hovered.png"),
                pressedImage: Image.FromFile(@"Images/Speed400Btn_Pressed.png"),
                clickAction: () => Time.TimeScale = 5);

            // Set up upgrade button
            this.upgradeBtn = new GuiButton(
                positionAndSize: new Rectangle((TileEngine.TilesX * TileEngine.TilesOnSpriteSize) + GuiLeftOffset, UpgradeButtonTopOffset, upgradeBtnStandard.Width, upgradeBtnStandard.Height),
                standardImage: upgradeBtnStandard,
                hoverImage: Image.FromFile(@"Images/UpgradeBtn_Hovered.png"),
                pressedImage: Image.FromFile(@"Images/UpgradeBtn_Pressed.png"),
                clickAction: this.UpgradeSelectedTower);

            // Set up destroy button
            this.destroyBtn = new GuiButton(
                positionAndSize: new Rectangle((TileEngine.TilesX * TileEngine.TilesOnSpriteSize) + GuiLeftOffset, this.upgradeBtn.PositionAndSize.Y + this.upgradeBtn.PositionAndSize.Height + 2, upgradeBtnStandard.Width, upgradeBtnStandard.Height),
                standardImage: destroyBtnStandard,
                hoverImage: Image.FromFile(@"Images/DestroyBtn_Hovered.png"),
                pressedImage: Image.FromFile(@"Images/DestroyBtn_Pressed.png"),
                clickAction: this.DestroySelectedTower);

            // Set up tower one button
            this.towerOneBtn = new GuiButton(
                positionAndSize: new Rectangle((TileEngine.TilesX * TileEngine.TilesOnSpriteSize) + GuiLeftOffset, TowerButtonsTopOffset, towerOneBtnStandard.Width, towerOneBtnStandard.Height),
                standardImage: towerOneBtnStandard,
                hoverImage: Image.FromFile(@"Images/TowerOneBtn_Hovered.png"),
                pressedImage: Image.FromFile(@"Images/TowerOneBtn_Pressed.png"),
                clickAction: () => this.SelectTowerToBuild(0));

            // Set up tower twp button
            this.towerTwoBtn = new GuiButton(
                positionAndSize: new Rectangle((TileEngine.TilesX * TileEngine.TilesOnSpriteSize) + GuiLeftOffset + this.towerOneBtn.PositionAndSize.Width + TowerButtonsSpacing, TowerButtonsTopOffset, towerTwoBtnStandard.Width, towerTwoBtnStandard.Height),
                standardImage: towerTwoBtnStandard,
                hoverImage: Image.FromFile(@"Images/TowerTwoBtn_Hovered.png"),
                pressedImage: Image.FromFile(@"Images/TowerTwoBtn_Pressed.png"),
                clickAction: () => this.SelectTowerToBuild(1));

            // Set up tower three button
            this.towerThreeBtn = new GuiButton(
                positionAndSize: new Rectangle((TileEngine.TilesX * TileEngine.TilesOnSpriteSize) + GuiLeftOffset + ((this.towerOneBtn.PositionAndSize.Width + TowerButtonsSpacing) * 2), TowerButtonsTopOffset, towerThreeBtnStandard.Width, towerThreeBtnStandard.Height),
                standardImage: towerThreeBtnStandard,
                hoverImage: Image.FromFile(@"Images/TowerThreeBtn_Hovered.png"),
                pressedImage: Image.FromFile(@"Images/TowerThreeBtn_Pressed.png"),
                clickAction: () => this.SelectTowerToBuild(2));

            // Set up tower four button
            this.towerFourBtn = new GuiButton(
                positionAndSize: new Rectangle((TileEngine.TilesX * TileEngine.TilesOnSpriteSize) + GuiLeftOffset + ((this.towerOneBtn.PositionAndSize.Width + TowerButtonsSpacing) * 3), TowerButtonsTopOffset, towerFourBtnStandard.Width, towerFourBtnStandard.Height),
                standardImage: towerFourBtnStandard,
                hoverImage: Image.FromFile(@"Images/TowerFourBtn_Hovered.png"),
                pressedImage: Image.FromFile(@"Images/TowerFourBtn_Pressed.png"),
                clickAction: () => this.SelectTowerToBuild(3));
        }

        private void DrawTowerButtonSelection(Graphics graphics)
        {
            // Return if no tower is selected
            if (this.SelectedTowerToBuild == -1)
            {
                return;
            }

            Pen pen = new Pen(Color.Yellow, 4);

            int posY = TowerOptionsTopOffset;

            // Get hovered node
            MapNode hoveredNode = InputManager.Instance.HovederedMouseNode;

            string nameAndPriceTxt;
            string damageTxt;
            string rangeTxt;
            string cooldownTxt;
            float range;
            Rectangle selectionRectangle;

            switch (this.SelectedTowerToBuild)
            {
                // Flame Tower
                case 0:
                    nameAndPriceTxt = "Flame Tower  $" + GameSettings.StandardTower_BuildPrice.ToString();
                    damageTxt = "Damage " + GameSettings.StandardTower_AttackDamage.ToString();
                    rangeTxt = "Range " + GameSettings.StandardTower_ShootRange.ToString();
                    cooldownTxt = "Cooldown " + GameSettings.StandardTower_CoolDown.ToString() + " s";
                    range = GameSettings.StandardTower_ShootRange;
                    selectionRectangle = this.towerOneBtn.PositionAndSize;
                    break;

                // Frost Tower
                case 1:
                    nameAndPriceTxt = "Frost Tower  $" + GameSettings.FrostTower_BuildPrice.ToString();
                    damageTxt = "Damage " + GameSettings.FrostTower_AttackDamage.ToString();
                    rangeTxt = "Range " + GameSettings.FrostTower_ShootRange.ToString();
                    cooldownTxt = "Cooldown " + GameSettings.FrostTower_CoolDown.ToString() + " s";
                    range = GameSettings.FrostTower_ShootRange;
                    selectionRectangle = this.towerTwoBtn.PositionAndSize;
                    break;

                // Sniper Tower
                case 2:
                    nameAndPriceTxt = "Sniper Tower  $" + GameSettings.SniperTower_BuildPrice.ToString();
                    damageTxt = "Damage " + GameSettings.SniperTower_AttackDamage.ToString();
                    rangeTxt = "Range " + GameSettings.SniperTower_ShootRange.ToString();
                    cooldownTxt = "Cooldown " + GameSettings.SniperTower_CoolDown.ToString() + " s";
                    range = GameSettings.SniperTower_ShootRange;
                    selectionRectangle = this.towerThreeBtn.PositionAndSize;
                    break;

                // Agility Tower
                default:
                    nameAndPriceTxt = "Agility Tower  $" + GameSettings.AgilityTower_BuildPrice.ToString();
                    damageTxt = "Damage " + GameSettings.AgilityTower_AttackDamage.ToString();
                    rangeTxt = "Range " + GameSettings.AgilityTower_ShootRange.ToString();
                    cooldownTxt = "Cooldown " + GameSettings.AgilityTower_CoolDown.ToString() + " s";
                    range = GameSettings.AgilityTower_ShootRange;
                    selectionRectangle = this.towerFourBtn.PositionAndSize;
                    break;
            }

            graphics.DrawRectangle(pen, selectionRectangle);

            // Write name
            graphics.DrawString(
                nameAndPriceTxt,
                new Font("Arial", 16),
                new SolidBrush(Color.White),
                this.nextWaveBtn.PositionAndSize.X,
                posY);

            // Write damage
            graphics.DrawString(
                damageTxt,
                new Font("Arial", 16),
                new SolidBrush(Color.White),
                this.nextWaveBtn.PositionAndSize.X,
                posY += 20);

            // Write range
            graphics.DrawString(
                rangeTxt,
                new Font("Arial", 16),
                new SolidBrush(Color.White),
                this.nextWaveBtn.PositionAndSize.X,
                posY += 20);

            // Draw circle indicating the towers radius
            if (hoveredNode != null)
            {
                graphics.FillEllipse(
                    new SolidBrush(Color.FromArgb(125, 0, 0, 0)),
                    new RectangleF(
                    hoveredNode.Position.X - range,
                    hoveredNode.Position.Y - range,
                    range * 2,
                    range * 2));
            }

            // Write cooldown
            graphics.DrawString(
                cooldownTxt,
                new Font("Arial", 16),
                new SolidBrush(Color.White),
                this.nextWaveBtn.PositionAndSize.X,
                posY += 20);
        }

        /// <summary>
        /// Calls the selected towers destroy method.
        /// </summary>
        private void DestroySelectedTower()
        {
            if (this.selectedTower != null)
            {
                this.selectedTower.Destroy();
            }
        }

        /// <summary>
        /// Sets the selected tower number.
        /// </summary>
        /// <param name="towerToBuild">
        /// The index number for the tower to build.
        /// </param>
        private void SelectTowerToBuild(int towerToBuild)
        {
            // Return if there is any enemies, we can't build under a wave
            if (GameObject.AllGameObjects.Any(g => g is Enemy))
            {
                return;
            }

            // Deselect a selected tower
            EventManager.Instance.DeselectedTower.SafeInvoke();
            
            if (this.SelectedTowerToBuild == towerToBuild)
            {
                // Deselect if we hit the button twice in a row
                this.SelectedTowerToBuild = -1;
                return;
            }

            this.SelectedTowerToBuild = towerToBuild;
        }

        /// <summary>
        /// Upgrades the selected tower, if you have enough currency.
        /// </summary>
        private void UpgradeSelectedTower()
        {
            if (GameManager.Instance.TryBuy(this.selectedTower.UpgradePrice))
            {
                this.selectedTower.Upgrade();
            }
        }
    }
}
