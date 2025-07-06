using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;
// ReSharper disable Unity.PerformanceCriticalCodeInvocation

namespace Commons
{
    public class DelegateCommand
    {
        protected static readonly ICommandExecutionContext DefaultContext
            = new BaseExecutionContext();
    }
    
    public class DelegateCommand<TData> : DelegateCommand, ICommand<TData>
    {
        private readonly Action<TData> _callback;
        private readonly ICommandExecutionContext _context;
            
        public DelegateCommand(Action<TData> callback, ICommandExecutionContext context = null)
        {
            _context = context ?? DefaultContext;
            _callback = callback;
        }

        public async UniTask Fire(TData data)
        {
            if (!CanFire)
            {
                return;
            }
            
            _callback?.Invoke(data);
            await Task.CompletedTask;
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

    public class AsyncDelegateCommand<TData> : DelegateCommand, ICommand<TData>
    {
        private readonly Func<TData, UniTask> _callback;
        private readonly ICommandExecutionContext _context;
        private UniTask _currentTask;

        public AsyncDelegateCommand(Func<TData, UniTask> callback, bool isBlocking = true, ICommandExecutionContext context = null)
        {
            _context = context ?? DefaultContext;
            _callback = callback;
            IsBlocking = isBlocking;
        }

        public async UniTask Fire(TData data)
        {
            if (!CanFire)
                return;
            
            _context.BeginCommandExecution(this);
            _currentTask = _callback.Invoke(data);
            await _currentTask;
            _currentTask = default;
            _context.EndCommandExecution(this);
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

        public bool CanFire => _context.CanExecuteCommand && _currentTask.Status != UniTaskStatus.Pending;
        public bool IsBlocking { get; }
    }
}