using Game.GameGlobal;
using System;
using System.Linq;
using UnityEngine;

namespace Game.Characters
{
    using GameGlobal;
    using System.Collections.Generic;
    using System.Net.Sockets;

    public class EnemiesRow : MonoBehaviour
    {
        private EnemieSocked[] _sockeds;
        public bool IsActive => _sockeds.Count(socked => socked.gameObject.activeInHierarchy) > 0;
        public int DestroyedEnemiesAmount => _sockeds.Count(socked => !socked.gameObject.activeInHierarchy);

        public void Setup(LoadedEnemieData p_data)
        {
            _sockeds ??= GetComponentsInChildren<EnemieSocked>();

            foreach (EnemieSocked socked in _sockeds)
            {
                socked.Setup(p_data.Sprite1, p_data.Sprite2);
                socked.OnDestroyed += delegate
                {
                    GameInstance.Gameplay.CurrentPoints += p_data.Points;
                };
                socked.gameObject.SetActive(true);
            }
        }

        public void ShowAllEnemies()
        {
            foreach (EnemieSocked socked in _sockeds)
            {
                socked.gameObject.SetActive(true);
            }
        }

        public void Move(Vector3 p_delta)
        {
            transform.position += p_delta;

            foreach (var socked in _sockeds)
            {
                socked.ChangeSprite();
            }
        }

        public bool IsOnEdge()
        {
            EnemieSocked leftSocked = _sockeds.FirstOrDefault(s => s.gameObject.activeInHierarchy);
            EnemieSocked rightSocked = _sockeds.LastOrDefault(s => s.gameObject.activeInHierarchy);

            return (leftSocked != null && leftSocked.transform.position.x <= -3f) 
                || (rightSocked != null && rightSocked.transform.position.x >= 3f);
        }

        public Dictionary<int, Vector3> GetPossibleFirePositions()
        {
            Dictionary<int, Vector3> result = new Dictionary<int, Vector3>();

            for (int i = 0; i < _sockeds.Length; i++)
            {
                if (!_sockeds[i].gameObject.activeInHierarchy)
                {
                    continue;
                }

                result.Add(i, _sockeds[i].transform.position + Vector3.down / 2f);
            }

            return result;
        }
    }
}
