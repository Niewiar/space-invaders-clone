using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Characters
{
    using GameGlobal;

    public class Player : MonoBehaviour
    {
        [SerializeField] private Bullet _bullet;

        private InputAction _moveAction;
        private InputAction _shotAction;

        private void Start()
        {
            var actionsMap = InputSystem.actions.FindActionMap("Game");
            _moveAction = actionsMap.FindAction("Move");
            _shotAction = actionsMap.FindAction("Space");

            GameInstance.Gameplay.OnGameStarted += delegate
            {
                transform.position = new Vector3(0, -4, 0);
                gameObject.SetActive(true);
                _shotAction.performed += Shot;
            };

            GameInstance.Gameplay.OnGameEnded += delegate
            {
                _shotAction.performed -= Shot;
                gameObject.SetActive(false);
                transform.position = new Vector3(0, -4, 0);
            };

            gameObject.SetActive(false);
        }

        private void Update()
        {
            if (!gameObject.activeInHierarchy)
            {
                return;
            }

            float x = transform.position.x + _moveAction.ReadValue<float>() * Time.deltaTime * GameInstance.Config.PlayerSpeed;
            x = Mathf.Clamp(x, -3.5f, 3.5f);
            transform.position = new Vector3(x, -4, 0);
        }

        private void Shot(InputAction.CallbackContext obj)
        {
            if (!gameObject.activeInHierarchy || _bullet.IsActive)
            {
                return;
            }

            _bullet.Activate(transform.position + Vector3.up * .5f, 1);
        }

        private void OnTriggerEnter(Collider other)
        {
            GameInstance.Gameplay.PlayerLives--;
        }
    }
}

