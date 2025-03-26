using System.Collections;
using System.Linq;
using UnityEngine;

namespace Game.Characters
{
    using GameGlobal;
    using System.Collections.Generic;

    public class EnemiesPool : MonoBehaviour
    {
        [SerializeField] private EnemyBulletsPool _enemyBullets;

        private EnemiesRow[] _enemiesRows;
        private readonly List<Vector3> _startPositions = new();

        private void Start()
        {
            _enemiesRows = GetComponentsInChildren<EnemiesRow>();

            for (int i = 0; i < _enemiesRows.Length; i++)
            {
                _enemiesRows[i].Setup(GameInstance.Config.GetEnemiesRowData(i));
                _startPositions.Add(_enemiesRows[i].transform.position);
            }

            GameInstance.Gameplay.OnGameStarted += delegate
            {
                gameObject.SetActive(true);
                ResetPool();
            };

            GameInstance.Gameplay.OnGameEnded += delegate
            {
                StopAllCoroutines();
                gameObject.SetActive(false);
            };

            gameObject.SetActive(false);
        }

        private IEnumerator MoveRowsCorouine()
        {
            bool isOnEdge = false;
            float horizontalDelta = -.2f;

            while (_enemiesRows.Any(r => r.IsActive))
            {
                foreach (var row in _enemiesRows)
                {
                    if (!row.IsActive)
                    {
                        continue;
                    }

                    yield return new WaitForSeconds(CalculateRowSpeed());

                    row.Move(new(horizontalDelta, 0, 0));

                    if (row.IsOnEdge())
                    {
                        isOnEdge = true;
                    }
                }

                if (isOnEdge)
                {
                    foreach (var row in _enemiesRows)
                    {
                        if (!row.IsActive)
                        {
                            continue;
                        }

                        row.Move(new(0, -.5f, 0));

                        if (row.transform.position.y <= -4f)
                        {
                            GameInstance.Gameplay.ForceGameEnd();
                        }
                    }

                    isOnEdge = false;
                    horizontalDelta *= -1;
                    yield return new WaitForSeconds(CalculateRowSpeed());
                }
            }

            ResetPool();
        }

        private void ResetPool()
        {
            for (int i = 0; i < _enemiesRows.Length; i++)
            {
                _enemiesRows[i].transform.position = _startPositions[i];
                _enemiesRows[i].ShowAllEnemies();
            }

            StopAllCoroutines();
            StartCoroutine(MoveRowsCorouine());
            StartCoroutine(Fire());
        }

        private float CalculateRowSpeed()
        {
            int multiplier = GetDestroyedEnemiesAmount() / GameInstance.Config.DestroyedEnemiesAmoutToSpeedup;
            float subtract = multiplier * GameInstance.Config.EnemiesRowSpeedupValue;
            float speed = GameInstance.Config.StartEnemiesRowSpeed - subtract;
            return Mathf.Max(speed, .1f);
        }

        private int GetDestroyedEnemiesAmount()
        {
            int result = 0;

            foreach (var row in _enemiesRows)
            {
                result += row.DestroyedEnemiesAmount;
            }

            return result;
        }

        private IEnumerator Fire()
        {
            while(true)
            {
                yield return new WaitForSeconds(Random.Range(1, 2));

                int fireRandom = Random.Range(0, 4);

                if (fireRandom == 0)
                {
                    continue;
                }

                Dictionary<int, Vector3> possiblePos = new();

                foreach (var row in _enemiesRows)
                {
                    foreach ((int index, Vector3 pos) in row.GetPossibleFirePositions())
                    {
                        if (possiblePos.ContainsKey(index))
                        {
                            continue;
                        }

                        possiblePos.Add(index, pos);
                    }
                }

                int amount = Mathf.Min(possiblePos.Count, fireRandom);

                for (int i = 0; i < amount; i++)
                {
                    int random = Random.Range(0, possiblePos.Count);
                    _enemyBullets.ActiveBullet(possiblePos.ElementAt(random).Value);
                    possiblePos.Remove(possiblePos.ElementAt(random).Key);
                }
            }
        }
    }
}
