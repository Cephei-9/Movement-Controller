using UnityEngine;

namespace Cephei
{
    public class OneShotSound : IEffect
    {
        private AudioSource _source;
        private SoundData _data;

        public OneShotSound(SoundData data, AudioSource source)
        {
            _data = data;
            _source = source;
        }

        public void Play() => _source.PlayOneShot(_data.Clip, _data.Volume);
    }
}
