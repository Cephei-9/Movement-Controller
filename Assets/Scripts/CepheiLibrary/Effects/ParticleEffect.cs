using UnityEngine;

namespace Cephei
{
    public class ParticleEffect : IEffect
    {
        [System.Serializable]
        public class Data
        {
            public ParticleSystem ParticleEffect;
        }

        private Data _data;

        private Transform _parentTransform;
        private Transform _originTransform;

        private Vector3 _originPosition;
        private Quaternion _originRotation;

        private Vector3 _position => _originTransform != null ? _originTransform.position : _originPosition;
        private Quaternion _rotation => _originTransform != null ? _originTransform.rotation : _originRotation;

        public ParticleEffect(Data data, Vector3 originPosition, Quaternion originRotation, Transform parentTransform)
        {
            _data = data;

            _originPosition = originPosition;
            _originRotation = originRotation;

            _parentTransform = parentTransform;
        }

        public ParticleEffect(Data data, Transform originTransform, Transform parentTransform)
        {
            _data = data;

            _originTransform = originTransform;
            _parentTransform = parentTransform;
        }

        public void Play() => CreateEffect(_position, _rotation);

        public void Play(Vector3 position, Quaternion rotation) => CreateEffect(position, rotation);

        private void CreateEffect(Vector3 position, Quaternion rotation)
        {
            ParticleSystem newEffect = UnityEngine.Object.Instantiate(_data.ParticleEffect, position, rotation, _parentTransform);
            DestroyAfterWork(newEffect);

            newEffect.Play();
        }

        private static void DestroyAfterWork(ParticleSystem newEffect)
        {
            float maxDuration = newEffect.main.duration + newEffect.main.startLifetimeMultiplier;
            UnityEngine.Object.Destroy(newEffect.gameObject, maxDuration);
        }
    }
}
