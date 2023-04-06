
namespace Plugins.JescosUtilityPackage.Runtime.Tickets {
    
    /// <summary>Checks if an item is in the list</summary>
    /// <remarks>Written by Mona, Philipp</remarks>
    public class BoolTicketSystem : TicketSystem<SimpleTicket, bool> {

        /// <summary>Current value of the ticket</summary>
        public bool HasTicket => CurrentValue;

        /// <summary>Calculates if ticket is in list</summary>
        /// <returns>bool</returns>
        protected override bool CalculateCurrentValue() 
            => _currentTickets.Count > 0;

    }
}