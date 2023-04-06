using UnityEngine;

namespace JescoDev.Utility.SmoothBrainTween.Plugins.Runtime.SmoothBrainTween {
    public partial class TweenInfo {
        
        public static float EaseQuartIn(float x) => Mathf.Pow(x, 4);
        public TweenInfo SetEaseQuartIn() => FuncWrapper(EaseQuartIn);
        
        public static float EaseQuartOut(float x) => 1 - Mathf.Pow(1 - x, 4);
        public TweenInfo SetEaseQuartOut() => FuncWrapper(EaseQuartOut);
        
        public static float EaseQuartInOut(float x) => x < 0.5
            ? 8 * Mathf.Pow(x, 4)
            : 1 - Mathf.Pow(-2 * x + 2, 4) / 2;
        public TweenInfo SetEaseQuartInOut() => FuncWrapper(EaseQuartInOut);
    }
}