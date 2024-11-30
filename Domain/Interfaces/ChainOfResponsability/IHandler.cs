using Domain.Dtos;

namespace Domain.Interfaces.ChainOfResponsability
{
    public interface IHandler
    {
        IHandler DefinirProximo(IHandler handler);
        void Processar(DefinirVencedoresDto request);
    }
}