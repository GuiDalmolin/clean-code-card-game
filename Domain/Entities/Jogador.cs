using Domain.Interfaces.Strategy;
using Domain.Resources;

namespace Domain.Entities
{
    public abstract class Jogador
    {
        public string Nome { get; set; } = string.Empty;
        public List<Carta> Cartas { get; set; } = new List<Carta>();
        public int Pontuacao = 100;
        public int Aposta = 0;
        public int Vitorias = 0;
        public int Turno = 0;

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
            return _jogadorStrategy.RealizarJogada(this);
        }

        public int Apostar()
        {
            return _jogadorStrategy.RealizarAposta(this);
        }

        public void Resetar()
        {
            Turno = 0;
            Cartas.Clear();
        }



    }
}