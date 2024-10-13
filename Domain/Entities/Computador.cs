using Domain.Strategies;

namespace Domain.Entities
{
    public class Computador : Jogador
    {
        public Computador(string nome) : base(nome, new ComputadorStrategy())
        {
        }
    }
}