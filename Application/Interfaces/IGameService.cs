using Domain.Entities;

namespace Application.Interfaces;

public interface IGameService
{
    void ConfigurarConsole();
    List<Jogador> CriarJogadores();
    bool JogadorHumanoTemFichas(List<Jogador> jogadores);
    void DefinirEstrategias(List<Jogador> jogadores);
    void RealizarApostas(List<Jogador> jogadores);
    void DistribuirCartas(List<Jogador> jogadores);
    void AvaliarVencedores(List<Jogador> jogadores);
    List<(Jogador jogador, bool ocultarSegundaCarta)> ObterEstadoMesa(List<Jogador> jogadores, bool ocultarSegundaCartaDealer = true);
    Carta PuxarCarta();
}