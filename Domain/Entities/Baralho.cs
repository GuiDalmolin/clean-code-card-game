using System.Security.Cryptography;
using Domain.Interfaces.Iterator;
using Domain.Iterators;
using Domain.Resources;

namespace Domain.Entities
{
    public class Baralho
    {
        private List<Carta> Cartas { get; set; }

        public Baralho()
        {
            Cartas = GerarBaralhoPadrao();
            Embaralhar();
        }

        private static List<Carta> GerarBaralhoPadrao()
        {
            return (from numero in Enum.GetValues<Enums.Numero>()
                from naipe in Enum.GetValues<Enums.Naipe>()
                select new Carta
                {
                    Numero = numero,
                    Naipe = naipe
                }).ToList();
        }

        private void Embaralhar()
        {
            Cartas = Cartas
                .OrderBy(_ => RandomNumberGenerator.GetInt32(int.MinValue, int.MaxValue))
                .ToList();
        }

        public IBaralhoIterator CriarIterator()
        {
            return new BaralhoIterator(Cartas);
        }
    }
}