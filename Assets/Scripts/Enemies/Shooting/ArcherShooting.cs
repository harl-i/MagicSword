namespace Enemies
{
    public class ArcherShooting : Shooting
    {
        private void Start() => Initialize();

        public override void Shoot() => ShootWithTowardsBullet(true);

        private void Initialize()
        {
            InitializePool(TowardsBullet);
            SetShootPointPosition();
        }
    }
}
