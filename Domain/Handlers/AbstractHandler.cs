using Domain.Dtos;
using Domain.Interfaces.ChainOfResponsability;

namespace Domain.Handlers;

public abstract class AbstractHandler : IHandler
{
    private IHandler? _nextHandler;

    public IHandler DefinirProximo(IHandler handler)
    {
        _nextHandler = handler;

        return handler;
    }

    public virtual void Processar(DefinirVencedoresDto request)
    {
        _nextHandler?.Processar(request);
    }
}