using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kae.StateMachine
{
    public abstract class StateMachine
    {
        protected int currentState;
        public int CurrentState { get { return currentState; } }

        protected ITransition stateTransition;

        public abstract Task ReceivedEvent(EventData supplementalData);
        public abstract Task Delete();

        public StateMachine(int initialState)
        {
            this.currentState = initialState;
            currentStateMachineState = StateMachineState.Confirmed;
        }

        public enum StateMachineState
        {
            Confirmed,
            Intermidiate,
            ToTerminate
        }

        protected StateMachineState currentStateMachineState;
        protected object currentStateMachineStateLock = new object();
        public StateMachineState CurrentStateMachineState { get { lock (currentStateMachineStateLock) { return currentStateMachineState; } } }
    }
}
