using UnityEngine;
using TMPro;

namespace Game.HUD
{
    using Game.GameGlobal;

    public class HUD : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _currentScore;
        [SerializeField] private TextMeshProUGUI _bestScore;
        [SerializeField] private TextMeshProUGUI _bestScoreOwner;
        [SerializeField] private TextMeshProUGUI _lives;
        [SerializeField] private GameObject _controls;

        private void Start()
        {
            BaseSetup();
            _controls.SetActive(false);

            GameInstance.Menu.OnMenuEtered += delegate { gameObject.SetActive(true); };
            GameInstance.Gameplay.OnGameStarted += delegate { _controls.SetActive(true); };

            GameInstance.Gameplay.OnGameEnded += delegate 
            {
                _currentScore.SetText("-");
                _lives.SetText("-");
                _controls.SetActive(false); 
            };

            GameInstance.Gameplay.OnPointsChanged += points =>
            {
                _currentScore.text = points.ToString();
            };

            GameInstance.Gameplay.OnLivesChanged += lives =>
            {
                _lives.text = lives.ToString();
            };

            gameObject.SetActive(false);
        }

        private void BaseSetup()
        {
            _currentScore.SetText("-");
            _bestScore.SetText("-"); //todo load record 
            _lives.SetText("-");
        }
    }
}

