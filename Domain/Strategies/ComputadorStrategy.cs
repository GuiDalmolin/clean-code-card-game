using System.Security.Cryptography;
using Domain.Entities;
using Domain.Interfaces.Strategy;
using Domain.Resources;

namespace Domain.Strategies;

public class ComputadorStrategy : IJogadorStrategy
{
    private static int NextSecure(int min, int max) =>
        RandomNumberGenerator.GetInt32(min, max);

    public int RealizarAposta(Jogador jogador)
    {
        return jogador.Pontuacao switch
        {
            > 70 when jogador.Vitorias > 2 => NextSecure(2, 5) * 10,
            > 70 => NextSecure(1, 4) * 10,
            > 40 when jogador.Vitorias > 2 => NextSecure(1, 3) * 10,
            _ => jogador.Pontuacao
        };
    }

    public Enums.Acao RealizarJogada(Jogador jogador)
    {
        var valor = Carta.GetValorTotal(jogador.Cartas);

        return valor switch
        {
            >= 21 => Enums.Acao.Manter,
            >= 9 and <= 11 when jogador.Turno == 1 =>
                NextSecure(0, 2) == 0 ? Enums.Acao.DoubleDown : Enums.Acao.Manter,
            <= 11 => NextSecure(0, 2) == 0 ? Enums.Acao.Puxar : Enums.Acao.Manter,
            _ => NextSecure(0, 4) == 0 ? Enums.Acao.Puxar : Enums.Acao.Manter
        };
    }
}