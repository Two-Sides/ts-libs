using System;

namespace TSLib.AI.Behaviour.StateMachines.PHFSM
{
    public class Transition
    {
        public PriorityHierarchicalState NextState { get; }

        public Transition(PriorityHierarchicalState nextState)
        {
            NextState = nextState ?? throw new ArgumentNullException(nameof(nextState));
        }

        public bool ConditionMet() => NextState.EnterCondition();
    }
}
