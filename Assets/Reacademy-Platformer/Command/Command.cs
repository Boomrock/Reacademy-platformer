using System;

namespace Command
{
    public abstract class Command : ICommand
    {
        public event Action OnCommandExecuteNotify;

        public virtual void Execute()
        {
            OnCommandExecuteNotify?.Invoke();
        }
        public abstract void Undo();
    }
}