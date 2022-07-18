// Copyright (c) Knowledge & Experience. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
using System;
using System.Collections.Generic;
using System.Text;

namespace Kae.StateMachine
{
    public abstract class EventData
    {
        protected int eventNumber;
        public int EventNumber { get { return eventNumber; } }

        public EventData(int eventNumber)
        {
            this.eventNumber = eventNumber;
            this.fireTiming = DateTime.Now;
        }

        public EventData(int eventNumber, DateTime fireTiming)
        {
            this.eventNumber = eventNumber;
            this.fireTiming = fireTiming;
        }

        protected DateTime fireTiming;
        public DateTime FireTiming { get; }
    }
}
