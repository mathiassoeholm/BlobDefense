namespace BlobDefense.Towers
{
    using System.Drawing;

    using BlobDefense.Enemies;

    /// <summary>
    /// Defines a very fast projectile, used by the agility tower.
    /// </summary>
    class SniperProjectile : Projectile
    {
        public SniperProjectile(Enemy enemy, PointF spawnPosition, float attackDamage, Tower towerSource)
            : base(enemy, attackDamage, towerSource, spawnPosition)
        {
            this.SpriteSheetSource = new Rectangle(102, 200, 9, 5);
            this.MoveSpeed = 100;
        }
    }
}
