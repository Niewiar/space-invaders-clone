using UnityEngine;

namespace Game.Characters
{
    using GameGlobal;

    public class UFO : MonoBehaviour
    {
        private float _timeToShow;
        [SerializeField] private Vector3 _startPos;
        [SerializeField] private Vector3 _endPos;

        private int _points;

        private void Start()
        {
            GameInstance.Gameplay.OnGameStarted += delegate
            {
                _timeToShow = Random.Range(GameInstance.Config.UfoMinShowTime, GameInstance.Config.UfoMaxShowTime);
                _points = Random.Range(GameInstance.Config.UfoMinPoints, GameInstance.Config.UfoMaxPoints);
                transform.position = _startPos;
                gameObject.SetActive(true);
            };

            GameInstance.Gameplay.OnGameEnded += delegate
            {
                gameObject.SetActive(false);
                transform.position = _startPos;
            };

            gameObject.SetActive(false);
        }

        private void Update()
        {
            if (_timeToShow > 0)
            {
                transform.position = _startPos;
                _timeToShow -= Time.deltaTime;
                return;
            }

            transform.position += Vector3.right * Time.deltaTime * GameInstance.Config.UfoSpeed;

            if (transform.position.x > _endPos.x) 
            {
                _points = Random.Range(GameInstance.Config.UfoMinPoints, GameInstance.Config.UfoMaxPoints);
                _timeToShow = Random.Range(GameInstance.Config.UfoMinShowTime, GameInstance.Config.UfoMaxShowTime);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            GameInstance.Gameplay.CurrentPoints += _points;
            _points = Random.Range(GameInstance.Config.UfoMinPoints, GameInstance.Config.UfoMaxPoints);
            _timeToShow = Random.Range(GameInstance.Config.UfoMinShowTime, GameInstance.Config.UfoMaxShowTime);
            transform.position = _startPos;
        }
    }
}

