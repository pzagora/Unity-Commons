using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
// ReSharper disable Unity.PerformanceCriticalCodeInvocation

namespace Commons
{
    public class DelegateCommand : ICommand
    {
        private readonly Action _callback;
        private readonly ICommandExecutionContext _context;

        public DelegateCommand(Action callback, ICommandExecutionContext context)
        {
            _callback = callback ?? throw new ArgumentNullException(nameof(callback));
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public UniTask Execute(object _) 
            => TryExecute();

        public UniTask TryExecute()
        {
            if (!CanExecute) return UniTask.CompletedTask;

            _callback.Invoke();
            return UniTask.CompletedTask;
        }

        public bool CanExecute => _context.CanExecuteCommand;
        public bool IsBlocking => false;
    }
    
    public class DelegateCommand<TData> : ICommand<TData>
    {
        private readonly Action<TData> _callback;
        private readonly ICommandExecutionContext _context;

        public DelegateCommand(Action<TData> callback, ICommandExecutionContext context)
        {
            _callback = callback ?? throw new ArgumentNullException(nameof(callback));
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public UniTask Execute(TData payload)
        {
            if (!CanExecute) 
                return UniTask.CompletedTask;

            _callback.Invoke(payload);
            return UniTask.CompletedTask;
        }

        public UniTask Execute(object payload)
        {
            try
            {
                return Execute((TData)payload);
            }
            catch (InvalidCastException)
            {
                Debug.LogError($"Command expected {typeof(TData)} but got {payload?.GetType()} instead");
                return UniTask.CompletedTask;
            }
        }

        public bool CanExecute => _context.CanExecuteCommand;
        public bool IsBlocking => false;
    }
}