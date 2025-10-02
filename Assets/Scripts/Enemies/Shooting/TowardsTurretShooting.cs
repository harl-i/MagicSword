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
            _showTurretComponent.enabled = false;
        }

        public override void Shoot()
        {
            StartCoroutine(LookAtPlayerAndShoot());
        }

        private void Initialize()
        {
            InitializePool(_towardsBullet);
            _showTurretComponent.enabled = true;
        }
    }
}