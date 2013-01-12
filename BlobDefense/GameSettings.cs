﻿namespace BlobDefense
{
    using System;

    public static class GameSettings
    {

        public const int StartLives = 20;

        public const int InitialCurrencyAmount = 100;

        public static readonly string SaveDataPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);

        #region Enemy Settings

        public const int DefaultEnemyHealthBarWidth = 25;
        public const int EnemyHealthBarHeight = 3;

        public const float Boss_MoveSpeed = 10;
        public const float Boss_StartHealth = 10000;
        public const float Boss_Bounty = 100;
        public const int Boss_HealthBarWidth = 50;

        public const float FastEnemy_MoveSpeed = 65;
        public const float FastEnemy_StartHealth = 40;
        public const float FastEnemy_Bounty = 10;

        public const float PikachuEnemy_MoveSpeed = 18f;
        public const float PikachuEnemy_StartHealth = 80;
        public const float PikachuEnemy_Bounty = 10;
        public const int PikachuEnemy_HealthBarWidth = 35;

        public const float StandardEnemy_MoveSpeed = 25f;
        public const float StandardEnemy_StartHealth = 40;
        public const float StandardEnemy_Bounty = 5;



        #endregion

        #region Tower Settings

        // STANDARD TOWER - Initial values
        public const float StandardTower_CoolDown = 0.8f;
        public const float StandardTower_ShootRange = 70f;
        public const float StandardTower_AttackDamage = 10;
        public const int StandardTower_BuildPrice = 25;
        public const int StandardTower_UpgradePrice = 15;

        // STANDARD TOWER - Upgrade values
        public const float StandardTower_AttackDamage_Upgrade = 1.2f;
        public const float StandardTower_ShootRange_Upgrade = 1.05f;
        public const float StandardTower_ShootCoolDown_Upgrade = 0.9f;
        public const int StandardTower_UpgradePrice_Upgrade = 2;

        // FROST TOWER - Initial values
        public const float FrostTower_CoolDown = 2;
        public const float FrostTower_ShootRange = 40;
        public const float FrostTower_AttackDamage = 2;
        public const float FrostTower_SlowDuration = 2;
        public const float FrostTower_SlowAmount = 0.5f;
        public const int FrostTower_BuildPrice = 50;
        public const int FrostTower_UpgradePrice = 25;

        // FROST TOWER - Upgrade values
        public const float FrostTower_AttackDamage_Upgrade = 1.2f;
        public const float FrostTower_ShootRange_Upgrade = 1.05f;
        public const float FrostTower_ShootCoolDown_Upgrade = 0.9f;
        public const float FrostTower_SlowDuration_Upgrade = 1.5f;
        public const int FrostTower_UpgradePrice_Upgrade = 2;

        // AGILITY TOWER - Initial values
        public const float AgilityTower_CoolDown = 0.2f;
        public const float AgilityTower_ShootRange = 90;
        public const float AgilityTower_AttackDamage = 2;

        public const int AgilityTower_BuildPrice = 40;
        public const int AgilityTower_UpgradePrice = 20;

        // AGILITY TOWER - Upgrade values
        public const float AgilityTower_AttackDamage_Upgrade = 1.2f;
        public const float AgilityTower_ShootRange_Upgrade = 1.05f;
        public const float AgilityTower_ShootCoolDown_Upgrade = 0.9f;
        public const int AgilityTower_UpgradePrice_Upgrade = 2;

        // SNIPER TOWER - Initial values
        public const float SniperTower_CoolDown = 3f;
        public const float SniperTower_ShootRange = 150;
        public const float SniperTower_AttackDamage = 40;
        public const int SniperTower_BuildPrice = 45;
        public const int SniperTower_UpgradePrice = 25;

        // SNIPER TOWER - Upgrade values
        public const float SniperTower_AttackDamage_Upgrade = 1.2f;
        public const float SniperTower_ShootRange_Upgrade = 1.05f;
        public const float SniperTower_ShootCoolDown_Upgrade = 0.9f;
        public const int SniperTower_UpgradePrice_Upgrade = 2;

        #endregion

        // Mutable settings
        public static string PlayerName = "Player Name";
    }
}
