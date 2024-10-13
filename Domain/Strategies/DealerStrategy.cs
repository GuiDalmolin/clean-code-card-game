using Domain.Entities;
using Domain.Interfaces.Strategy;
using Domain.Resources;

namespace Domain.Strategies
{
    public class DealerStrategy : IJogadorStrategy
    {
        public Enums.Acao RealizarJogada(List<Carta> cartas)
        {
            return Carta.GetValorTotal(cartas) < 17 ? Enums.Acao.Puxar : Enums.Acao.Manter;
        }
    }
}