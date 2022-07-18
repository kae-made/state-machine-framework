// Copyright (c) Knowledge & Experience. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
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
