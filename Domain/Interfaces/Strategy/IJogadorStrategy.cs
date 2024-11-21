using Domain.Entities;
using Domain.Resources;

namespace Domain.Interfaces.Strategy
{
    public interface IJogadorStrategy
    {
        Enums.Acao RealizarJogada(List<Carta> cartas);
        int RealizarAposta(Jogador jogador);
    }
}