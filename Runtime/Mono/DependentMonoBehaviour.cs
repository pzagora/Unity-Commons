namespace Commons
{
    public abstract class DependentMonoBehaviour : ValidatedMonoBehaviour
    {
        protected virtual void Awake()
            => Binder.Bind(this);

        private void OnDestroy()
            => Binder.Unbind(this);
    }
}
