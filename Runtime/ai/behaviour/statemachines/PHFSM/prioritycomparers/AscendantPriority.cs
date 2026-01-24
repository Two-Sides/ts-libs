using System.Collections.Generic;

namespace TSLib.AI.Behaviour.StateMachines.PHFSM.PriorityComparers
{
    public readonly struct AscendantPriority : IComparer<Transition>
    {
        public readonly int Compare(Transition x, Transition y)
        {
            if (x is null) return 1;
            if (y is null) return -1;

            int xp = x.NextState.Priority;
            int yp = y.NextState.Priority;

            return xp.CompareTo(yp);
        }
    }
}
