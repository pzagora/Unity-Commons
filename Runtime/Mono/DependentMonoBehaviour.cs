using Commons.Injection;

namespace Commons.Mono
{
    public abstract class DependentMonoBehaviour : ValidatedMonoBehaviour
    {
        private void Awake()
            => Binder.Bind(this);

        private void OnDestroy()
            => Binder.Unbind(this);
    }
}
