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

        public UniTask Fire(object _) 
            => Fire();

        public UniTask Fire()
        {
            if (!CanFire) return UniTask.CompletedTask;

            _callback.Invoke();
            return UniTask.CompletedTask;
        }

        public bool CanFire => _context.CanExecuteCommand;
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

        public UniTask Fire(TData data)
        {
            if (!CanFire) 
                return UniTask.CompletedTask;

            _callback.Invoke(data);
            return UniTask.CompletedTask;
        }

        public UniTask Fire(object data)
        {
            try
            {
                return Fire((TData)data);
            }
            catch (InvalidCastException)
            {
                Debug.LogError($"Command expected {typeof(TData)} but got {data?.GetType()} instead");
                return UniTask.CompletedTask;
            }
        }

        public bool CanFire => _context.CanExecuteCommand;
        public bool IsBlocking => false;
    }
}