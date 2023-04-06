using System;
using UnityEngine;

namespace JescoDev.Utility.SmoothBrainTween.Plugins.Runtime.SmoothBrainTween {
    public partial class TweenInfo {

        public static float EaseSinIn(float x) => 1 - Mathf.Cos((x * Mathf.PI) / 2);
        public TweenInfo SetEaseSinIn() => FuncWrapper(EaseSinIn);
        public static float EaseSinOut(float x) => Mathf.Sin((x * Mathf.PI) / 2);
        public TweenInfo SetEaseSinOut() => FuncWrapper(EaseSinOut);
        public static float EaseSinInOut(float x) => -(Mathf.Cos(Mathf.PI * x) - 1) / 2;
        public TweenInfo SetEaseSinInOut() => FuncWrapper(EaseSinInOut);
        
    }
}