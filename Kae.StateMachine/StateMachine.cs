﻿// Copyright (c) Knowledge & Experience. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
using Kae.Utility.Logging;
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

        protected Logger logger;

        public abstract Task ReceivedEvent(EventData supplementalData);
        public abstract Task ReceivedSelfEvent(EventData supplementalData);
        public abstract Task Delete();

        public StateMachine(int initialState, Logger logger = null)
        {
            this.currentState = initialState;
            currentStateMachineState = StateMachineState.Confirmed;

            this.logger = logger;
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

        public void ForceUpdateState(int stateNumber)
        {
            lock (currentStateMachineStateLock)
            {
                int nowState = this.currentState;
                this.currentState |= stateNumber;
                if (logger != null) logger.LogWarning($"State is updated force from {nowState} to {stateNumber}");
            }
        }
    }
}
