using System.Collections;
using System;
using System.Linq;
using UnityEngine;

namespace Game.Characters
{
    using GameGlobal;

    public class EnemiesPool : MonoBehaviour
    {
        private EnemiesRow[] _enemiesRows;

        private void Start()
        {
            _enemiesRows = GetComponentsInChildren<EnemiesRow>();

            for (int i = 0; i < _enemiesRows.Length; i++)
            {
                _enemiesRows[i].Setup(GameInstance.Config.GetEnemiesRowData(i));
            }

            GameInstance.Gameplay.OnGameStarted += delegate
            {
                gameObject.SetActive(true);
                StartCoroutine(MoveRowsCorouine());
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

                    row.Move(new(horizontalDelta, 0, 0));

                    if (row.IsOnEdge())
                    {
                        isOnEdge = true;
                    }

                    yield return new WaitForSeconds(CalculateRowSpeed());
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

                        if (row.transform.position.y <= -3.5f)
                        {
                            GameInstance.Gameplay.ForceGameEnd();
                        }
                    }

                    isOnEdge = false;
                    horizontalDelta *= -1;
                    yield return new WaitForSeconds(CalculateRowSpeed());
                }
            }
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
    }
}
