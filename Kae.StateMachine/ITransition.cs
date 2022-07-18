// Copyright (c) Knowledge & Experience. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
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
