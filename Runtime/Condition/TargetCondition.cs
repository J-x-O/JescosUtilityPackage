using System;
using UnityEngine;

namespace Entities.Condition {
    
    [Serializable]
    public abstract class TargetCondition<T> : ICondition {

        [SerializeField] private T _target;
        
        public bool Evaluate() => Evaluate(_target);

        protected abstract bool Evaluate(T target);
    }
}