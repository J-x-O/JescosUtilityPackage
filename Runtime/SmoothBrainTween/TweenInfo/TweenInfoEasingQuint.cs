using UnityEngine;

namespace JescoDev.Utility.SmoothBrainTween.Plugins.Runtime.SmoothBrainTween {
    public partial class TweenInfo {
        
        public static float EaseQuintIn(float x) => Mathf.Pow(x, 5);
        public TweenInfo SetEaseQuintIn() => FuncWrapper(EaseQuintIn);
        
        public static float EaseQuintOut(float x) => 1 - Mathf.Pow(1 - x, 5);
        public TweenInfo SetEaseQuintOut() => FuncWrapper(EaseQuintOut);
        
        public static float EaseQuintInOut(float x) => x < 0.5
            ? 16 * Mathf.Pow(x, 5)
            : 1 - Mathf.Pow(-2 * x + 2, 5) / 2;
        public TweenInfo SetEaseQuintInOut() => FuncWrapper(EaseQuintInOut);
        
    }
}