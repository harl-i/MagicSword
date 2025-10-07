namespace Enemies
{
    public class ScorpionShooting : Shooting
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
            SetShootPointPosition();
        }
    }
}