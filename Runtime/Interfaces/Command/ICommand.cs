using Cysharp.Threading.Tasks;
// ReSharper disable UnusedMethodReturnValue.Global
// ReSharper disable UnusedMemberInSuper.Global

namespace Commons
{
    public interface ICommand
    {
        UniTask Execute(object payload = null);
        bool CanExecute { get; }
        bool IsBlocking { get; }
    }

    public interface ICommand<in TData> : ICommand
    {
        UniTask Execute(TData payload);
    }
}