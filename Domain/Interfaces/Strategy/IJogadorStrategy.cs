using Domain.Entities;
using Domain.Resources;

namespace Domain.Interfaces.Strategy
{
    public interface IJogadorStrategy
    {
        Enums.Acao RealizarJogada(Jogador jogador);
        int RealizarAposta(Jogador jogador);
    }
}