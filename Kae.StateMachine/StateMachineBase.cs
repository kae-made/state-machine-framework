using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Kae.StateMachine
{
    public abstract class StateMachineBase : StateMachine
    {
        protected IList<EventData> receivedEvents;
        protected Thread runningThread;

        public StateMachineBase(int initialState) : base(initialState)
        {
            receivedEvents = new List<EventData>();
            runningThread = new Thread(StateMachineExecution);
        }

        protected abstract void RunEntryAction(int nextState, EventData eventData);

        private void StateMachineExecution(object obj)
        {
            if (logger != null) logger.LogInfo("start state machine execution...");
            while (true)
            {
                lock (receivedEvents)
                {
                    while (receivedEvents.Count <= 0)
                        Monitor.Wait(receivedEvents);
                    if (receivedEvents.Count > 0)
                    {
                        var nextEvent = receivedEvents[0];
                        receivedEvents.RemoveAt(0);
                        Monitor.PulseAll(receivedEvents);
                        lock (currentStateMachineStateLock)
                        {
                            currentStateMachineState = StateMachineState.Intermidiate;
                        }
                        int nextState = (int)stateTransition.GetNextState(currentState, nextEvent.EventNumber);
                        if (nextState >= 0)
                        {
                            RunEntryAction(nextState, nextEvent);
                            currentState = nextState;
                            lock (currentStateMachineStateLock)
                            {
                                currentStateMachineState = StateMachineState.Confirmed;
                            }
                        }
                        else if (nextState == (int)ITransition.Transition.CantHappen)
                        {
                            if (logger != null) logger.LogError("transition is 'cannot happen'");
                            throw new CantHappenTransitionException(this, currentState, nextEvent.EventNumber);
                        }
                    }
                    if (currentStateMachineState == StateMachineState.ToTerminate)
                    {
                        if (logger != null) logger.LogInfo("state machine is stopping...");
                        break;
                    }

                }
            }
        }

        public override Task ReceivedEvent(EventData supplementalData)
        {
            if (logger != null) logger.LogInfo($"pushing received event:{supplementalData.EventNumber}");
            Task t = new Task(() =>
            {
                lock (receivedEvents)
                {
                    receivedEvents.Add(supplementalData);
                    Monitor.PulseAll(receivedEvents);
                }
            });
            return t; 
        }

        public override Task Delete()
        {
            Task t = new Task(() =>
             {
                 lock (receivedEvents)
                 {
                     currentStateMachineState = StateMachineState.ToTerminate;
                     Monitor.PulseAll(receivedEvents);
                     if (logger != null) logger.LogInfo("state machine has been terminated");
                 }
             });
            return t;
        }
    }
}
