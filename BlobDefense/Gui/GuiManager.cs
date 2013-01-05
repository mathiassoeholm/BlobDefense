using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlobDefense.Gui
{
    using System.Drawing;

    using BlobDefense.Towers;
    using BlobDefense.WaveSpawner;

    internal class GuiManager : Singleton<GuiManager>
    {
        public const int RightPanelWidth = 205;
        private const int GuiLeftOffset = 10;
        private const int SpeedBtnTopOffset = 53;
        private const int SpaceBetweenSpeedButtons = 65;
        private const int TowerOptionsTopOffset = 290;
        private const int UpgradeButtonTopOffset = 395;

        private GuiButton nextWaveBtn;
        private GuiButton speed100Btn;
        private GuiButton speed200Btn;
        private GuiButton speed400Btn;
        private GuiButton upgradeBtn;
        private GuiButton destroyBtn;


        private Tower selectedTower;

        /// <summary>
        /// Prevents a default instance of the <see cref="GuiManager"/> class from being created.
        /// </summary>
        private GuiManager()
        {
            this.SetUpInGameGui();
            EventManager.Instance.TowerWasSelected += this.OnTowerSelected;
            EventManager.Instance.DeselectedTower += () => this.selectedTower = null;
        }

        public void DrawInGameGui(Graphics graphics)
        {
            // Draw buttons
            this.nextWaveBtn.Draw(graphics);
            this.speed100Btn.Draw(graphics);
            this.speed200Btn.Draw(graphics);
            this.speed400Btn.Draw(graphics);
            
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
