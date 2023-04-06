using UnityEngine;

namespace JescoDev.Utility.SmoothBrainTween.Plugins.Runtime.SmoothBrainTween {
    public partial class SmoothBrainTween {
        
        public static TweenInfo Rotate(GameObject gameObject, Quaternion targetRotation, float duration) {
            if(gameObject == null) return null ;
            TweenInfo info = new TweenInfo();
            
            Quaternion startRotation = gameObject.transform.rotation;
            
            Coroutine routine = _instance.StartCoroutine(
                GenericTweenRoutineCoroutine(info, duration, null ,
                    progress => ApplyRotation(progress, gameObject, startRotation, targetRotation),
                    progress => ApplyRotation(progress, gameObject, startRotation, targetRotation),
                    customVecValue: f => Quaternion.Lerp(startRotation, targetRotation, f).eulerAngles));
            
            _instance.AddNewTween(info, routine);
            return info;
        }

        private static void ApplyRotation(float progress, GameObject gameObject, Quaternion startScale, Quaternion targetScale) {
            Quaternion currentScale = Quaternion.LerpUnclamped(startScale, targetScale, progress);
            gameObject.transform.rotation = currentScale;
        }
        
        public static TweenInfo RotateLocal(GameObject gameObject, Quaternion targetRotation, float duration) {
            if(gameObject == null) return null ;
            TweenInfo info = new TweenInfo();
            
            Quaternion startRotation = gameObject.transform.localRotation;
            
            Coroutine routine = _instance.StartCoroutine(
                GenericTweenRoutineCoroutine(info, duration, null ,
                    progress => ApplyRotationLocal(progress, gameObject, startRotation, targetRotation),
                    progress => ApplyRotationLocal(progress, gameObject, startRotation, targetRotation),
                    customVecValue: f => Quaternion.Lerp(startRotation, targetRotation, f).eulerAngles));
            
            _instance.AddNewTween(info, routine);
            return info;
        }

        private static void ApplyRotationLocal(float progress, GameObject gameObject, Quaternion startScale, Quaternion targetScale) {
            Quaternion currentScale = Quaternion.LerpUnclamped(startScale, targetScale, progress);
            gameObject.transform.localRotation = currentScale;
        }

    }
}