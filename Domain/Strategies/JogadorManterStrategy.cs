using Domain.Entities;
using Domain.Interfaces.Strategy;
using Domain.Resources;

namespace Domain.Strategies
{
    public class JogadorManterStrategy : IJogadorStrategy
    {
        public int RealizarAposta(Jogador jogador)
        {
            return 0;
        }

        public Enums.Acao RealizarJogada(Jogador jogador)
        {
            return Enums.Acao.Manter;
        }
    }
}