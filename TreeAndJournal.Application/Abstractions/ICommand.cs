using MediatR;
using TreeAndJournal.Domain.Abstractions;

namespace TreeAndJournal.Application.Abstractions
{
    public interface ICommand : IRequest<Result>, IBaseCommand
    {
    }

    public interface IBaseCommand
    { 
    }
}
