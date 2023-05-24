using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Beta.Audio;
using UnityEngine;
using UnityEngine.Audio;

namespace Plugins.Audio.AudioManagers {

    /// <summary> A Token created when an effect is started, identifying the playing sound </summary>
    public class SoundToken {
        public AudioEvent Event { get; }
        public AudioSource Source { get; }
        public float Delay { get; }
        public float FadeDuration { get; }
        public bool Loop { get; }
        
        public SoundToken(AudioEvent @event, AudioSource source, float delay, float fadeDuration, bool loop) {
            Event = @event;
            Source = source;
            Delay = delay;
            FadeDuration = fadeDuration;
            Loop = loop;
        }
    }
    
    /// <summary> Generic Class managing any AudioEvent </summary>
    /// <author> Written by Nikolas </author>
    public class AudioManager : MonoBehaviour {
        
        public IReadOnlyDictionary<SoundToken, Coroutine> RunningSfx => _runningSfx;
        private readonly Dictionary<SoundToken, Coroutine> _runningSfx = new();
        private readonly Queue<AudioSource> _freeSources = new();

        private AudioSource GetFreeSource() {
            if (_freeSources.TryDequeue(out AudioSource source)) return source;
            return gameObject.AddComponent<AudioSource>();
        }

        private void FreeSource(AudioSource source) {
            if(source == null) return;
            source.Stop();
            source.volume = 0;
            _freeSources.Enqueue(source);
        }

        /// <summary> Used for UnityEditor Events </summary>
        public void PlaySimpleSound(AudioEvent audioEvent) => PlaySound(audioEvent);
        
        /// <summary> Starts a new Audio Source with the to be played Event </summary>
        /// <param name="audioEvent"> the event that will be played </param>
        /// <param name="delay"> the duration between this method call and the start of the vfx </param>
        /// /// <param name="fadeDuration"> the time it takes until the effect is turned up and down </param>
        /// <param name="loop"> if the effect should loop, if true it can only be canceled with the token </param>
        /// <returns> the token that can be used to cancel the vfx </returns>
        public SoundToken PlaySound(AudioEvent audioEvent, float delay = 0, float fadeDuration = 0, bool loop = false) {

            SoundToken token = new SoundToken(audioEvent, GetFreeSource(), delay, fadeDuration, loop);
            Coroutine routine = StartCoroutine(PlayCoroutine(token));
            _runningSfx.Add(token, routine);
            return token;
        }

        /// <summary> Stops a certain played Sound </summary>
        /// <param name="token"> the token received when playing this sound </param>
        public void StopSound(SoundToken token) {

            if (token == null) return;
            if (!_runningSfx.TryGetValue(token, out Coroutine coroutine)) return;
            
            if (coroutine != null) StopCoroutine(coroutine);
            FreeSource(token.Source);
        }

        /// <summary> Stops a certain played Sound, but fade it out instead of ending abruptly. </summary>
        /// <param name="token"> The token received when playing this sound </param>
        public void StopSoundWithFadeOut(SoundToken token) {
            
            if (token == null) return;
            if (!_runningSfx.TryGetValue(token, out Coroutine coroutine)) return;
            
            if (coroutine != null) StopCoroutine(coroutine);

            if (token.Source != null) StartCoroutine(StopCoroutine(token));
        }

        /// <summary>Coroutine that creates the AudioSource, plays the sound and removes the AudioSource after that</summary>
        /// <param name="token"> the data class containing all information about the to be played vfx </param>
        private IEnumerator PlayCoroutine(SoundToken token) {
            
            // wait the provided delay
            yield return new WaitForSeconds(token.Delay);

            // add SoundSource and play the vfx
            token.Event.Play(token.Source);

            // fade in the volume
            float targetVolume = token.Source.volume;
            
            float timePassed = 0;
            while (timePassed < token.FadeDuration) {
                float progress = timePassed / token.FadeDuration;
                token.Source.volume = Mathf.Lerp(0, targetVolume, progress);
                timePassed += Time.deltaTime;
                yield return null;
            }
            
            // if we loop just exit and let it play forever
            if (token.Loop) {
                token.Source.loop = true;
                yield break;
            }
            
            // wait the time of the sfx (subtract fadeIn and fadeOut)
            yield return new WaitForSecondsRealtime(Mathf.Max(token.Source.clip.length - token.FadeDuration * 2, 0));

            yield return StopCoroutine(token);
        }

        private IEnumerator StopCoroutine(SoundToken token) {

            float startVolume = token.Source.volume;
            
            float timePassed = 0;
            while (timePassed < token.FadeDuration) {
                float progress = timePassed / token.FadeDuration;
                token.Source.volume = Mathf.Lerp(startVolume, 0, progress);
                timePassed += Time.deltaTime;
                yield return null;
            }
            
            FreeSource(token.Source);
        }

        private void OnDestroy() {
            StopAllCoroutines();
        }
        
        /// <summary> Keeps the GameObject of the SFXManager alive until all sounds on it finished playing. </summary>
        public void DestroyManagerAfterSoundEnded() {
            
            // de-parent so the object doesn't get destroyed when the projectile dies
            transform.parent = null;
            
            float longestTimeLeftToPlay = 0;
            
            // go through every currently registered sound of this manager...
            foreach (KeyValuePair<SoundToken, Coroutine> runningSound in RunningSfx) {
                // ...check if the source still exists (could be gone due to pausing)...
                if (runningSound.Key.Source == null) continue;
                
                // ...check if the remaining duration of this clip is longer than the remaining duration of any other clip...
                float tempTime = runningSound.Key.Source.clip.length - runningSound.Key.Source.time;
                
                // ...if yes, store this duration...
                if (longestTimeLeftToPlay < tempTime) {
                    longestTimeLeftToPlay = tempTime;
                }
            }

            // ...and use it to determine after what time the SFXManager can be destroyed, since all sounds will have stopped playing by then
            Destroy(gameObject, longestTimeLeftToPlay);
        }
        
    }

}