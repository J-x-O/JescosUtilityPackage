using UnityEngine;

namespace JescoDev.Utility.SmoothBrainTween.Plugins.Runtime.SmoothBrainTween {
    public partial class TweenInfo {

        public float EaseExpoIn(float x) => Fallback0(x, () => Mathf.Pow(2, 10 * x - 10));
        public TweenInfo SetEaseExpoIn() => FuncWrapper(EaseExpoIn);
        
        public float EaseExpoOut(float x) => Fallback1(x, () => 1 - Mathf.Pow(2, -10 * x));
        public TweenInfo SetEaseExpoOut() => FuncWrapper(EaseExpoOut);
        
        public float EaseExpoInOut(float x) => Fallback01(x, () => x < 0.5
            ? Mathf.Pow(2, 20 * x - 10) / 2
            : (2 - Mathf.Pow(2, -20 * x + 10)) / 2);
        public TweenInfo SetEaseExpoInOut() => FuncWrapper(EaseExpoInOut);
        
    }
}