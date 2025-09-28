using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Bullets
{
    public class BulletPool : MonoBehaviour
    {
        [SerializeField] private Transform _container;
        [SerializeField] private int _capacity;

        protected List<Bullet> Pool = new List<Bullet>();

        protected void Initialize(Bullet prefab)
        {
            for (int i = 0; i < _capacity; i++)
            {
                Bullet spawned = Instantiate(prefab, _container.transform);
                spawned.gameObject.SetActive(false);

                Pool.Add(spawned);
            }
        }

        protected bool TryGetObject(out Bullet result)
        {
            result = Pool.FirstOrDefault(p => p.gameObject.activeSelf == false);

            return result != null;
        }
    }
}