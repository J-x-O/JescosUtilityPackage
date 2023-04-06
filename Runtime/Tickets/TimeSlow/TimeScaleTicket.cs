using Time = UnityEngine.Time;

namespace Plugins.JescosUtilityPackage.Runtime.Tickets.TimeSlow {
    public class TimeScaleTicket {
        
        public readonly float TimeChangeFactor;
        public readonly float RemoveTime;
        
        public TimeScaleTicket(float timeChangeFactor, float removeTime) {
            TimeChangeFactor = timeChangeFactor;
            RemoveTime = Time.time + removeTime;
        }

    }
}