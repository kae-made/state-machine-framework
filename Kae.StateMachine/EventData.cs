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
        }
    }
}
