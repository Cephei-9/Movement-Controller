using UnityEngine;

namespace Cephei
{
    public class LoopSound : ISound
    {
        private AudioSource _source;
        private SoundData _data;

        public LoopSound(SoundData data, AudioSource source)
        {
            _data = data;
            _source = source;

            _source.loop = true;
        }

        public void Play()
        {
            _data.SetupSource(_source);

            if (_source.isPlaying == false)
                _source.Play();
        }

        public void Stop() => _source.Stop();
    }
}
