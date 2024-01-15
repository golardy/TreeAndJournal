using MediatR;
using TreeAndJournal.Domain.Abstractions;

namespace TreeAndJournal.Application.Abstractions
{
    public interface ICommandHandler<TCommand> : IRequestHandler<TCommand, Result>
        where TCommand : ICommand
    {
    }
}
