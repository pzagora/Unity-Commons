using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
// ReSharper disable Unity.PerformanceCriticalCodeInvocation

namespace Commons
{
    public class DelegateCommand<TData> : ICommand<TData>
    {
        private readonly Action<TData> _callback;
        private readonly ICommandExecutionContext _context;
            
        public DelegateCommand(ICommandExecutionContext context, Action<TData> callback)
        {
            // HACK: if context == null or CanExecuteCommand throws an exception then the below code will crash (expected)
            var canExecute = context.CanExecuteCommand;
            this._context = context;
            this._callback = callback;
        }

        public async UniTask Fire(TData data)
        {
            if (!_context.CanExecuteCommand)
            {
                return;
            }
            
            _callback?.Invoke(data);
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

    public class AsyncDelegateCommand<TData> : ICommand<TData>
    {
        private readonly Func<TData, UniTask> _callback;
        private readonly ICommandExecutionContext _context;
        private UniTask _currentTask;

        public AsyncDelegateCommand(ICommandExecutionContext context, Func<TData, UniTask> callback, bool isBlocking = true)
        {
            // HACK: if context == null or CanExecuteCommand throws an exception then the below code will crash (expected)
            var canExecute = context.CanExecuteCommand;
            this._context = context;
            this._callback = callback;
            IsBlocking = isBlocking;
        }

        public async UniTask Fire(TData data)
        {
            if (!CanFire)
            {
                return;
            }
            
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