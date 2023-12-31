using System;

namespace JescoDev.Utility.EventUtility {
    public interface IReadOnlyVarBasedEventSystem<TVar, TSub> {

        public void RegisterHandler(ITypeBasedEventHandler<TSub> eventHandler, params TVar[] subscribedClasses);
        public void RegisterStartHandler(Action<TSub> eventHandler, params TVar[] subscribedClasses);
        public void RegisterStopHandler(Action<TSub> eventHandler, params TVar[] subscribedClasses);

        public void UnregisterHandler(ITypeBasedEventHandler<TSub> eventHandler, params TVar[] subscribedClasses);
        public void UnregisterStartHandler(Action<TSub> eventHandler, params TVar[] subscribedClasses);
        public void UnregisterStopHandler(Action<TSub> eventHandler, params TVar[] subscribedClasses);

    }
    
    public interface IReadOnlyVarBasedEventSystem<TVar> {

        public void RegisterHandler(Action eventHandler, params TVar[] subscribedClasses);
        public void UnregisterHandler(Action eventHandler, params TVar[] subscribedClasses);

    }
}