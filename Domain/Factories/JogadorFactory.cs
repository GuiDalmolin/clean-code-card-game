using Domain.Entities;
using Domain.Interfaces.Factory;
using Domain.Resources;

namespace Domain.Factories
{
    public class JogadorFactory : IJogadorFactory
    {
        public Jogador CriarJogador(string nome, Enums.TipoJogador tipo) =>
            tipo switch
            {
                Enums.TipoJogador.Dealer => new Dealer(nome),
                Enums.TipoJogador.Pessoa => new Humano(nome),
                Enums.TipoJogador.Computador => new Computador(nome),
                _ => throw new ArgumentOutOfRangeException(nameof(tipo), tipo, "Tipo de jogador inválido.")
            };
    }
}