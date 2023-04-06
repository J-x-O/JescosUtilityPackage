using UnityEngine;

namespace JescoDev.Utility.SmoothBrainTween.Plugins.Runtime.SmoothBrainTween {
    public partial class TweenInfo {
        
        public static float EaseQuadIn(float x) => Mathf.Pow(x, 2);
        public TweenInfo SetEaseQuadIn() => FuncWrapper(EaseQuadIn);
        public static float EaseQuadOut(float x) => 1 - (1 - x) * (1 - x);
        public TweenInfo SetEaseQuadOut() => FuncWrapper(EaseQuadOut);
        public static float EaseQuadInOut(float x) => x < 0.5 ? 2 * x * x : 1 - Mathf.Pow(-2 * x + 2, 2) / 2;
        public TweenInfo SetEaseQuadInOut() => FuncWrapper(EaseQuadInOut);
        
    }
}