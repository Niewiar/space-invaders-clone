﻿using System;
using UnityEngine;

namespace Game.Gameplay
{
    using SM;
    using GameGlobal;

    public class GameplayController : MonoState
    {
        public event Action OnGameStarted;
        public event Action OnGameEnded;
        public event Action<int> OnPointsChanged;
        public event Action<int> OnLivesChanged;

        public bool GameEnded { get; private set; }

        private int _addedExtraLives;

        private int _currentPoints;
        public int CurrentPoints { 
            get => _currentPoints;
            set
            {
                _currentPoints = value;

                int potentialExtraLives = _currentPoints / GameInstance.Config.PointsToGetExtraLive;
                potentialExtraLives -= _addedExtraLives;

                if (potentialExtraLives > 0)
                {
                    _addedExtraLives += potentialExtraLives;
                    PlayerLives += potentialExtraLives;
                }

                OnPointsChanged?.Invoke(_currentPoints);
            }    
        }

        private int _playerLives;
        public int PlayerLives
        {
            get => _playerLives;
            set
            {
                _playerLives = value;
                OnLivesChanged?.Invoke(_playerLives);

                if (_playerLives == 0)
                {
                    GameEnded = true;
                }
            }
        }

        public override void OnEnter()
        {
            GameEnded = false;
            _addedExtraLives = 0;
            CurrentPoints = 0;
            PlayerLives = GameInstance.Config.PlayerStartLives;
            OnGameStarted?.Invoke();
        }

        public override void OnExit()
        {
            OnGameEnded?.Invoke();
        }

        public void ForceGameEnd()
        {
            GameEnded = true;
        }
    }
}
