using System;
using System.Collections.Generic;
using System.Linq;
using Plugins.JescosUtilityPackage.Runtime.Tickets.TimeSlow;
using UnityEngine;


namespace Plugins.JescosUtilityPackage.Runtime.Tickets {
    
    public class TimeScaleManager : TicketSystem<TimeScaleTicket, float> {
        
        public static event Action<float> OnTimeScaleChanged;
        
        private static readonly TimeScaleManager Instance = new ();
        
        // Calculates new value based on tickets
        protected override float CalculateCurrentValue() {
            
            // savety check in case on ticket is not removed due to poor coding
            // note that this will only be fixed, once a new ticket is added, but still better than frozen forever
            IEnumerable<TimeScaleTicket> toBeRemoved = _currentTickets.ToList()
                .Where(t => t.RemoveTime > 0f && Time.time >= t.RemoveTime);
            
            foreach (TimeScaleTicket ticket in toBeRemoved) RemoveSlowTicket(ticket);

            // stack slows
            float result = 1;
            foreach (TimeScaleTicket ticket in _currentTickets) {
                result = result * ticket.TimeChangeFactor;
            }
            return result;
        }

        protected override void OnUpdate() {
            Time.timeScale = CurrentValue;
            TryInvoke(OnTimeScaleChanged, CurrentValue);
        }
        
        private static void TryInvoke<T>(Action<T> action, T param) {
            if (action == null) return;

            foreach(Action<T> handler in action.GetInvocationList().Cast<Action<T>>()) {
                try { handler(param); }
                catch (Exception e) { Debug.LogException(e);}
            }
        }

        public static void UpdateSlowTicket(ref TimeScaleTicket timeScaleTicket, float timeScale, float removeTime) {
            if (timeScaleTicket != null) Instance.RemoveTicket(timeScaleTicket); 
            timeScaleTicket= new TimeScaleTicket(timeScale, removeTime);
            Instance.AddTicket(timeScaleTicket);
        }

        /// <summary> Adds a ticket to the Time Slow Manager </summary>
        public static void AddSlowTicket(TimeScaleTicket ticket) => Instance.AddTicket(ticket);
        
        /// <summary> Removes a ticket from the Time Slow Manager </summary>
        public static void RemoveSlowTicket(TimeScaleTicket ticket) => Instance.RemoveTicket(ticket);

    }
}