using System;
using System.Collections.Generic;
using Command;
using UnityEngine;

namespace Bootstrap
{
    public class Bootstrap : IBootstrap
    {
        public event Action OnExecuteAllComandsNotify;
        
        private Queue<ICommand> _queueCommands = new Queue<ICommand>();

        public void Add(ICommand command)
        {
            if (command == null)
            {
                Debug.LogError("The command is null");
                return;
            }
            _queueCommands.Enqueue(command);
        }
        
        public void Execute()
        {
            if (_queueCommands.Count == 0)
            {
                OnExecuteAllComandsNotify?.Invoke();
                return;
            }
            var command = _queueCommands.Dequeue();
            command.OnCommandExecuteNotify += Execute;
            command.Execute();
        }
    }
}