using Domain.Interfaces.Iterator;
using Domain.Iterators;
using Domain.Resources;

namespace Domain.Entities
{
    public class Baralho
    {
        public List<Carta> Cartas { get; private set; }

        public Baralho()
        {
            Cartas = GerarBaralhoPadrao();
            Embaralhar();
        }

        private List<Carta> GerarBaralhoPadrao()
        {
            var cartas = new List<Carta>();
            foreach (var numero in Enum.GetValues(typeof(Enums.Numero)).Cast<Enums.Numero>())
            {
                foreach (var naipe in Enum.GetValues(typeof(Enums.Naipe)).Cast<Enums.Naipe>())
                {
                    cartas.Add(new Carta { Numero = numero, Naipe = naipe });
                }
            }
            return cartas;
        }

        private void Embaralhar()
        {
            var random = new Random();
            Cartas = Cartas.OrderBy(c => random.Next()).ToList();
        }

        public IBaralhoIterator CriarIterator()
        {
            return new BaralhoIterator(Cartas);
        }
    }
}