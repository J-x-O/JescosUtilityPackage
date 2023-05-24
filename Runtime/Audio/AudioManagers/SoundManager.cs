using System;
using UnityEngine;
using UnityEngine.Audio;

namespace Plugins.Audio.AudioManagers {

    [DefaultExecutionOrder(-100)]
    public class SoundManager : MonoBehaviour {
    
        /// <summary>Static instance of the GameSettingsManager.</summary>
        public static SoundManager Instance { get; private set;  }

        public static AudioMixerGroup MusicAudioGroup => Instance != null ? Instance._musicAudioGroup : null;
        [SerializeField] private AudioMixerGroup _musicAudioGroup;

        public static AudioMixerGroup EffectsAudioGroup => Instance != null ? Instance._effectsAudioGroup : null;
        [SerializeField] private AudioMixerGroup _effectsAudioGroup;

        public static AudioMixerGroup VoicesAudioGroup => Instance != null ? Instance._voicesAudioGroup : null;
        [SerializeField] private AudioMixerGroup _voicesAudioGroup;
    
        public static AudioMixerGroup AmbienceAudioGroup => Instance != null ? Instance._ambienceAudioGroup : null;
        [SerializeField] private AudioMixerGroup _ambienceAudioGroup;

        private void Awake() {
            if (Instance != null) {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

}
