using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public interface ICoroutineRunner
    {
        Coroutine StartCoroutine(IEnumerator coroutine);
    }
}