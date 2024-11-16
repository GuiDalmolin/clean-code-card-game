using Domain.Entities;
using Domain.Interfaces.Strategy;
using Domain.Resources;

namespace Domain.Strategies
{
    public class ComputadorStrategy : IJogadorStrategy
    {
        private static readonly Random _random = new Random();

        public Enums.Acao RealizarJogada(List<Carta> cartas)
        {
            var valor = Carta.GetValorTotal(cartas);

            if (valor >= 21) return Enums.Acao.Manter;

            if (valor <= 11) return _random.Next(2) == 0 ? Enums.Acao.Puxar : Enums.Acao.Manter;

            return _random.Next(4) == 0 ? Enums.Acao.Puxar : Enums.Acao.Manter;
        }
    }
}