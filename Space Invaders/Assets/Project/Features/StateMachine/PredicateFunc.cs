using System;

namespace Game.SM
{
    public class PredicateFunc : IPredicate
    {
        private readonly Func<bool> _func;

        public PredicateFunc(Func<bool> p_func)
        {
            _func = p_func;
        }

        public bool Evaluate() => _func.Invoke();
    }
}