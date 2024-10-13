using Domain.Entities;
using Domain.Interfaces.Strategy;
using Domain.Resources;

namespace Domain.Strategies
{
    public class HumanoStrategy : IJogadorStrategy
    {
        public Enums.Acao RealizarJogada(List<Carta> cartas)
        {
            Console.Write("Deseja puxar uma carta? (s/n) ");
            var input = Console.ReadLine();
            return input?.ToLower() == "s" ? Enums.Acao.Puxar : Enums.Acao.Manter;
        }
    }
}