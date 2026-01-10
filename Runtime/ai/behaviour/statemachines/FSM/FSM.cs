using System;
using System.Collections.Generic;

namespace TwoSides.AI.Behaviour.StateMachines.PFSM
{

    public class FSM<TEntity> : IStateMachine<TEntity>
    {
        public IEqualityComparer<State<TEntity>> StateComparer { get; private set; }

        public TEntity Owner { get; private set; }

        public State<TEntity> CurrentState { get; private set; }

        public State<TEntity> PreviousState { get; private set; }

        public FSM(
            TEntity owner,
            State<TEntity> currentState,
            State<TEntity> previousState = null,
            IEqualityComparer<State<TEntity>> stateComparer = null
            )
        {
            Owner = owner ?? throw new ArgumentNullException(nameof(owner));
            CurrentState = currentState ?? throw new ArgumentNullException(nameof(currentState));
            PreviousState = previousState ?? currentState;
            StateComparer = stateComparer ?? EqualityComparer<State<TEntity>>.Default;

            // Enters the initial state immediately upon creation.
            CurrentState.Enter(this, Owner);
        }

        public void Update()
        {
            CurrentState.Execute(this, Owner);
        }

        public void ChangeState(State<TEntity> newState, bool allowSameState = false)
        {
            if (newState == null)
                throw new ArgumentNullException(nameof(newState));

            if (!allowSameState && IsSameState(CurrentState, newState))
                return;

            PreviousState = CurrentState;
            CurrentState.Exit(this, Owner);
            CurrentState = newState;
            CurrentState.Enter(this, Owner);
        }

        public void RevertToPreviousState(bool allowSameState = false)
        {
            ChangeState(PreviousState, allowSameState);
        }

        public void ChangeOwner(TEntity newOwner, bool reenterCurrentState)
        {
            if (newOwner == null)
                throw new ArgumentNullException(nameof(newOwner));

            var oldOwner = Owner;
            Owner = newOwner;

            if (reenterCurrentState)
            {
                CurrentState.Exit(this, oldOwner);
                CurrentState.Enter(this, Owner);
            }
        }

        public bool IsSameState(State<TEntity> s1, State<TEntity> s2)
        {
            return StateComparer.Equals(s1, s2);
        }
    }
}

