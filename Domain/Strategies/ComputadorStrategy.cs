using Domain.Entities;
using Domain.Interfaces.Strategy;
using Domain.Resources;

namespace Domain.Strategies
{
    public class ComputadorStrategy : IJogadorStrategy
    {
        private static readonly Random _random = new Random();

        public int RealizarAposta(Jogador computador)
        {
            if (computador.Pontuacao > 70)
            {
                if (computador.Vitorias > 2)
                {
                    return _random.Next(2, 5);
                }

                return _random.Next(1, 4);
            }
            else if (computador.Pontuacao > 40)
            {
                if (computador.Vitorias > 2)
                {
                    return _random.Next(1, 3);
                }
            }

            return computador.Pontuacao;
        }

        public Enums.Acao RealizarJogada(List<Carta> cartas)
        {
            var valor = Carta.GetValorTotal(cartas);

            if (valor >= 21) return Enums.Acao.Manter;

            if (valor <= 11) return _random.Next(2) == 0 ? Enums.Acao.Puxar : Enums.Acao.Manter;

            return _random.Next(4) == 0 ? Enums.Acao.Puxar : Enums.Acao.Manter;
        }
    }
}