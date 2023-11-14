using UnityEngine;

namespace Cephei
{
    public class AudioSourceInstancer
    {
        public AudioSource GetSource(GameObject gameObject, bool loop = false)
        {
            AudioSource newSource = gameObject.AddComponent<AudioSource>();
            newSource.loop = loop;
            return newSource;
        }
    }
}
