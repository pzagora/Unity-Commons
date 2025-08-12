namespace Commons
{
    public class ExecutionContext : BaseObservable, ICommandExecutionContext
    {
        private bool _lockedByCommand;
        private bool _lockedGlobally;
        private bool _canExecuteCommand;

        private bool LockedByCommand
        {
            get => _lockedByCommand;
            set => UpdateValue(ref _lockedByCommand, value);
        }

        private bool LockedGlobally
        {
            get => _lockedGlobally;
            set => UpdateValue(ref _lockedGlobally, value);
        }

        public bool CanExecuteCommand
            => !LockedByCommand && !LockedGlobally;

        public void BeginCommandExecution(ICommand command)
        {
            if (command.IsBlocking)
                LockedByCommand = true;
        }

        public void EndCommandExecution(ICommand command)
        {
            if (command.IsBlocking)
                LockedByCommand = false;
        }

        public void LockContext()
            => LockedGlobally = true;

        public void UnlockContext()
            => LockedGlobally = false;
    }
}