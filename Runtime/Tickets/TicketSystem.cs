using System;
using System.Collections.Generic;

namespace Plugins.JescosUtilityPackage.Runtime.Tickets {
    
    /// <summary>add and remove tickets from the ticket system</summary>
    /// <typeparam name="TTicket">the ticket</typeparam>
    /// <typeparam name="TValue">ticket value</typeparam>
    /// <remarks>Written by Mona, Philipp</remarks>
    
    public abstract class TicketSystem<TTicket, TValue> {

        /// <summary> List of current tickets </summary>
        protected readonly List<TTicket> _currentTickets = new List<TTicket>();

        /// <summary> Current value of all tickets </summary>
        public TValue CurrentValue { get; protected set; }

        /// <summary> On change gets called when a ticket is added or removed </summary>
        public event Action<TValue> OnChange;

        /// <summary> Method Called when the Value is Updated, if you want to do additional stuff </summary>
        protected virtual void OnUpdate() { }

        protected TicketSystem() {
            
        }

        /// <summary> Adds a ticket </summary>
        /// <param name="ticket"> The ticket that will be added</param>
        public void AddTicket(TTicket ticket) {
            _currentTickets.Add(ticket);
            RefreshValue();
            OnAddTicket(ticket);
        }

        /// <summary> Removes a ticket </summary>
        /// <param name="ticket"> The ticket that will be removed </param>
        public void RemoveTicket(TTicket ticket) {
            if (ticket == null) return;
            _currentTickets.Remove(ticket);
            RefreshValue();
        }

        protected void RefreshValue() {
            CurrentValue = CalculateCurrentValue();
            OnUpdate();
            OnChange?.Invoke(CurrentValue);
        }

        /// <summary> An Internal Event when a ticket is added </summary>
        /// <param name="ticket"> the added ticket </param>
        protected virtual void OnAddTicket(TTicket ticket) { }
        
        /// <summary> Abstract method to calculate the combined Value of all Tickets </summary>
        /// <returns> The calculated Value </returns>
        protected abstract TValue CalculateCurrentValue();
        
    }
}