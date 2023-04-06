using UnityEngine;

namespace JescoDev.Utility.SmoothBrainTween.Plugins.Runtime.SmoothBrainTween {
    public partial class TweenInfo {
        
        private const float BackConstant1 = 1.70158f;
        private const float BackConstant2 = BackConstant1 * 1.525f;
        private const float BackConstant3 = BackConstant1 + 1f;
        
        public static float EaseBackIn(float x) => BackConstant3 * Mathf.Pow(x, 3) - BackConstant1 * Mathf.Pow(x, 2);
        public TweenInfo SetEaseBackIn() => FuncWrapper(EaseBackIn);
        
        public static float EaseBackOut(float x) => 1 + BackConstant3 * Mathf.Pow(x - 1, 3) + BackConstant1 * Mathf.Pow(x - 1, 2);
        public TweenInfo SetEaseBackOut() => FuncWrapper(EaseBackOut);
        
        public static float EaseBackInOut(float x) =>  x < 0.5
            ? (Mathf.Pow(2 * x, 2) * ((BackConstant2 + 1) * 2 * x - BackConstant2)) / 2
            : (Mathf.Pow(2 * x - 2, 2) * ((BackConstant2 + 1) * (x * 2 - 2) + BackConstant2) + 2) / 2;
        public TweenInfo SetEaseBackInOut() => FuncWrapper(EaseBackInOut);
        
    }
}