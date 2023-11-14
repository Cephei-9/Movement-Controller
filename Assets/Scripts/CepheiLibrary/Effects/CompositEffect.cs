using UnityEngine;

namespace Cephei
{
    public class CompositEffect : IEffect
    {
        private IEffect[] _effects;

        public CompositEffect(params IEffect[] effects) => _effects = effects;

        public void Play()
        {
            foreach (var item in _effects)
            {
                item.Play();
            }
        }

        public void Play(Vector3 position, Quaternion rotation)
        {
            foreach (var item in _effects)
            {
                item.Play(position, rotation);
            }
        }
    }
}
