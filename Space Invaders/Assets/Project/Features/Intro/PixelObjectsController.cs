using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Intro
{
    public class PixelObjectsController : MonoBehaviour
    {
        [SerializeField] private float _sequenceTime = 3f;
        [SerializeField] private List<GameObject> _objectsToOff;

        internal Action OnSequenceComplete;

        internal void Reset()
        {
            OnSequenceComplete = null;

            foreach (GameObject obj in _objectsToOff)
            {
                obj.SetActive(true);
            }

            gameObject.SetActive(false);
        }

        internal void StartSequence(Action p_onEndAction)
        {
            Reset();
            gameObject.SetActive(true);
            OnSequenceComplete = p_onEndAction;
            StartCoroutine(SequenceCoroutine(_sequenceTime / _objectsToOff.Count));
        }

        private IEnumerator SequenceCoroutine(float p_waitTime)
        {
            List<GameObject> objects = new List<GameObject>(_objectsToOff);

            while(objects.Count > 0)
            {
                int random = UnityEngine.Random.Range(0, objects.Count);
                objects[random].SetActive(false);
                objects.RemoveAt(random);
                yield return new WaitForSeconds(p_waitTime);
            }

            OnSequenceComplete?.Invoke();
        }
    }
}

