using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GameProgramming.Utility.TypeBasedEventSystem {
    
    /// <summary>The custom event system hooking into each <see cref="StatusEffectManager"/>.</summary>
    /// <remarks>Written by Max, Paul, Philipp</remarks>
    public class TypeBasedEventSystem<T> {

        private const int EVENT_START = 0, EVENT_END = 1;
        
        // list that keeps track of all events with their corresponding effect classtype
        private readonly Dictionary<Type, Action<T>[]> _typeEventHandlers = new Dictionary<Type, Action<T>[]>();
        
        /// <summary>Registers a new event action linked to a fitting class type.</summary>
        /// <param name="subscribedClasses">All types extending <see cref="StatusEffectType" /> which will be listened to.</param>
        /// <param name="eventHandler">The custom event handler class.</param>
        public void RegisterHandler(ITypeBasedEventHandler<T> eventHandler, params Type[] subscribedClasses) {
            foreach (Type subscribedClass in subscribedClasses) {

                ValidateSubscription(subscribedClass);
                
                // register native event actions
                _typeEventHandlers[subscribedClass][EVENT_START] += eventHandler.HandleNewState;
                _typeEventHandlers[subscribedClass][EVENT_END] += eventHandler.HandleEndState;
            }
        }
        
        /// <summary>Registers a new event action linked to a fitting class type.</summary>
        /// <param name="subscribedClasses">All types extending <see cref="StatusEffectType" /> which will be listened to.</param>
        /// <param name="eventHandler">The custom event handler class.</param>
        public void RegisterStartHandler(Action<T> eventHandler, params Type[] subscribedClasses) {
            foreach (Type subscribedClass in subscribedClasses) {
                
                ValidateSubscription(subscribedClass);
                
                // register native event actions
                _typeEventHandlers[subscribedClass][EVENT_START] += eventHandler;
            }
        }
        
        /// <summary>Registers a new event action linked to a fitting class type.</summary>
        /// <param name="subscribedClasses">All types extending <see cref="StatusEffectType" /> which will be listened to.</param>
        /// <param name="eventHandler">The custom event handler class.</param>
        public void RegisterStopHandler(Action<T> eventHandler, params Type[] subscribedClasses) {
            foreach (Type subscribedClass in subscribedClasses) {

                ValidateSubscription(subscribedClass);
                
                // register native event actions
                _typeEventHandlers[subscribedClass][EVENT_END] += eventHandler;
            }
        }

        private void ValidateSubscription(Type type) {
            // check if valid type was given
            if (!type.IsSubclassOf(typeof(T)))
                throw new ArgumentException($"The event handler type ({type}) must be a subclass of {typeof(T)}.");

            // register new registry if no handler of the type was created yet
            if (!_typeEventHandlers.ContainsKey(type)) _typeEventHandlers.Add(type, new Action<T>[2]);
        }
        
        /// <summary>Unregisters an event handler of given status effect types.</summary>
        /// <param name="eventHandler">The event handler which will be unregistered.</param>
        /// <param name="subscribedClasses">The status effect types which the event handler will no longer listen to.</param>
        public void UnregisterHandler(ITypeBasedEventHandler<T> eventHandler, params Type[] subscribedClasses) {
            foreach (Type subscribedClass in subscribedClasses) {

                // if no registry for the given effect type, no Action has to be unregistered
                if(ValidateUnsubscribtion(subscribedClass)) continue;

                // unregister native event actions
                _typeEventHandlers[subscribedClass][EVENT_START] -= eventHandler.HandleNewState;
                _typeEventHandlers[subscribedClass][EVENT_END] -= eventHandler.HandleEndState;
            }
        }
        
        /// <summary>Unregisters an event handler of given status effect types.</summary>
        /// <param name="eventHandler">The event handler which will be unregistered.</param>
        /// <param name="subscribedClasses">The status effect types which the event handler will no longer listen to.</param>
        public void UnregisterStartHandler(Action<T> eventHandler, params Type[] subscribedClasses) {
            foreach (Type subscribedClass in subscribedClasses) {

                // if no registry for the given effect type, no Action has to be unregistered
                if(ValidateUnsubscribtion(subscribedClass)) continue;

                // unregister native event actions
                _typeEventHandlers[subscribedClass][EVENT_START] -= eventHandler;
            }
        }
        
        /// <summary>Unregisters an event handler of given status effect types.</summary>
        /// <param name="eventHandler">The event handler which will be unregistered.</param>
        /// <param name="subscribedClasses">The status effect types which the event handler will no longer listen to.</param>
        public void UnregisterStopHandler(Action<T> eventHandler, params Type[] subscribedClasses) {
            foreach (Type subscribedClass in subscribedClasses) {

                // if no registry for the given effect type, no Action has to be unregistered
                if(ValidateUnsubscribtion(subscribedClass)) continue;

                // unregister native event actions
                _typeEventHandlers[subscribedClass][EVENT_END] -= eventHandler;
            }
        }

        private bool ValidateUnsubscribtion(Type type) {
            // check if valid type was given
            if (!type.IsSubclassOf(typeof(T)))
                throw new ArgumentException("The event handler type must be a specific status effect type.");
            
            return !_typeEventHandlers.ContainsKey(type);
        }

        /// <summary>Notifies all listeners about the start with the given effect`s type.</summary>
        /// <param name="invokedType">Effect that was started.</param>
        public void InvokeTypeBasedEventStart(T invokedType) {
            // do not invoke event again if already running
            //if (statusEffect.Duration > statusEffect.Type.BaseDuration) return;

            if (_typeEventHandlers.ContainsKey(invokedType.GetType()))
                _typeEventHandlers[invokedType.GetType()][EVENT_START]?.Invoke(invokedType);
        }
        
        /// <summary>Notifies all listeners about the start with the given effect`s type.</summary>
        /// <param name="invokedType">Effect that was started.</param>
        public void InvokeTypeBasedEventEnd(T invokedType) {
            // do not invoke event again if already running
            //if (statusEffect.Duration > statusEffect.Type.BaseDuration) return;

            if (_typeEventHandlers.ContainsKey(invokedType.GetType()))
                _typeEventHandlers[invokedType.GetType()][EVENT_END]?.Invoke(invokedType);
        }
    }
}