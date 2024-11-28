using Domain.Entities;
using Domain.Interfaces.Strategy;
using Domain.Resources;

namespace Domain.Strategies
{
    public class DealerStrategy : IJogadorStrategy
    {
        public int RealizarAposta(Jogador jogador)
        {
            return jogador.Pontuacao;
        }

        public Enums.Acao RealizarJogada(Jogador jogador)
        {
            return Carta.GetValorTotal(jogador.Cartas) < 17 ? Enums.Acao.Puxar : Enums.Acao.Manter;
        }
    }
}