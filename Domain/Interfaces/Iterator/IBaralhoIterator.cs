using Domain.Entities;

namespace Domain.Interfaces.Iterator
{
    public interface IBaralhoIterator
    {
        bool HasNext();
        Carta Next();
        int Restantes { get; }
    }
}