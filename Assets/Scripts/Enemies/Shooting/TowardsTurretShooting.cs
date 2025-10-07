namespace Enemies
{
    public class TowardsTurretShooting : Shooting
    {
        private void Start()
        {
            Initialize();
        }

        private void OnDisable()
        {
            ShowTurretComponent.enabled = false;
        }

        public override void Shoot()
        {
            StartCoroutine(LookAtPlayerAndShoot());
        }

        private void Initialize()
        {
            InitializePool(TowardsBullet);
            ShowTurretComponent.enabled = true;
        }
    }
}