namespace Enemies
{
    public class SnowmanShooting : Shooting
    {
        private void Start()
        {
            Initialize();
        }

        public override void Shoot()
        {
            ShootWithHomingBullet();
        }

        private void Initialize()
        {
            InitializePool(HomingBullet);
        }
    }
}