namespace Enemies
{
    public class SpiderShooting : Shooting
    {
        private void Start()
        {
            Initialize();
        }

        public override void Shoot()
        {
            SetShootPointPosition();
            ShootWithStraightBullet();
        }

        private void Initialize()
        {
            InitializePool(StraightBullet);
            SetShootPointPosition();
        }
    }
}