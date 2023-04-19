using System;
using UnityEngine.Scripting.APIUpdating;

namespace Entities.Condition.Util {
    
    [Serializable]
    [AddTypeMenu("Util/False")]
    [MovedFrom("Entities.ConditionSystem.FreeConditions")]
    public class ConditionFalse : ICondition {
        public bool Evaluate() => false;
    }
}