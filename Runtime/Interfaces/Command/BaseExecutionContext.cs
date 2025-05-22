namespace Commons
{
    public class BaseExecutionContext : ICommandExecutionContext
    {
        private bool _lockedByCommand;
        private bool _lockedGlobally;

        public bool CanExecuteCommand => !_lockedByCommand && !_lockedGlobally;
        
        public void BeginCommandExecution(ICommand command)
        {
            if (command.IsBlocking)
            {
                _lockedByCommand = true;
            }
        }

        public void EndCommandExecution(ICommand command)
        {
            if (command.IsBlocking)
            {
                _lockedByCommand = false;
            }
        }

        public void Lock()
        {
            _lockedGlobally = true;
        }

        public void Unlock()
        {
            _lockedGlobally = false;
        }
    }
    
}