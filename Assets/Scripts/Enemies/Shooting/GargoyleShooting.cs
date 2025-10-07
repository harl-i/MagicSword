namespace Enemies
{
    public class GargoyleShooting : Shooting
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