using Cysharp.Threading.Tasks;
// ReSharper disable UnusedMethodReturnValue.Global
// ReSharper disable UnusedMemberInSuper.Global

namespace Commons
{
    public interface ICommand
    {
        UniTask Fire(object data);
        bool CanFire { get; }
        bool IsBlocking { get; }
    }

    public interface ICommand<in TData> : ICommand
    {
        UniTask Fire(TData data);
    }
}