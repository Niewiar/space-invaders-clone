using UnityEngine;

namespace Game.GameGlobal
{
    using System.Collections;
    using SM;

    public class Intro : MonoBehaviour, IState
    {
        [SerializeField] private float _introTime = 3f;
        [SerializeField] private RectTransform _titleTransform;

        private float _loadTime;

        public bool Complete { get; private set; }

        public void OnEnter()
        {
            _titleTransform.localScale = Vector3.zero;
            _loadTime = Time.timeSinceLevelLoad;
            Complete = false;
        }

        public void OnExit() { }

        public void OnUpdate()
        {
            if (Complete)
            {
                return;
            }

            float currentTime = Time.timeSinceLevelLoad - _loadTime;
            float percent = Mathf.Clamp01(currentTime / _introTime);
            _titleTransform.localScale = Vector3.one * percent;

            if (percent >= 1)
            {
                Complete = true;
            }
        }
    }
}