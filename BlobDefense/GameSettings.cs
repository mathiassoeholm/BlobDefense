namespace BlobDefense
{
    public static class GameSettings
    {

        public const int StartLives = 20;

        // Mutable settings
        public static string PlayerName;

        #region Tower Settings

        // STANDARD TOWER - Initial values
        public const float StandardTower_CoolDown = 0.8f;
        public const float StandardTower_ShootRange = 70f;
        public const float StandardTower_AttackDamage = 10;
        public const int StandardTower_BuildPrice = 25;
        public const int StandardTower_UpgradePrice = 15;

        // STANDARD TOWER - Upgrade values
        public const float StandardTower_AttackDamage_Upgrade = 1.2f;
        public const float StandardTower_ShootRange_Upgrade = 1.2f;
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
        public const float FrostTower_ShootRange_Upgrade = 1.2f;
        public const float FrostTower_ShootCoolDown_Upgrade = 0.9f;
        public const float FrostTower_SlowDuration_Upgrade = 1.5f;
        public const int FrostTower_UpgradePrice_Upgrade = 2;

        #endregion

    }
}
