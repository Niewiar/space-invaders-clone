using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Characters
{
    using GameGlobal;

    [RequireComponent(typeof(SpriteRenderer))]
    public class Player : MonoBehaviour
    {
        [SerializeField] private Bullet _bullet;
        [SerializeField] private Sprite _ship;
        [SerializeField] private Sprite _explode;

        private InputAction _moveAction;
        private InputAction _shotAction;

        private SpriteRenderer _spriteRenderer;
        private float _explodeTime;

        private void Start()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();

            var actionsMap = InputSystem.actions.FindActionMap("Game");
            _moveAction = actionsMap.FindAction("Move");
            _shotAction = actionsMap.FindAction("Space");

            GameInstance.Gameplay.OnGameStarted += delegate
            {
                _spriteRenderer.sprite = _ship;
                _explodeTime = 0;
                Time.timeScale = 1;

                transform.position = new Vector3(0, -4, 0);
                gameObject.SetActive(true);
                _shotAction.performed += Shot;
            };

            GameInstance.Gameplay.OnGameEnded += delegate
            {
                _spriteRenderer.sprite = _ship;
                _explodeTime = 0;
                Time.timeScale = 1;

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

            if (_explodeTime > 0) 
            {
                _explodeTime -= Time.unscaledDeltaTime;

                if (_explodeTime <= 0)
                {
                    Vector3 pos = transform.position;
                    pos.x = 0;
                    transform.position = pos;
                    _spriteRenderer.sprite = _ship;
                    Time.timeScale = 1;
                }

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

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Time.timeScale = 0;
            _spriteRenderer.sprite = _explode;
            _explodeTime = 1;
            GameInstance.Gameplay.PlayerLives--;
        }
    }
}

