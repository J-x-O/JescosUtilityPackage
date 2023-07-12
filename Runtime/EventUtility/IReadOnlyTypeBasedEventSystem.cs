using System;
using GameProgramming.Utility.TypeBasedEventSystem;

namespace JescoDev.Utility.EventUtility.EventUtility {
    public interface IReadOnlyTypeBasedEventSystem<T> {

        public void RegisterHandler(ITypeBasedEventHandler<T> eventHandler, params Type[] subscribedClasses);
        public void RegisterStartHandler(Action<T> eventHandler, params Type[] subscribedClasses);
        public void RegisterStopHandler(Action<T> eventHandler, params Type[] subscribedClasses);

        public void UnregisterHandler(ITypeBasedEventHandler<T> eventHandler, params Type[] subscribedClasses);
        public void UnregisterStartHandler(Action<T> eventHandler, params Type[] subscribedClasses);
        public void UnregisterStopHandler(Action<T> eventHandler, params Type[] subscribedClasses);

    }
}