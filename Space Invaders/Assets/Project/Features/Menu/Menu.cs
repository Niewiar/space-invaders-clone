using UnityEngine;

namespace Game.Menu
{
    using SM;
    using HUD;
    using UnityEngine.InputSystem;
    using System;

    public class Menu : MonoState
    {
        [SerializeField] private GameObject _blinkText;
        [SerializeField] private float _blinkSpeed = 1f;

        private float _blinkTime;
        private InputAction _spaceAction;

        public bool GameStarted { get; private set; }

        public Action OnMenuEtered;

        public override void OnEnter()
        {
            GameStarted = false;

            _blinkText.SetActive(true);
            _blinkTime = 0;

            if (_spaceAction == null)
            {
                var gameInput = InputSystem.actions.FindActionMap("Game");
                _spaceAction = gameInput.FindAction("Space");
            }

            _spaceAction.performed += StartGame;

            OnMenuEtered?.Invoke();
        }

        private void StartGame(InputAction.CallbackContext obj)
        {
            _spaceAction.performed -= StartGame;
            GameStarted = true;
        }

        public override void OnExit()
        {
            _blinkText.SetActive(false);
        }

        public override void OnUpdate()
        {
            _blinkTime += Time.unscaledDeltaTime;

            if (_blinkTime >= _blinkSpeed)
            {
                _blinkText.SetActive(!_blinkText.activeInHierarchy);
                _blinkTime = 0;
            }
        }
    }
}
