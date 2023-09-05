using System;
using Command;

namespace Bootstrap
{
    public interface IBootstrap
    {
        event Action OnExecuteAllComandsNotify;
        void Add(ICommand command);
        void Execute();
    }
}