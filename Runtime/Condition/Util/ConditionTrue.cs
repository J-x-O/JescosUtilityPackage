using System;
using UnityEngine.Scripting.APIUpdating;

namespace Entities.Condition.Util {
    
    [Serializable]
    [AddTypeMenu("Util/True")]
    [MovedFrom("Entities.ConditionSystem.FreeConditions")]
    public class ConditionTrue : ICondition {
        public bool Evaluate() => true;
    }
}