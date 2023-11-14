using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Cephei
{
    [System.Serializable]
    public class SoundData
    {
        public AudioClip Clip;
        [Range(0, 1)] public float Volume = 1;

        public void SetupSource(AudioSource source)
        {
            source.clip = Clip;
            source.volume = Volume;
        }
    }

    [System.Serializable]
    public class SoundDataMax
    {
        public AudioClip Clip;

        public List<AudioClip> Clips = new List<AudioClip>();
        
        [Range(0, 1)] 
        public float Volume = 1;

        [Range(0, 1)]
        public float VolumeRandomFactor;
        
        [Range(-3, 3)] 
        public float Pitch = 1;

        [Range(0, 1)]
        public float PitchRandomFactor;

        private AudioClip _previousClip;

        public void PlayClip(AudioSource source)
        {
            source.pitch = GetRandomizedPitch();
            source.PlayOneShot(GetRandomClip(), GetRandomizedVolume());
        }

        public float GetRandomizedVolume() => 
            GetRandomizedValue(Volume, VolumeRandomFactor);
        
        public float GetRandomizedPitch() => 
            GetRandomizedValue(Pitch, PitchRandomFactor);

        public AudioClip GetRandomClip()
        {
            if (Clips.Count > 1)
            {
                while (true)
                {
                    AudioClip clip = Clips[Random.Range(0, Clips.Count)];

                    if (clip != _previousClip)
                    {
                        _previousClip = clip;
                        return clip;
                    }
                }
            }
            
            if (Clips != null && Clips.Count > 0 && Clips[0] != null)
                return Clips[0];
            
            return Clip;
        }

        private float GetRandomizedValue(float value, float randomFactor) => 
            value + value * CustomRandom.Sign() * Random.Range(0, randomFactor);
    }
}
