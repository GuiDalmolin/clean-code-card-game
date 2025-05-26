using Application.Interfaces;
using Domain.Dtos;
using Domain.Entities;
using Domain.Factories;
using Domain.Handlers;
using Domain.Interfaces.Iterator;
using Domain.Resources;
using Domain.Strategies;

namespace Application.Services;

public class GameService : IGameService
{
    private readonly Func<string> _inputProvider;
    private IBaralhoIterator? _iterator;

    public GameService(Func<string>? inputProvider = null)
    {
        _inputProvider = inputProvider ?? (() => Console.ReadLine() ?? string.Empty);
    }

    public void ConfigurarConsole() =>
        Console.OutputEncoding = System.Text.Encoding.UTF8;

    public List<Jogador> CriarJogadores()
    {
        var factory = new JogadorFactory();
        return
        [
            factory.CriarJogador("dealer", Enums.TipoJogador.Dealer),
            factory.CriarJogador("cpu 1", Enums.TipoJogador.Computador),
            factory.CriarJogador("cpu 2", Enums.TipoJogador.Computador),
            factory.CriarJogador("você", Enums.TipoJogador.Pessoa)
        ];
    }

    public bool JogadorHumanoTemFichas(List<Jogador>? jogadores)
    {
        if (jogadores is not { Count: > 3 })
            return false;
        return jogadores[3].Pontuacao > 0;
    }

    public void DefinirEstrategias(List<Jogador>? jogadores)
    {
        if (jogadores is not { Count: >= 4 }) return;

        jogadores[0].SetStrategy(new DealerStrategy());
        jogadores[1].SetStrategy(new ComputadorStrategy());
        jogadores[2].SetStrategy(new ComputadorStrategy());
        jogadores[3].SetStrategy(new HumanoStrategy(_inputProvider));
    }

    public void RealizarApostas(List<Jogador> jogadores)
    {
        foreach (var jogador in jogadores)
        {
            jogador.Apostar();
            jogador.DiminuirPontosAposta();
        }
    }

    public void DistribuirCartas(List<Jogador> jogadores)
    {
        var baralho = new Baralho();
        _iterator = baralho.CriarIterator();

        foreach (var jogador in jogadores)
            for (int i = 0; i < 2; i++)
                jogador.Cartas.Add(_iterator.Next());
    }

    public void AvaliarVencedores(List<Jogador>? jogadores)
    {
        if (jogadores is not { Count: not 0 }) return;

        var dealer = jogadores[0];
        var pontuacoes = jogadores.ToDictionary(j => j.Nome, j => Carta.GetValorTotal(j.Cartas));
        var dealerPts = pontuacoes[dealer.Nome];

        var chain = CriarCadeiaDeResponsabilidade();

        foreach (var jogador in jogadores.Skip(1))
        {
            var pts = pontuacoes[jogador.Nome];
            var dto = new DefinirVencedoresDto(jogador, pts, dealerPts);
            chain.Processar(dto);
        }
    }

    public List<(Jogador jogador, bool ocultarSegundaCarta)> ObterEstadoMesa(List<Jogador> jogadores, bool ocultarSegundaCartaDealer = true)
    {
        return jogadores.Select(j =>
                (j, j.Nome.Equals("dealer",
                        StringComparison.CurrentCultureIgnoreCase) &&
                    ocultarSegundaCartaDealer)
            )
            .ToList();
    }

    public Carta PuxarCarta()
    {
        if (_iterator == null)
            throw new InvalidOperationException("Baralho não inicializado.");

        return _iterator.Next();
    }

    private static EstouroHandler CriarCadeiaDeResponsabilidade()
    {
        var estouro = new EstouroHandler();
        var vitoria = new VitoriaHandler();
        var empate = new EmpateHandler();
        var derrota = new DerrotaHandler();

        estouro.DefinirProximo(vitoria);
        vitoria.DefinirProximo(empate);
        empate.DefinirProximo(derrota);

        return estouro;
    }
}