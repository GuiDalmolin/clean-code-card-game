using Domain.Strategies;

namespace Domain.Entities
{
    public class Dealer : Jogador
    {
        public Dealer(string nome) : base(nome, new DealerStrategy())
        {
        }
    }
}