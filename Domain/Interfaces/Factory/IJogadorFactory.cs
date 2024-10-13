using Domain.Entities;
using Domain.Resources;

namespace Domain.Interfaces.Factory
{
    public interface IJogadorFactory
    {
        Jogador CriarJogador(string nome, Enums.TipoJogador tipo);
    }
}