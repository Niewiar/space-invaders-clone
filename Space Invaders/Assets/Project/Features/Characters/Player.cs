using UnityEngine;

namespace Game.Characters
{
    using GameGlobal;
    using System;
    using System.Collections;
    using UnityEngine.InputSystem;

    public class Player : Character
    {
        [SerializeField] private float _speed = 3;
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

        private void Shot(InputAction.CallbackContext obj)
        {
            if (!gameObject.activeInHierarchy || _bullet.IsActive)
            {
                return;
            }

            _bullet.Activate(transform.position + Vector3.up * .5f, 1);
        }

        private void Update()
        {
            if (!gameObject.activeInHierarchy)
            {
                return;
            }

            float x = transform.position.x + _moveAction.ReadValue<float>() * Time.deltaTime * _speed;
            x = Mathf.Clamp(x, -3.5f, 3.5f);
            transform.position = new Vector3(x, -4, 0);
        }
    }
}

