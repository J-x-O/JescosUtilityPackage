using System.Linq;

namespace Plugins.JescosUtilityPackage.Runtime.Tickets {
    
    /// <summary>Simple float ticket system</summary>
    /// <remarks>Written by Philipp, Peter</remarks>
    public class FloatTicketSystem : TicketSystem<FloatTicket, float> {

        public readonly float NeutralValue;

        public FloatTicketSystem(float neutralValue) {
            NeutralValue = neutralValue;
            RefreshValue();
        }

        /// <summary>Calculates if ticket is in list</summary>
        /// <returns>bool</returns>
        protected override float CalculateCurrentValue()
            => NeutralValue + _currentTickets.Sum(t => t.Value);

    }
}