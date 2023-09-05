using System;

namespace Command
{
    public interface ICommand
    {
        event Action OnCommandExecuteNotify;
        void Execute();
        void Undo();
    }
}