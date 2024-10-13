using Domain.Strategies;

namespace Domain.Entities
{
    public class Humano : Jogador
    {
        public Humano(string nome) : base(nome, new HumanoStrategy())
        {
        }
    }
}