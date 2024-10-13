using Domain.Entities;
using Domain.Interfaces.Strategy;
using Domain.Resources;

namespace Domain.Strategies
{
    public class JogadorManterStrategy : IJogadorStrategy
    {
        public Enums.Acao RealizarJogada(List<Carta> cartas)
        {
            return Enums.Acao.Manter;
        }
    }
}