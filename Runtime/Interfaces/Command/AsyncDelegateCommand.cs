using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;
// ReSharper disable Unity.PerformanceCriticalCodeInvocation

namespace Commons
{
    public class AsyncDelegateCommand : ICommand
    {
        private readonly Func<UniTask> _callback;
        private readonly ICommandExecutionContext _context;
        private UniTask _currentTask;

        public AsyncDelegateCommand(Func<UniTask> callback, ICommandExecutionContext context, bool isBlocking = true)
        {
            _callback = callback ?? throw new ArgumentNullException(nameof(callback));
            _context = context ?? throw new ArgumentNullException(nameof(context));
            IsBlocking = isBlocking;
        }
        
        public UniTask Execute(object _ = null) 
            => TryExecute();

        private async UniTask TryExecute()
        {
            if (!CanExecute) 
                return;
            
            try
            {
                _context.BeginCommandExecution(this);
                _currentTask = _callback.Invoke();
                await _currentTask;
            }
            finally
            {
                _currentTask = default;
                _context.EndCommandExecution(this);
            }

            await Task.Yield();
        }

        public bool CanExecute => _context.CanExecuteCommand && _currentTask.Status != UniTaskStatus.Pending;
        public bool IsBlocking { get; }
    }

    public class AsyncDelegateCommand<TData> : ICommand<TData>
    {
        private readonly Func<TData, UniTask> _callback;
        private readonly ICommandExecutionContext _context;
        private UniTask _currentTask;

        public AsyncDelegateCommand(Func<TData, UniTask> callback, ICommandExecutionContext context, bool isBlocking = true)
        {
            _callback = callback ?? throw new ArgumentNullException(nameof(callback));
            _context = context ?? throw new ArgumentNullException(nameof(context));
            IsBlocking = isBlocking;
        }

        public async UniTask Execute(TData payload)
        {
            if (!CanExecute) 
                return;

            try
            {
                _context.BeginCommandExecution(this);
                _currentTask = _callback.Invoke(payload);
                await _currentTask;
            }
            finally
            {
                _currentTask = default;
                _context.EndCommandExecution(this);
            }
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

        public bool CanExecute => _context.CanExecuteCommand && _currentTask.Status != UniTaskStatus.Pending;
        public bool IsBlocking { get; }
    }
}