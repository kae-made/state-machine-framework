// Copyright (c) Knowledge & Experience. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
using Kae.StateMachine;
using System;
using System.Collections.Generic;
using System.Text;

namespace StateMachineSample
{

    internal interface ActiveObject : IDisposable
    {
        public int X { get; set; }
        public double D { get; set; }
        public string S { get; set; }
    }

    internal class ActiveObjectBase : ActiveObject
    {
        protected ActiveObjectStateMachine stateMachine;
        protected int attr_X;
        protected double attr_D;
        protected string attr_S;

        public int X { get { return attr_X; } set { attr_X = value; } }
        public double D { get { return attr_D; } set { attr_D = value; } }
        public string S { get { return attr_S; } set { attr_S = value; } }

        public ActiveObjectBase(int x, double d, string s)
        {
            this.attr_X = x;
            this.attr_D = d;
            this.attr_S = s;

            stateMachine = new ActiveObjectStateMachine(this);
        }

        public void Dispose()
        {
            stateMachine.Delete();
        }
    }

    internal class ActiveObjectEvent1 : EventData
    {
        public ActiveObjectEvent1() : base((int)ActiveObjectStateMachine.EventNumber.Event1)
        {

        }
    }

    internal class ActiveObjectEvent2 : EventData
    {
        public ActiveObjectEvent2(int mode) : base((int)ActiveObjectStateMachine.EventNumber.Event2)
        {
            this.Mode = mode;
        }
        public int Mode { get; set; }
    }

    internal class ActiveObjectEvent3 : EventData
    {
        public ActiveObjectEvent3(int level) : base((int)ActiveObjectStateMachine.EventNumber.Event3)
        {
            this.Level = level;
        }
        public int Level { get; set; }
    }

    internal partial class ActiveObjectStateMachine : StateMachineBase
    {
        public enum State
        {
            Ready = 0,
            Prepared = 1,
            Working = 2
        }
        public enum EventNumber
        {
            Event1 = 0,
            Event2 = 1,
            Event3 = 2
        }

        protected ActiveObject target;

        public ActiveObjectStateMachine(ActiveObject target) : base((int)State.Ready)
        {
            this.target = target;
            this.stateTransition = this;
        }

        protected override void RunEntryAction(int nextState, EventData eventData)
        {
            switch (nextState)
            {
                case (int)State.Prepared:
                    EntryActionReady();
                    break;
                case (int)State.Ready:
                    EntryActionPrepared(((ActiveObjectEvent2)eventData).Mode);
                    break;
                case (int)State.Working:
                    EntryActionWorking(((ActiveObjectEvent3)eventData).Level);
                    break;
                default:
                    break;
            }
        }
    }
    internal partial class ActiveObjectStateMachine : ITransition
    { 
        protected int[,] stateTransitionTable = new int[3, 3] {
            {(int)ActiveObjectStateMachine.State.Prepared, (int)ITransition.Transition.Ignore,(int)ITransition.Transition.Ignore },
            {(int)ITransition.Transition.Ignore,(int)ActiveObjectStateMachine.State.Working,(int)ITransition.Transition.CantHappen },
            {(int)ITransition.Transition.Ignore,(int)ITransition.Transition.Ignore,(int)ActiveObjectStateMachine.State.Ready }
           };

        public int GetNextState(int currentState, int eventNumber)
        {
            return stateTransitionTable[currentState, eventNumber];
        }
    }
    
    internal partial class ActiveObjectStateMachine {

        public void EntryActionReady()
        {

        }

        public void EntryActionPrepared(int mode)
        { 
        }

        public void EntryActionWorking(int level)
        {

        }
    }

}
