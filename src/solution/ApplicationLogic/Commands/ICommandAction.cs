namespace ApplicationLogic.Commands
{
    public interface ICommandAction<TOutput>: ICommand
    {
        TOutput Execute();
    }

}