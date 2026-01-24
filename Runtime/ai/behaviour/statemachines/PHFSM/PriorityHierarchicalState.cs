using System;
using System.Collections.Generic;
using TSLib.AI.Behaviour.StateMachines.HFSM;

namespace TSLib.AI.Behaviour.StateMachines.PHFSM
{
    public abstract class PriorityHierarchicalState : HierarchicalState
    {
        public abstract int Priority { get; }

        protected List<Transition> PrioritizedTransitions { get; } = new();

        public sealed override void Execute(IStateMachine stateMachine, float deltaTime)
        {
            Update(deltaTime);

            TryTransitionToNextState(stateMachine);
        }

        protected virtual void Update(float deltaTime) { }
        public abstract bool EnterCondition();
        public abstract bool ExitCondition();

        public void Add(Transition transition)
        {
            if (transition == null) return;
            if (transition.NextState == null) return;

            PrioritizedTransitions.Add(transition);
        }

        public void SortTransitions(IComparer<Transition> comparer)
        {
            if (comparer == null) throw new ArgumentNullException(nameof(comparer));

            PrioritizedTransitions.Sort(comparer);
        }

        private void TryTransitionToNextState(IStateMachine hfsm)
        {
            if (!ExitCondition()) return;

            for (int i = 0; i < PrioritizedTransitions.Count; i++)
            {
                var transition = PrioritizedTransitions[i];

                if (!transition.ConditionMet()) continue;

                hfsm.TransitionTo(transition.NextState);

                break;
            }
        }
    }
}
