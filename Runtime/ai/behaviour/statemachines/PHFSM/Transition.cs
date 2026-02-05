using System;

namespace TSLib.AI.Behaviour.StateMachines.PHFSM
{
    public class Transition
    {
        public PHS NextState { get; private set; }

        public Transition() { }
        public Transition(PHS nextState) => SetNextState(nextState);

        public void SetNextState(PHS nextState)
        {
            if (nextState == null) throw new ArgumentNullException(nameof(nextState));

            if (NextState != null) throw new InvalidOperationException(
                "(invalid) NextState is already assinged.");

            NextState = nextState;
        }

        public bool EnterCondition()
        {
            if (NextState == null) throw new ArgumentNullException(nameof(NextState));
            return NextState.EnterCondition;
        }
    }
}
