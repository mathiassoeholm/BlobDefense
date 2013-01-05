﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlobDefense.Gui
{
    using System.Drawing;

    using BlobDefense.Towers;
    using BlobDefense.WaveSpawner;

    using Extensions;

    internal class GuiManager : Singleton<GuiManager>
    {
        public const int RightPanelWidth = 205;
        private const int GuiLeftOffset = 10;
        private const int SpeedBtnTopOffset = 53;
        private const int SpaceBetweenSpeedButtons = 65;
        private const int TowerOptionsTopOffset = 200;
        private const int UpgradeButtonTopOffset = 315;
        private const int TowerButtonsTopOffset = 150;
        private const int TowerButtonsSpacing = 15;

        private GuiButton nextWaveBtn;
        private GuiButton speed100Btn;
        private GuiButton speed200Btn;
        private GuiButton speed400Btn;
        private GuiButton upgradeBtn;
        private GuiButton destroyBtn;
        private GuiButton towerOneBtn;
        private GuiButton towerTwoBtn;

        private Tower selectedTower;

        /// <summary>
        /// Gets or set the id for the tower to build.
        /// When clicking on a tower icon, this gets set to the appropriate value.
        /// -1 Means no tower.
        /// </summary>
        public int SelectedTowerToBuild { get; private set; }

        /// <summary>
        /// Prevents a default instance of the <see cref="GuiManager"/> class from being created.
        /// </summary>
        private GuiManager()
        {
            // Select no tower as a start
            this.SelectedTowerToBuild = -1;
            
            this.SetUpInGameGui();
            EventManager.Instance.TowerWasSelected += this.OnTowerSelected;
            EventManager.Instance.DeselectedTower += () => this.selectedTower = null;

            // Stop building a tower, if a tower was selected or a wave started
            EventManager.Instance.TowerWasSelected += (notUsing) => this.SelectedTowerToBuild = -1;
            EventManager.Instance.WaveStarted += () => this.SelectedTowerToBuild = -1;
        }

        public void DrawInGameGui(Graphics graphics)
        {
            // Draw buttons
            this.nextWaveBtn.Draw(graphics);
            this.speed100Btn.Draw(graphics);
            this.speed200Btn.Draw(graphics);
            this.speed400Btn.Draw(graphics);
            this.towerOneBtn.Draw(graphics);
            this.towerTwoBtn.Draw(graphics);

            // Draw a selction rectangle around the selected type of tower
            this.DrawTowerButtonSelection(graphics);
            
            if (this.selectedTower != null)
            {
                // Draw upgrade button
                this.upgradeBtn.Draw(graphics);

                // Draw destroy button
                this.destroyBtn.Draw(graphics);
                
                // Draw selection rectangle around selected tower
                graphics.DrawRectangle(
                    new Pen(Color.Yellow),
                    new Rectangle(
                        (int)selectedTower.Position.X - (TileEngine.TilesOnSpriteSize / 2),
                        (int)selectedTower.Position.Y - (TileEngine.TilesOnSpriteSize / 2),
                        TileEngine.TilesOnSpriteSize,
                        TileEngine.TilesOnSpriteSize));

                // Draw range circle around the tower
                graphics.DrawEllipse(Pens.Black,
                    new RectangleF(
                        selectedTower.Position.X - (this.selectedTower.ShootRange),
                        selectedTower.Position.Y - (this.selectedTower.ShootRange),
                        this.selectedTower.ShootRange * 2,
                        this.selectedTower.ShootRange * 2));

                int yPos = TowerOptionsTopOffset;

                // Write selected tower
                graphics.DrawString("Selected tower:",
                    new Font("Arial", 16), new SolidBrush(Color.White),
                    this.nextWaveBtn.PositionAndSize.X,
                    yPos);

                // Write level and kills
                graphics.DrawString("Kills " + this.selectedTower.Kills.ToString(),
                    new Font("Arial", 16), new SolidBrush(Color.White),
                    this.nextWaveBtn.PositionAndSize.X,
                    (yPos += 20));

                // Write damage
                graphics.DrawString("Damage " + ((int)this.selectedTower.AttackDamage).ToString(),
                    new Font("Arial", 16), new SolidBrush(Color.White),
                    this.nextWaveBtn.PositionAndSize.X,
                    (yPos += 20));

                // Write range
                graphics.DrawString("Range " + ((int)this.selectedTower.ShootRange).ToString(),
                    new Font("Arial", 16), new SolidBrush(Color.White),
                    this.nextWaveBtn.PositionAndSize.X,
                    (yPos += 20));

                // Write cooldown
                graphics.DrawString("Cooldown " + this.selectedTower.ShootCooldown.ToString() + " s",
                    new Font("Arial", 16), new SolidBrush(Color.White),
                    this.nextWaveBtn.PositionAndSize.X,
                    (yPos += 20));

                // Write upgrade price
                graphics.DrawString("$" + this.selectedTower.UpgradePrice.ToString(),
                    new Font("Arial", 16), new SolidBrush(Color.White),
                    this.upgradeBtn.PositionAndSize.X + this.upgradeBtn.PositionAndSize.Width,
                    this.upgradeBtn.PositionAndSize.Y + 10);
            }
            
            // Draw wave number
            graphics.DrawString("Wave " + WaveManager.Instance.CurrentWave.ToString(), new Font("Arial", 16), new SolidBrush(Color.White), this.nextWaveBtn.PositionAndSize.X + this.nextWaveBtn.PositionAndSize.Width + 10, nextWaveBtn.PositionAndSize.Y);

            // Draw currency amount
            graphics.DrawString("$" + GameManager.Instance.Currency.ToString(),
                    new Font("Arial", 16), new SolidBrush(Color.White),
                    this.nextWaveBtn.PositionAndSize.X,
                    this.speed100Btn.PositionAndSize.Y + this.speed100Btn.PositionAndSize.Height + 5);
        }

        private void OnTowerSelected(Tower selectedTower)
        {
            this.selectedTower = selectedTower;
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
                clickAction: () => Time.TimeScale = 5);

            // Set up speed 400% button
            this.speed400Btn = new GuiButton(
                positionAndSize: new Rectangle((TileEngine.TilesX * TileEngine.TilesOnSpriteSize) + GuiLeftOffset + (SpaceBetweenSpeedButtons * 2), SpeedBtnTopOffset, speed400BtnStandard.Width, speed400BtnStandard.Height),
                standardImage: speed400BtnStandard,
                hoverImage: Image.FromFile(@"Images/Speed400Btn_Hovered.png"),
                pressedImage: Image.FromFile(@"Images/Speed400Btn_Pressed.png"),
                clickAction: () => Time.TimeScale = 10);

            // Set up upgrade button
            this.upgradeBtn = new GuiButton(
                positionAndSize: new Rectangle((TileEngine.TilesX * TileEngine.TilesOnSpriteSize) + GuiLeftOffset, UpgradeButtonTopOffset, upgradeBtnStandard.Width, upgradeBtnStandard.Height),
                standardImage: upgradeBtnStandard,
                hoverImage: Image.FromFile(@"Images/UpgradeBtn_Hovered.png"),
                pressedImage: Image.FromFile(@"Images/UpgradeBtn_Pressed.png"),
                clickAction: UpgradeSelectedTower);

            // Set up destroy button
            this.destroyBtn = new GuiButton(
                positionAndSize: new Rectangle((TileEngine.TilesX * TileEngine.TilesOnSpriteSize) + GuiLeftOffset, this.upgradeBtn.PositionAndSize.Y + this.upgradeBtn.PositionAndSize.Height + 2, upgradeBtnStandard.Width, upgradeBtnStandard.Height),
                standardImage: destroyBtnStandard,
                hoverImage: Image.FromFile(@"Images/DestroyBtn_Hovered.png"),
                pressedImage: Image.FromFile(@"Images/DestroyBtn_Pressed.png"),
                clickAction: () => this.selectedTower.Destroy());

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
        }

        private void DrawTowerButtonSelection(Graphics graphics)
        {
            // Return if no tower is selected
            if (this.SelectedTowerToBuild == -1)
            {
                return;
            }

            Pen pen = new Pen(Color.Yellow, 4);



            int yPos = TowerOptionsTopOffset;

            switch (this.SelectedTowerToBuild)
            {
                case 0:
                    graphics.DrawRectangle(pen, this.towerOneBtn.PositionAndSize);

                    // Write name
                    graphics.DrawString("Flame Tower  $" + GameSettings.StandardTower_BuildPrice.ToString(),
                        new Font("Arial", 16), new SolidBrush(Color.White),
                        this.nextWaveBtn.PositionAndSize.X,
                        yPos);

                    // Write damage
                    graphics.DrawString("Damage " + GameSettings.StandardTower_AttackDamage.ToString(),
                        new Font("Arial", 16), new SolidBrush(Color.White),
                        this.nextWaveBtn.PositionAndSize.X,
                        (yPos += 20));

                    // Write range
                    graphics.DrawString("Range " + GameSettings.StandardTower_ShootRange.ToString(),
                        new Font("Arial", 16), new SolidBrush(Color.White),
                        this.nextWaveBtn.PositionAndSize.X,
                        (yPos += 20));

                    // Write cooldown
                    graphics.DrawString("Cooldown " + GameSettings.StandardTower_CoolDown.ToString() + " s",
                        new Font("Arial", 16), new SolidBrush(Color.White),
                        this.nextWaveBtn.PositionAndSize.X,
                        (yPos += 20));

                    break;
                case 1:
                    graphics.DrawRectangle(pen, this.towerTwoBtn.PositionAndSize);

                    // Write name
                    graphics.DrawString("Frost Tower  $" + GameSettings.FrostTower_BuildPrice.ToString(),
                        new Font("Arial", 16), new SolidBrush(Color.White),
                        this.nextWaveBtn.PositionAndSize.X,
                        yPos);

                    // Write damage
                    graphics.DrawString("Damage " + GameSettings.FrostTower_AttackDamage.ToString(),
                        new Font("Arial", 16), new SolidBrush(Color.White),
                        this.nextWaveBtn.PositionAndSize.X,
                        (yPos += 20));

                    // Write range
                    graphics.DrawString("Range " + GameSettings.FrostTower_ShootRange.ToString(),
                        new Font("Arial", 16), new SolidBrush(Color.White),
                        this.nextWaveBtn.PositionAndSize.X,
                        (yPos += 20));

                    // Write cooldown
                    graphics.DrawString("Cooldown " + GameSettings.FrostTower_CoolDown.ToString() + " s",
                        new Font("Arial", 16), new SolidBrush(Color.White),
                        this.nextWaveBtn.PositionAndSize.X,
                        (yPos += 20));
                    break;
            }
        }

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

        private void UpgradeSelectedTower()
        {
            if (GameManager.Instance.TryBuy(this.selectedTower.UpgradePrice))
            {
                this.selectedTower.Upgrade();
            }
        }
    }
}
