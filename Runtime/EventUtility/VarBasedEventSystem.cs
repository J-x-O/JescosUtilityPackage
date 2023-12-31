using System;
using System.Collections.Generic;

namespace JescoDev.Utility.EventUtility {
    
    /// <summary>The custom event system hooking into each <see cref="StatusEffectManager"/>.</summary>
    public class VarBasedEventSystem<TVar, TSub> : IReadOnlyVarBasedEventSystem<TVar, TSub> {

        private const int EVENT_START = 0, EVENT_END = 1;
        
        // list that keeps track of all events with their corresponding effect classtype
        private readonly Dictionary<TVar, Action<TSub>[]> _typeEventHandlers = new Dictionary<TVar, Action<TSub>[]>();

        /// <summary>Registers a new event action linked to a fitting class type.</summary>
        /// <param name="subscribedClasses">All types extending <see cref="StatusEffectType" /> which will be listened to.</param>
        /// <param name="eventHandler">The custom event handler class.</param>
        public void RegisterHandler(ITypeBasedEventHandler<TSub> eventHandler, params TVar[] subscribedClasses) {
            foreach (TVar subscribedClass in subscribedClasses) {
                _typeEventHandlers.TryAdd(subscribedClass, new Action<TSub>[2]);
                _typeEventHandlers[subscribedClass][EVENT_START] += eventHandler.HandleNewState;
                _typeEventHandlers[subscribedClass][EVENT_END] += eventHandler.HandleEndState;
            }
        }
        
        /// <summary>Registers a new event action linked to a fitting class type.</summary>
        /// <param name="subscribedClasses">All types extending <see cref="StatusEffectType" /> which will be listened to.</param>
        /// <param name="eventHandler">The custom event handler class.</param>
        public void RegisterStartHandler(Action<TSub> eventHandler, params TVar[] subscribedClasses) {
            foreach (TVar subscribedClass in subscribedClasses) {
                _typeEventHandlers.TryAdd(subscribedClass, new Action<TSub>[2]);
                _typeEventHandlers[subscribedClass][EVENT_START] += eventHandler;
            }
        }
        
        /// <summary>Registers a new event action linked to a fitting class type.</summary>
        /// <param name="subscribedClasses">All types extending <see cref="StatusEffectType" /> which will be listened to.</param>
        /// <param name="eventHandler">The custom event handler class.</param>
        public void RegisterStopHandler(Action<TSub> eventHandler, params TVar[] subscribedClasses) {
            foreach (TVar subscribedClass in subscribedClasses) {
                _typeEventHandlers.TryAdd(subscribedClass, new Action<TSub>[2]);
                _typeEventHandlers[subscribedClass][EVENT_END] += eventHandler;
            }
        }
        
        /// <summary>Unregisters an event handler of given status effect types.</summary>
        /// <param name="eventHandler">The event handler which will be unregistered.</param>
        /// <param name="subscribedClasses">The status effect types which the event handler will no longer listen to.</param>
        public void UnregisterHandler(ITypeBasedEventHandler<TSub> eventHandler, params TVar[] subscribedClasses) {
            foreach (TVar subscribedClass in subscribedClasses) {
                if (!_typeEventHandlers.ContainsKey(subscribedClass)) continue;
                _typeEventHandlers[subscribedClass][EVENT_START] -= eventHandler.HandleNewState;
                _typeEventHandlers[subscribedClass][EVENT_END] -= eventHandler.HandleEndState;
            }
        }

        /// <summary>Unregisters an event handler of given status effect types.</summary>
        /// <param name="eventHandler">The event handler which will be unregistered.</param>
        /// <param name="subscribedClasses">The status effect types which the event handler will no longer listen to.</param>
        public void UnregisterStartHandler(Action<TSub> eventHandler, params TVar[] subscribedClasses) {
            foreach (TVar subscribedClass in subscribedClasses) {
                if(!_typeEventHandlers.ContainsKey(subscribedClass)) continue;
                _typeEventHandlers[subscribedClass][EVENT_START] -= eventHandler;
            }
        }
        
        /// <summary>Unregisters an event handler of given status effect types.</summary>
        /// <param name="eventHandler">The event handler which will be unregistered.</param>
        /// <param name="subscribedClasses">The status effect types which the event handler will no longer listen to.</param>
        public void UnregisterStopHandler(Action<TSub> eventHandler, params TVar[] subscribedClasses) {
            foreach (TVar subscribedClass in subscribedClasses) {
                if(!_typeEventHandlers.ContainsKey(subscribedClass)) continue;
                _typeEventHandlers[subscribedClass][EVENT_END] -= eventHandler;
            }
        }

        /// <summary>Notifies all listeners about the start with the given effect`s type.</summary>
        /// <param name="invokedType">Effect that was started.</param>
        /// <param name="eventObject"></param>
        public void InvokeVarBasedEventStart(TVar invokedType, TSub eventObject) {
            if (_typeEventHandlers.TryGetValue(invokedType, out Action<TSub>[] handler))
                handler[EVENT_START]?.Invoke(eventObject);
        }

        /// <summary>Notifies all listeners about the start with the given effect`s type.</summary>
        /// <param name="invokedType">Effect that was started.</param>
        /// <param name="eventObject"></param>
        public void InvokeVarBasedEventEnd(TVar invokedType, TSub eventObject) {
            if (_typeEventHandlers.TryGetValue(invokedType, out Action<TSub>[] handler))
                handler[EVENT_END]?.Invoke(eventObject);
        }
    }

    /// <summary>The custom event system hooking into each <see cref="StatusEffectManager"/>.</summary>
    public class VarBasedEventSystem<TVar> : IReadOnlyVarBasedEventSystem<TVar> {
        

        // list that keeps track of all events with their corresponding effect classtype
        private readonly Dictionary<TVar, Action> _typeEventHandlers = new Dictionary<TVar, Action>();

        /// <summary>Registers a new event action linked to a fitting class type.</summary>
        /// <param name="subscribedClasses">All types extending <see cref="StatusEffectType" /> which will be listened to.</param>
        /// <param name="eventHandler">The custom event handler class.</param>
        public void RegisterHandler(Action eventHandler, params TVar[] subscribedClasses) {
            foreach (TVar subscribedClass in subscribedClasses) {
                _typeEventHandlers.TryAdd(subscribedClass, null);
                _typeEventHandlers[subscribedClass] += eventHandler;
            }
        }

        /// <summary>Unregisters an event handler of given status effect types.</summary>
        /// <param name="eventHandler">The event handler which will be unregistered.</param>
        /// <param name="subscribedClasses">The status effect types which the event handler will no longer listen to.</param>
        public void UnregisterHandler(Action eventHandler, params TVar[] subscribedClasses) {
            foreach (TVar subscribedClass in subscribedClasses) {
                if (!_typeEventHandlers.ContainsKey(subscribedClass)) continue;
                _typeEventHandlers[subscribedClass] -= eventHandler;
            }
        }
        
        /// <summary>Notifies all listeners about the start with the given effect`s type.</summary>
        /// <param name="invokedType">Effect that was started.</param>
        public void InvokeVarBasedEvent(TVar invokedType) {
            if (_typeEventHandlers.TryGetValue(invokedType, out Action handler))
                handler?.Invoke();
        }
    }
}