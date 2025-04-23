using System.Collections;
using UnityEngine;

namespace Include
{
    public interface ICoroutineHandler
    {
        Coroutine StartCoroutine(IEnumerator routine);
        void StopCoroutine(Coroutine routine);
    }
}

