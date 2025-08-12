using Cysharp.Threading.Tasks;
// ReSharper disable UnusedMethodReturnValue.Global
// ReSharper disable UnusedMemberInSuper.Global

namespace Commons
{
    public interface ICommandExecutionContext : IObservable
    {
        bool CanExecuteCommand { get; }
        void BeginCommandExecution(ICommand command);
        void EndCommandExecution(ICommand command);
    }
}