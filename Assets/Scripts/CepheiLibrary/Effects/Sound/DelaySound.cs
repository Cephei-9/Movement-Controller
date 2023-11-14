using System.Collections;
using UnityEngine;

namespace Cephei
{
    public class DelaySound : ISound
    {
        [System.Serializable]
        public class Data
        {
            public float DelayTime = 1;
        }

        private Data _data;

        private IEffect _sound;
        private ICoroutineRunner _coroutineRunner;

        private Coroutine _coroutine;

        public DelaySound(Data data, IEffect sound, ICoroutineRunner coroutineRunner)
        {
            _data = data;
            _sound = sound;
            _coroutineRunner = coroutineRunner;
        }

        public void Play() => _coroutine =  _coroutineRunner.StartCoroutine(DelayPlay());

        public void Stop()
        {
            if (_coroutine != null)
                _coroutineRunner.StopCoroutine(_coroutine);
        }

        private IEnumerator DelayPlay()
        {
            yield return new WaitForSeconds(_data.DelayTime);

            _sound.Play();
            _coroutine = null;
        }
    }
}
