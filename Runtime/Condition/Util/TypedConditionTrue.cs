using System;
using System.Linq;
using UnityEngine;

namespace Entities.Condition.Util {
    
    [Serializable]
    [AddTypeMenu("Util/True")]
    public class TypedConditionTrue<T> : ITypedCondition<T> {
        public bool Evaluate(T target) => true;
    }
}