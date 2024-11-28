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

        public Enums.Acao RealizarJogada(Jogador jogador)
        {
            if (jogador.Turno == 1)
                Console.Write("Deseja puxar uma carta (s), manter (n) ou fazer doubledown (d)? ");
            else
                Console.Write("Deseja puxar uma carta (s) ou manter (n)? ");

            var input = _inputProvider()?.ToLower();

            return input switch
            {
                "s" => Enums.Acao.Puxar,
                "n" => Enums.Acao.Manter,
                "d" => jogador.Turno == 1 ? Enums.Acao.DoubleDown : Enums.Acao.Manter,
                _ => Enums.Acao.Manter,
            };
        }
    }
}