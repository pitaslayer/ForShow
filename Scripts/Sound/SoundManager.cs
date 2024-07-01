using System.Collections.Generic;
using UnityEngine;

namespace PUZ.Sound
{
    public class SoundManager : MonoBehaviour
    {
        [System.Serializable]
        public class SoundData
        {
            public string name;
            public AudioClip audioClip;
            public float playedTime;
        }
    
        [SerializeField]
        private SoundData[] soundDatas;
    
        private AudioSource[] audioSourceList = new AudioSource[20];

        private Dictionary<string, SoundData> soundDictionary = new Dictionary<string, SoundData>();
    
        [SerializeField]
        private float playableDistance = 0.2f;
    
        private void Awake()
        {

            for (var i = 0; i < audioSourceList.Length; ++i)
            {
                audioSourceList[i] = gameObject.AddComponent<AudioSource>();
            }

            foreach (var soundData in soundDatas)
            {
                soundDictionary.Add(soundData.name, soundData);
            }
        }

        private AudioSource GetUnusedAudioSource()
        {
            for (var i = 0; i < audioSourceList.Length; ++i)
            {
                if (audioSourceList[i].isPlaying == false) return audioSourceList[i];
            }
    
            return null; 
        }

        public void Play(AudioClip clip, AudioSource source)
        {
            if (source == null) return; 
            source.clip = clip;
            source.Play();
        }

        public void Play(AudioClip clip, AudioSource source, bool loop = false)
        {
            if (source == null) return; 
            source.clip = clip;
            source.loop = loop;
            source.Play();
        }
    
    
        public void Play(AudioClip clip)
        {
            var audioSource = GetUnusedAudioSource();
            if (audioSource == null) return; 
            audioSource.clip = clip;
            audioSource.Play();
        }
    
        public void Play(string name)
        {
            if (soundDictionary.TryGetValue(name, out var soundData)) 
            {
                if (Time.realtimeSinceStartup - soundData.playedTime < playableDistance) return;    
                soundData.playedTime = Time.realtimeSinceStartup;
                Play(soundData.audioClip); 
            }
            else
            {
                Debug.LogWarning($"No sound:{name}");
            }
        }

        public void Play(string nam, AudioSource source, bool loop = false)
        {
            if (soundDictionary.TryGetValue(name, out var soundData)) 
            {
                if (Time.realtimeSinceStartup - soundData.playedTime < playableDistance) return;    
                soundData.playedTime = Time.realtimeSinceStartup;
                Play(soundData.audioClip, source, loop); 
            }
            else
            {
                Debug.LogWarning($"No sound:{name}");
            }
        }

        public void Stop(string nam, AudioSource source)
        {
            if (source != null && source.clip.name.CompareTo(name) == 0)
            {
                source.Stop();
            }
            else
            {
                Debug.LogWarning($"No sound:{name}");
            }
        }

        public void Stop(AudioSource source)
        {
            if (source != null) 
            {
                source.Stop();
            }
            else
            {
                Debug.LogWarning($"No source:{name}");
            }
        }
    }
} 