using UnityEngine;

namespace Cephei
{
    public interface IEffect
    {
        public void Play();

        public void Play(Vector3 position, Quaternion rotation) => Play();
    }
}
