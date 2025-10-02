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
            ShootWithStraightBullet();
        }

        private void Initialize()
        {
            InitializePool(_straightBullet);
            SetShootPointPosition();
        }
    }
}