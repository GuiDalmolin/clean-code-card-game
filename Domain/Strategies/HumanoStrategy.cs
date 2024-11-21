using Domain.Entities;
using Domain.Interfaces.Strategy;
using Domain.Resources;

namespace Domain.Strategies
{
    public class HumanoStrategy : IJogadorStrategy
    {
        private readonly Func<string> _inputProvider;

        public HumanoStrategy(Func<string>? inputProvider = null)
        {
            _inputProvider = inputProvider ?? (() => Console.ReadLine() ?? string.Empty);
        }

        public int RealizarAposta(Jogador jogador)
        {
            Console.Write($"Apostar quantas fichas? (max {jogador.Pontuacao}) -> ");
            var input = _inputProvider()?.ToLower();

            if (int.TryParse(input, out int aposta) && aposta > 0 && aposta <= jogador.Pontuacao)
            {
                return aposta;
            }

            return jogador.Pontuacao >= 10 ? 10 : jogador.Pontuacao;
        }

        public Enums.Acao RealizarJogada(List<Carta> cartas)
        {
            Console.Write("Deseja puxar uma carta? (s/n) ");
            var input = _inputProvider()?.ToLower();
            return input?.ToLower() == "s" ? Enums.Acao.Puxar : Enums.Acao.Manter;
        }
    }
}