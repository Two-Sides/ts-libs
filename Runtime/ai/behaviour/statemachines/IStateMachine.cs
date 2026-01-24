namespace TSLib.AI.Behaviour.StateMachines
{
    public interface IStateMachine
    {
        public void Execute(float deltaTime);

        public void TransitionTo(State newState, bool doEnter = true, bool doExit = true, bool allowSameState = false);

        public void RevertToPrevious(bool doEnter = false, bool doExit = true, bool allowSameState = false);

        public bool IsSameState(State s1, State s2);
    }
}