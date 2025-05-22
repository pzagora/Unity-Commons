using System.Collections;

namespace Commons.Services
{
    public class CoroutineData
    {
        public readonly IEnumerator Method;
        public readonly UnityEngine.Coroutine Coroutine;
    
        public CoroutineData(IEnumerator enumerator, UnityEngine.Coroutine coroutine)
        {
            Method = enumerator;
            Coroutine = coroutine;
        }
    }
}
