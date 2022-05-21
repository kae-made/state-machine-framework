using System;
using System.Collections.Generic;
using System.Text;

namespace Kae.StateMachine
{
    public class CantHappenTransitionException : Exception
    {
        public CantHappenTransitionException(StateMachine stateMachine, int currentState, int eventNumber)
        {
            this.StateMachine = stateMachine;
            this.CurrentState = currentState;
            this.EventNumber = eventNumber;
        }
        public StateMachine StateMachine { get; set; }
        public int CurrentState { get; set; }
        public int EventNumber { get; set; }
    }
}
