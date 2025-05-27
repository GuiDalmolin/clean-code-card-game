using Application.Services;
using Domain.Entities;
using Xunit;
using Assert = Xunit.Assert;

namespace Tests.Application;

public class GameServiceTests
{
    private readonly GameService _service;

    public GameServiceTests()
    {
        _service = new GameService();
        _service.ConfigurarConsole();
    }

    [Fact]
    public void CriarJogadores_DeveCriarJogadoresCorretamente()
    {
        // Arrange & Act
        var jogadores = _service.CriarJogadores();

        // Assert
        Assert.NotNull(jogadores);
        Assert.NotEmpty(jogadores);
        Assert.Contains(jogadores, j => j is Humano);
        Assert.Contains(jogadores, j => j is Dealer);
        Assert.Contains(jogadores, j => j is Computador);
    }

    [Fact]
    public void JogadorHumanoTemFichas_RetornaTrueSeHumanoTemFichas()
    {
        // Arrange
        var jogadores = _service.CriarJogadores();

        // Act
        var resultado = _service.JogadorHumanoTemFichas(jogadores);

        // Assert
        Assert.True(resultado);
    }

    [Fact]
    public void JogadorHumanoTemFichas_RetornaFalseSeNaoTemFichas()
    {
        // Arrange
        var jogadores = _service.CriarJogadores();
        foreach (var j in jogadores)
        {
            typeof(Jogador).GetProperty("Pontuacao")?.SetValue(j, 0);
        }

        // Act
        var resultado = _service.JogadorHumanoTemFichas(jogadores);

        // Assert
        Assert.False(resultado);
    }

    [Fact]
    public void DefinirEstrategias_AtribuiEstrategiasParaTodos()
    {
        // Arrange
        var fakeInput = new Queue<string>(["n"]);
        var service = new GameService(() => fakeInput.Dequeue());
        var jogadores = service.CriarJogadores();

        // Act
        service.DefinirEstrategias(jogadores);
        var acoes = jogadores.Select(jogador => jogador.Jogar()).ToList();

        // Assert
        foreach (var acao in acoes)
        {
            Assert.True(Enum.IsDefined(acao));
        }
    }

    [Fact]
    public void DefinirEstrategias_ComListaVazia_NaoLanca()
    {
        // Arrange
        var listaVazia = new List<Jogador>();

        // Act & Assert
        var exception = Record.Exception(() => _service.DefinirEstrategias(listaVazia));
        Assert.Null(exception);
    }

    [Fact]
    public void RealizarApostas_ApostasSaoRealizadas()
    {
        // Arrange
        var fakeInput = new Queue<string>(["10"]);
        var service = new GameService(() => fakeInput.Dequeue());
        var jogadores = service.CriarJogadores();
        service.DefinirEstrategias(jogadores);

        // Act
        _service.RealizarApostas(jogadores);

        // Assert
        foreach (var jogador in jogadores)
        {
            Assert.True(jogador.Aposta > 0);
        }
    }

    [Fact]
    public void RealizarApostas_ComListaVazia_NaoLanca()
    {
        // Arrange
        var listaVazia = new List<Jogador>();

        // Act & Assert
        var exception = Record.Exception(() => _service.RealizarApostas(listaVazia));
        Assert.Null(exception);
    }

    [Fact]
    public void DistribuirCartas_JogadoresRecebemCartas()
    {
        // Arrange
        var jogadores = _service.CriarJogadores();

        // Act
        _service.DistribuirCartas(jogadores);

        // Assert
        foreach (var jogador in jogadores)
        {
            Assert.NotEmpty(jogador.Cartas);
        }
    }

    [Fact]
    public void DistribuirCartas_ComListaVazia_NaoLanca()
    {
        // Arrange
        var listaVazia = new List<Jogador>();

        // Act & Assert
        var exception = Record.Exception(() => _service.DistribuirCartas(listaVazia));
        Assert.Null(exception);
    }

    [Fact]
    public void AvaliarVencedores_AvaliaSemErro()
    {
        // Arrange
        var jogadores = _service.CriarJogadores();
        _service.DistribuirCartas(jogadores);

        // Act & Assert
        var exception = Record.Exception(() => _service.AvaliarVencedores(jogadores));
        Assert.Null(exception);
    }

    [Fact]
    public void AvaliarVencedores_ComListaVazia_NaoLanca()
    {
        // Arrange
        var listaVazia = new List<Jogador>();

        // Act & Assert
        var exception = Record.Exception(() => _service.AvaliarVencedores(listaVazia));
        Assert.Null(exception);
    }

    [Fact]
    public void ObterEstadoMesa_RetornaEstadoCorreto()
    {
        // Arrange
        var jogadores = _service.CriarJogadores();

        // Act
        var estado = _service.ObterEstadoMesa(jogadores);

        // Assert
        Assert.NotNull(estado);
        Assert.All(estado, item =>
        {
            var (jogador, ocultarSegundaCarta) = item;
            Assert.NotNull(jogador);
            Assert.IsType<bool>(ocultarSegundaCarta);
        });
    }

    [Fact]
    public void ObterEstadoMesa_ComListaVazia_RetornaListaVazia()
    {
        // Arrange
        var listaVazia = new List<Jogador>();

        // Act
        var estado = _service.ObterEstadoMesa(listaVazia);

        // Assert
        Assert.NotNull(estado);
        Assert.Empty(estado);
    }

    [Fact]
    public void PuxarCarta_DevolveCartaValida()
    {
        // Arrange
        var jogadores = _service.CriarJogadores();
        _service.DistribuirCartas(jogadores);

        // Act
        var carta = _service.PuxarCarta();

        // Assert
        Assert.NotNull(carta);
        Assert.IsType<Carta>(carta);
    }
}