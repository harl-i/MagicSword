namespace Enemies
{
    public class TurretShooting : Shooting
    {
        private void Start()
        {
            Initialize();
        }

        public override void Shoot()
        {
            ShootWithTowardsBullet(false);
        }

        private void Initialize()
        {
            InitializePool(_towardsBullet);
            _showTurretComponent.enabled = true;
        }

        private void OnDisable()
        {
            _showTurretComponent.enabled = false;
        }
    }
}