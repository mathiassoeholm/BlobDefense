using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlobDefense.Towers
{
    using System.Drawing;

    using BlobDefense.Enemies;

    internal class FrostProjectile : Projectile
    {
        private readonly float slowDuration;

        private readonly float slowAmount;

        public FrostProjectile(Enemy enemy, PointF spawnPosition, float attackDamage, float slowDuration, float slowAmount, Tower towerSource)
            : base(enemy, attackDamage, towerSource)
        {
            this.slowDuration = slowDuration;
            this.slowAmount = slowAmount;
            this.SpriteSheetSource = new Rectangle(100, 187, 13, 6);
            this.Position = spawnPosition;
            this.MoveSpeed = 200;
        }

        protected override void OnTargetHit()
        {
            // Slow the enemy
            this.EnemyTarget.Slow(this.slowAmount, this.slowDuration);
            
            base.OnTargetHit();
        }
    }
}
