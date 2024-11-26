using Domain.Entities;
using Domain.Interfaces.Iterator;

namespace Domain.Strategies
{
    public class DoubleDownStrategy : IJogadorStrategy
    {
        private bool jaComprouCarta = false;

        public bool ExecutarAcao(Jogador jogador, IBaralhoIterator iterator)
        {
            if (!jaComprouCarta)
            {
                jogador.Pontuacao -= jogador.ApostaAtual;
                jogador.ApostaAtual *= 2;

                jogador.Cartas.Add(iterator.Next());
                jaComprouCarta = true;
            }
            return true;
        }
    }
}
