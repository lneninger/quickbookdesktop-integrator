using FocusApplication.Business.Commands;

namespace ApplicationLogic.Business.Commands
{
    public interface ICommandAction<TOutput>: ICommand
    {
        TOutput Execute();
    }

}