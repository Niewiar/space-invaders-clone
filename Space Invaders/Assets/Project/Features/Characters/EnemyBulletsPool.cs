using System.Collections.Generic;
using UnityEngine;

namespace Game.Characters
{
    public class EnemyBulletsPool : MonoBehaviour
    {
        [SerializeField] private Bullet _bulletPrefab;
        private readonly List<Bullet> _createdBullets = new();

        public void ActiveBullet(Vector3 p_pos) 
        { 
            foreach (Bullet bullet in _createdBullets) 
            {
                if (!bullet.IsActive)
                {
                    bullet.Activate(p_pos, -1);
                    return;
                }
            }

            Bullet newBullet = Instantiate(_bulletPrefab, transform);
            _createdBullets.Add(newBullet);
            newBullet.Activate(p_pos, -1);
        }
    }
}
