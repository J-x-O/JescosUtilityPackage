namespace GameProgramming.Utility.TypeBasedEventSystem {
    
    /// <summary>
    /// Classes which implement this interface can be added as a event handler to a <see cref="TypeBasedEventSystem" />. They therefore define custom
    /// behaviour when a certain effect starts or ends.
    /// </summary>
    public interface ITypeBasedEventHandler<T> {
        
        /// <summary> Gets called when a status effect is started </summary>
        /// <param name="instanceOfType"> the active instance, passed in by the event system </param>
        void HandleNewState(T instanceOfType);

        /// <summary> Gets called when a status effect is ended </summary>
        /// <param name="instanceOfType"> The active instance, passed in by the event system </param>
        void HandleEndState(T instanceOfType);
    }
}