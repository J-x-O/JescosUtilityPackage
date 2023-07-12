using System;
using GameProgramming.Utility.TypeBasedEventSystem;

namespace JescoDev.Utility.EventUtility.EventUtility {
    public interface IReadOnlyVarBasedEventSystem<TVar, TSub> {

        public void RegisterHandler(ITypeBasedEventHandler<TSub> eventHandler, params TVar[] subscribedClasses);
        public void RegisterStartHandler(Action<TSub> eventHandler, params TVar[] subscribedClasses);
        public void RegisterStopHandler(Action<TSub> eventHandler, params TVar[] subscribedClasses);

        public void UnregisterHandler(ITypeBasedEventHandler<TSub> eventHandler, params TVar[] subscribedClasses);
        public void UnregisterStartHandler(Action<TSub> eventHandler, params TVar[] subscribedClasses);
        public void UnregisterStopHandler(Action<TSub> eventHandler, params TVar[] subscribedClasses);

    }
}