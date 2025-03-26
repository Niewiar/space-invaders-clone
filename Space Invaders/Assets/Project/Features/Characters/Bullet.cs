using UnityEngine;

namespace Game.Characters
{
    using GameGlobal;

    public class Bullet : MonoBehaviour
    {
        private int _yDir;

        public bool IsActive => gameObject.activeInHierarchy;

        private void Awake()
        {
            gameObject.SetActive(false);
        }

        public void Activate(Vector3 p_startPos, int p_yDir)
        {
            _yDir = p_yDir;
            transform.position = p_startPos;
            gameObject.SetActive(true);
        }

        private void Update()
        {
            if (!IsActive)
            {
                return;
            }

            transform.position += Vector3.up * _yDir * GameInstance.Config.BulletSpeed * Time.deltaTime;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            gameObject.SetActive(false);
        }
    }
}
