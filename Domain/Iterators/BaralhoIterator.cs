using Domain.Entities;
using Domain.Interfaces.Iterator;

namespace Domain.Iterators
{
    public class BaralhoIterator : IBaralhoIterator
    {
        private readonly List<Carta> _cartas;
        private int _currentIndex = 0;

        public BaralhoIterator(List<Carta> cartas)
        {
            _cartas = cartas;
        }

        public bool HasNext()
        {
            return _currentIndex < _cartas.Count;
        }

        public Carta Next()
        {
            if (!HasNext())
                throw new InvalidOperationException("Não há mais cartas no baralho.");

            return _cartas[_currentIndex++];
        }
    }
}