using Domain.Entities;
using Domain.Interfaces.Strategy;
using Domain.Resources;
using System;

namespace Domain.Strategies
{
    public class ComputadorStrategy : IJogadorStrategy
    {
        private static readonly Random _random = new Random();

        public Enums.Acao RealizarJogada(List<Carta> cartas)
        {
            return _random.Next(2) == 0 ? Enums.Acao.Puxar : Enums.Acao.Manter;
        }
    }
}