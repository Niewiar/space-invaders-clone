using System;
using UnityEngine;

namespace Game.Menu
{
    using SM;
    using GameGlobal;
    using UnityEngine.UI;
    using UnityEngine.InputSystem;

    public class GameEnd : MonoState
    {
        [SerializeField] private GameObject _recordText;

        public event Action OnRecordChanged;

        public bool GameStarted { get; private set; }

        private InputAction _spaceAction;

        private void Start()
        {
            var gameInput = InputSystem.actions.FindActionMap("Game");
            _spaceAction = gameInput.FindAction("Space");

            gameObject.SetActive(false);
        }

        private void StartGame(InputAction.CallbackContext obj)
        {
            _spaceAction.performed -= StartGame;
            GameStarted = true;
        }

        public override void OnEnter()
        {
            GameStarted = false;
            _spaceAction.performed += StartGame;

            _recordText.SetActive(false);

            int lastRecord = PlayerPrefs.GetInt("Record");

            if (lastRecord < GameInstance.Gameplay.CurrentPoints)
            {
                PlayerPrefs.SetInt("Record", GameInstance.Gameplay.CurrentPoints);
                OnRecordChanged?.Invoke();
                _recordText.SetActive(true);
            }

            LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)transform);

            gameObject.SetActive(true);
        }

        public override void OnExit()
        {
            gameObject.SetActive(false);
        }
    }
}
