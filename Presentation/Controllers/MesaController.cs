using Application.Interfaces;
using Domain.Entities;
using Domain.Resources;
using Domain.Strategies;
using Presentation.Views;

namespace Presentation.Controllers;

public class MesaController(IGameService? jogo = null)
{
    private readonly IGameService _jogo = jogo ?? new Application.Services.GameService();

    public void IniciarJogo()
    {
        _jogo.ConfigurarConsole();
        var jogadores = _jogo.CriarJogadores();

        var rodadas = 0;
        while (_jogo.JogadorHumanoTemFichas(jogadores))
        {
            rodadas++;

            _jogo.DefinirEstrategias(jogadores);
            _jogo.RealizarApostas(jogadores);
            _jogo.DistribuirCartas(jogadores);

            AtualizarMesa(jogadores);

            JogarTurnosComAtualizacao(jogadores);

            JogarDealerComAtualizacao(jogadores);

            AtualizarMesa(jogadores, false);

            _jogo.AvaliarVencedores(jogadores);

            jogadores.ForEach(j => j.Resetar());
        }

        MesaView.MostrarFimDeJogo(rodadas);
    }

    private void AtualizarMesa(List<Jogador> jogadores, bool ocultarSegundaCartaDealer = true)
    {
        var estadoMesa = _jogo.ObterEstadoMesa(jogadores, ocultarSegundaCartaDealer);
        Console.Clear();
        foreach (var (jogador, ocultarSegunda) in estadoMesa)
        {
            MesaView.MostrarCartas(jogador, ocultarSegunda);
        }
    }

    private void JogarTurnosComAtualizacao(List<Jogador> jogadores)
    {
        bool continuar;
        do
        {
            continuar = false;
            for (var i = jogadores.Count - 1; i >= 1; i--)
            {
                var jogador = jogadores[i];
                jogador.ProximoTurno();
                var acao = jogador.Jogar();

                switch (acao)
                {
                    case Enums.Acao.Puxar:
                        jogador.Cartas.Add(_jogo.PuxarCarta());
                        break;
                    case Enums.Acao.DoubleDown:
                        jogador.DiminuirPontosAposta();
                        jogador.DobrarAposta();
                        jogador.Cartas.Add(_jogo.PuxarCarta());
                        jogador.SetStrategy(new JogadorManterStrategy());
                        break;
                    case Enums.Acao.Manter:
                        jogador.SetStrategy(new JogadorManterStrategy());
                        break;
                }

                AtualizarMesa(jogadores);

                if (acao != Enums.Acao.Manter)
                    continuar = true;
            }
        } while (continuar);
    }

    private void JogarDealerComAtualizacao(List<Jogador> jogadores)
    {
        var dealer = jogadores[0];
        while (true)
        {
            Thread.Sleep(1000);
            var acao = dealer.Jogar();

            if (acao == Enums.Acao.Puxar)
                dealer.Cartas.Add(_jogo.PuxarCarta());
            else
                break;

            AtualizarMesa(jogadores, false);
        }
    }
}