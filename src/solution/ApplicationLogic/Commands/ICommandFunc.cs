﻿namespace ApplicationLogic.Commands
{
    public interface ICommandFunc<TInput, TOutput> where TOutput: class, new()
    {
        TOutput Execute(TInput input);
    }
}