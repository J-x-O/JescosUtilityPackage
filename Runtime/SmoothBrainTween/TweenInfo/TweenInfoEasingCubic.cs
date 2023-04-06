using UnityEngine;

namespace JescoDev.Utility.SmoothBrainTween.Plugins.Runtime.SmoothBrainTween {
    public partial class TweenInfo {
        
        public static float EaseCubicIn(float x) => Mathf.Pow(x, 3);
        public TweenInfo SetEaseCubicIn() => FuncWrapper(EaseCubicIn);
        
        public static float EaseCubicOut(float x) => 1 - Mathf.Pow(1 - x, 3);
        public TweenInfo SetEaseCubicOut() => FuncWrapper(EaseCubicOut);
        
        public static float EaseCubicInOut(float x) => x < 0.5 
            ? 4 * Mathf.Pow(x, 3)
            : 1 - Mathf.Pow(-2 * x + 2, 3) / 2;
        public TweenInfo SetEaseCubicInOut() => FuncWrapper(EaseCubicInOut);
    }
}