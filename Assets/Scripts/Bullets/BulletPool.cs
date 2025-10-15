using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Bullets
{
    public class BulletPool : MonoBehaviour
    {
        [SerializeField] private Transform _container;

        private int _capacity = 6;

        private List<Bullet> _pool = new List<Bullet>();

        public void Initialize(Bullet prefab)
        {
            for (int i = 0; i < _capacity; i++)
            {
                Bullet spawned = Instantiate(prefab, _container.transform);
                spawned.gameObject.SetActive(false);

                _pool.Add(spawned);
            }
        }

        public bool TryGetObject(out Bullet result)
        {
            result = _pool.FirstOrDefault(p => p.gameObject.activeSelf == false);

            return result != null;
        }
    }
}
