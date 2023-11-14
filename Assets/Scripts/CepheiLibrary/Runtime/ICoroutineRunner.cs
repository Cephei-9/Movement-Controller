using System.Collections;
using UnityEngine;

namespace Cephei
{
    public interface ICoroutineRunner
    {
        public Coroutine StartCoroutine(IEnumerator routine);

        public void StopCoroutine(Coroutine coroutine);
    }
}
