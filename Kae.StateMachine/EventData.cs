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
        protected string eventName;
        public int EventNumber { get { return eventNumber; } }
        public string EventName { get { return eventName; } }

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

        public EventData(string eventName, int eventNumber)
        {
            this.eventName = eventName;
            this.eventNumber = eventNumber;
            this.fireTiming = DateTime.Now;
        }

        protected DateTime fireTiming;
        public DateTime FireTiming { get; }

        public abstract void Send();

        public abstract IDictionary<string, object> GetSupplementalData();
    }
}
