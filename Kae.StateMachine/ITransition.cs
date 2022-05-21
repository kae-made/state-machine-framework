using System;
using System.Collections.Generic;
using System.Text;

namespace Kae.StateMachine
{
    public interface ITransition
    {
        public enum Transition
        {
            CantHappen =-2,
            Ignore = -1
        }

        int GetNextState(int currentState, int eventNumber);
    }
}
