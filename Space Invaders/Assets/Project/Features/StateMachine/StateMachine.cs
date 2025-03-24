using System;
using System.Collections.Generic;

namespace Game.SM
{
    public class StateMachine
    {
        private readonly Dictionary<Type, StateNode> _nodes = new();
        private readonly HashSet<ITransition> _anyTransitions = new();

        private StateNode _currentNode;

        public void SetState(IState p_state)
        {
            _currentNode = _nodes[p_state.GetType()];
            _currentNode.State.OnEnter();
        }

        public void Update()
        {
            var transition = GetTransition();

            if (transition != null)
            {
                ChangeState(transition.TargetState);
            }

            _currentNode.State?.OnUpdate();
        }

        public void AddTransition(IState p_from, IState p_to, IPredicate p_condition)
        {
            GetOrAddNode(p_from).AddTransition(GetOrAddNode(p_to).State, p_condition);
        }

        public void AddAnyTransition(IState p_to, IPredicate p_condition)
        {
            _anyTransitions.Add(new Transition(p_to, p_condition));
        }

        private StateNode GetOrAddNode(IState p_state)
        {
            var node = _nodes.GetValueOrDefault(p_state.GetType());

            if (node == null)
            {
                node = new StateNode(p_state);
                _nodes.Add(p_state.GetType(), node);
            }

            return node;
        }

        private void ChangeState(IState p_state)
        {
            if (p_state == _currentNode.State)
            {
                return;
            }

            var prevState = _currentNode.State;
            var nextStateNode = _nodes[p_state.GetType()];

            prevState?.OnExit();
            nextStateNode.State?.OnEnter();
            _currentNode = nextStateNode;
        }

        private ITransition GetTransition()
        {
            foreach (var transition in _anyTransitions)
            {
                if (transition.Condition.Evaluate())
                    return transition;
            }

            foreach (var transition in _currentNode.Transitions)
            {
                if (transition.Condition.Evaluate())
                    return transition;
            }

            return null;
        }

        private class StateNode
        {
            public IState State { get; }
            public HashSet<ITransition> Transitions { get; }

            public StateNode(IState p_state)
            {
                State = p_state;
                Transitions = new();
            }

            public void AddTransition(IState p_to, IPredicate p_condition)
            {
                Transitions.Add(new Transition(p_to, p_condition));
            }
        }
    }
}