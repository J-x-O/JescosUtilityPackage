using System;
using UnityEngine;

namespace JescoDev.Utility.SmoothBrainTween.Plugins.Runtime.SmoothBrainTween {
    public partial class TweenInfo {

        /// <summary> The Function defining the easing type </summary>
        public Func<float, float> Easing { get; private set; } = x => x;
        
        //just a collection of math stolen from https://easings.net/#

        public TweenInfo SetEaseLinear() => FuncWrapper(x => x);

        public TweenInfo SetCustomEase(AnimationCurve curve) => FuncWrapper(curve.Evaluate);

        // fallback functions to return a value instead of computing the other one
        private static float Fallback0(float x, Func<float> otherValue) {
            if (Math.Abs(x) < 0.00001f) return 0;
            return otherValue();
        }
        
        private static float Fallback1(float x, Func<float> otherValue) {
            if (Math.Abs(x - 1) < 0.00001f) return 1;
            return otherValue();
        }
        
        private static float Fallback01(float x, Func<float> otherValue) {
            if (Math.Abs(x) < 0.00001f) return 0;
            if (Math.Abs(x - 1) < 0.00001f) return 1;
            return otherValue();
        }

        /// <summary> utility function to set the easing and return the info object for chaining syntax </summary>
        private TweenInfo FuncWrapper(Func<float, float> function) {
            Easing = function;
            return this;
        }
    }
}