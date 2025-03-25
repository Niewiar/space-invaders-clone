using UnityEngine;

namespace Game.Intro
{
    using System.Collections;
    using SM;

    public class Intro : MonoState
    {
        [SerializeField] private PixelObjectsController _titlePartOne;
        [SerializeField] private PixelObjectsController _titlePartTwo;
        [SerializeField] private GameObject _creators;
        [SerializeField] private GameObject _recreator;
        [SerializeField] private GameObject _hud;

        public bool Complete { get; private set; }

        public override void OnEnter()
        {
            _hud.SetActive(false);
            _creators.SetActive(false);
            _recreator.SetActive(false);
            _titlePartOne.Reset();
            _titlePartTwo.Reset();

            Complete = false;

            _titlePartOne.StartSequence(() => 
            {
                _titlePartTwo.StartSequence(() => 
                {
                    StartCoroutine(ShowCreatorsCoroutine());
                });
            });
        }

        public override void OnExit() 
        {
            _titlePartOne.Reset();
            _titlePartTwo.Reset();
            _creators.SetActive(false);
            _recreator.SetActive(false);
            gameObject.SetActive(false);
        }

        private IEnumerator ShowCreatorsCoroutine()
        {
            yield return new WaitForSeconds(1);
            _creators.SetActive(true);
            yield return new WaitForSeconds(1);
            _recreator.SetActive(true);
            yield return new WaitForSeconds(1);
            _hud.SetActive(true);
            yield return new WaitForSeconds(1);
            Complete = true;
        }
    }
}