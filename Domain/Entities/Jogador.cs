using Domain.Interfaces.Strategy;
using Domain.Resources;

namespace Domain.Entities
{
    public abstract class Jogador
    {
        public string Nome { get; set; } = string.Empty;
        public List<Carta> Cartas { get; set; } = new List<Carta>();

        protected IJogadorStrategy _jogadorStrategy;

        protected Jogador(string nome, IJogadorStrategy jogadorStrategy)
        {
            Nome = nome;
            _jogadorStrategy = jogadorStrategy;
        }

        public void SetStrategy(IJogadorStrategy strategy)
        {
            _jogadorStrategy = strategy;
        }

        public Enums.Acao Jogar()
        {
            return _jogadorStrategy.RealizarJogada(Cartas);
        }
    }
}