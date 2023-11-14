using UnityEngine;

namespace Cephei
{
    public class Sound : ISound
    {
        private SoundData _data;
        private AudioSource _source;

        public Sound(SoundData data, AudioSource source)
        {
            _data = data;
            _source = source;
        }

        public void Play()
        {
            _data.SetupSource(_source);
            _source.Play();
        }

        public void Stop() => _source.Stop();
    }
}
