namespace Enemies
{
    public class TurretShooting : Shooting
    {
        private void Start() => Initialize();

        public override void Shoot()
        {
            ShowTurretComponent.enabled = true;
            ShootWithTowardsBullet(false);
        }

        private void Initialize()
        {
            InitializePool(TowardsBullet);
            ShowTurretComponent.enabled = true;
        }

        private void OnDisable() => ShowTurretComponent.enabled = false;
    }
}
