using UnityEngine;

namespace JescoDev.Utility.SmoothBrainTween.Plugins.Runtime.SmoothBrainTween {
    public partial class TweenInfo {
        
        private const float ElasticConstant1 = (2 * Mathf.PI) / 3;
        private const float ElasticConstant2 = (2 * Mathf.PI) / 4.5f;
        
        public static float EaseElasticIn(float x) => Fallback01(x, () => -Mathf.Pow(2, 10 * x - 10) * Mathf.Sin((x * 10 - 10.75f) * ElasticConstant1));
        public TweenInfo SetEaseElasticIn() => FuncWrapper(EaseElasticIn);
        
        public static float EaseElasticOut(float x) => Fallback01(x, () => Mathf.Pow(2, -10 * x) * Mathf.Sin((x * 10 - 0.75f) * ElasticConstant1) + 1);
        public TweenInfo SetEaseElasticOut() => FuncWrapper(EaseElasticOut);
        
        public static float EaseElasticInOut(float x) => Fallback01(x, () => x < 0.5
            ? -(Mathf.Pow(2, 20 * x - 10) * Mathf.Sin((20 * x - 11.125f) * ElasticConstant2)) / 2
            : (Mathf.Pow(2, -20 * x + 10) * Mathf.Sin((20 * x - 11.125f) * ElasticConstant2)) / 2 + 1);
        public TweenInfo SetEaseElasticInOut() => FuncWrapper(EaseElasticInOut);
        
    }
}