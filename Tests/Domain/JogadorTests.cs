using System.Reflection;
using Domain.Entities;
using Domain.Interfaces.Strategy;
using Domain.Resources;
using Domain.Strategies;
using Moq;
using Xunit;
using Assert = Xunit.Assert;

namespace Tests.Domain;

public class JogadorTests
{
    private static void SetPontuacao(Jogador jogador, int valor)
    {
        var prop = typeof(Jogador).GetProperty("Pontuacao", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
        prop?.SetValue(jogador, valor);
    }

    private static void SetCartas(Jogador jogador, List<Carta> cartas)
    {
        var prop = typeof(Jogador).GetProperty("Cartas", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
        prop?.SetValue(jogador, cartas);
    }

    private static void SetVitorias(Jogador jogador, int valor)
    {
        var prop = typeof(Jogador).GetProperty("Vitorias", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
        prop?.SetValue(jogador, valor);
    }

    [Fact]
    public void RealizarJogada_DeveRetornarPuxar_QuandoInputForS()
    {
        // Arrange
        var strategy = new HumanoStrategy(() => "s");
        var jogador = new Humano("");

        // Act
        var resultado = strategy.RealizarJogada(jogador);

        // Assert
        Assert.Equal(Enums.Acao.Puxar, resultado);
    }

    [Fact]
    public void RealizarJogada_DeveRetornarManter_QuandoInputForN()
    {
        // Arrange
        var strategy = new HumanoStrategy(() => "n");
        var jogador = new Humano("");

        // Act
        var resultado = strategy.RealizarJogada(jogador);

        // Assert
        Assert.Equal(Enums.Acao.Manter, resultado);
    }

    [Fact]
    public void RealizarJogada_DeveRetornarManter_QuandoInputForInvalido()
    {
        // Arrange
        var strategy = new HumanoStrategy(() => "x");
        var jogador = new Humano("");

        // Act
        var resultado = strategy.RealizarJogada(jogador);

        // Assert
        Assert.Equal(Enums.Acao.Manter, resultado);
    }

    [Fact]
    public void RealizarAposta_HumanoStrategy_DeveRetornarValorCorreto()
    {
        // Arrange
        var jogador = new Humano("");
        SetPontuacao(jogador, 10);

        var strategyValida = new HumanoStrategy(() => "5");
        var strategyZero = new HumanoStrategy(() => "0");
        var strategyMaior = new HumanoStrategy(() => "15");
        var strategyTexto = new HumanoStrategy(() => "abc");
        var strategyMenorQue10 = new HumanoStrategy(() => "7");
        var strategyMenorQuePontuacao = new HumanoStrategy(() => "3");

        // Act & Assert
        Assert.Equal(5, strategyValida.RealizarAposta(jogador));
        Assert.Equal(10, strategyZero.RealizarAposta(jogador));
        Assert.Equal(10, strategyMaior.RealizarAposta(jogador));
        Assert.Equal(10, strategyTexto.RealizarAposta(jogador));

        SetPontuacao(jogador, 5);
        Assert.Equal(5, strategyMenorQue10.RealizarAposta(jogador));
        Assert.Equal(3, strategyMenorQuePontuacao.RealizarAposta(jogador));
    }

    [Fact]
    public void DealerStrategy_DevePuxarQuandoValorTotalMenorQue17()
    {
        // Arrange
        var dealerStrategy = new DealerStrategy();
        var cartas = new List<Carta>
        {
            new() { Numero = Enums.Numero.Dois, Naipe = Enums.Naipe.Ouro },
            new() { Numero = Enums.Numero.As, Naipe = Enums.Naipe.Copas }
        };
        var dealer = new Dealer("");
        SetCartas(dealer, cartas);

        // Act
        var acao = dealerStrategy.RealizarJogada(dealer);

        // Assert
        Assert.Equal(Enums.Acao.Puxar, acao);
    }

    [Fact]
    public void DealerStrategy_DeveManterQuandoValorTotalIgualOuMaiorQue17()
    {
        // Arrange
        var dealerStrategy = new DealerStrategy();
        var cartas = new List<Carta>
        {
            new() { Numero = Enums.Numero.Dez, Naipe = Enums.Naipe.Ouro },
            new() { Numero = Enums.Numero.Sete, Naipe = Enums.Naipe.Copas }
        };
        var dealer = new Dealer("");
        SetCartas(dealer, cartas);

        // Act
        var acao = dealerStrategy.RealizarJogada(dealer);

        // Assert
        Assert.Equal(Enums.Acao.Manter, acao);
    }

    [Fact]
    public void JogadorManterStrategy_DeveSempreRetornarManter()
    {
        // Arrange
        var estrategia = new JogadorManterStrategy();
        var jogador = new Humano("");

        // Act
        var resultado = estrategia.RealizarJogada(jogador);

        // Assert
        Assert.Equal(Enums.Acao.Manter, resultado);
    }

    [Fact]
    public void ComputadorStrategy_DeveRetornarDiferentesAcoes()
    {
        // Arrange
        var strategy = new ComputadorStrategy();
        var resultados = new HashSet<Enums.Acao>();
        var jogador = new Humano("");
        SetCartas(jogador, [new Carta { Numero = Enums.Numero.Cinco, Naipe = Enums.Naipe.Copas }]);

        // Act
        for (var i = 0; i < 10; i++)
        {
            var resultado = strategy.RealizarJogada(jogador);
            resultados.Add(resultado);
            if (resultados.Count == 2) break;
        }

        // Assert
        Assert.Contains(Enums.Acao.Puxar, resultados);
        Assert.Contains(Enums.Acao.Manter, resultados);
    }

    [Fact]
    public void ComputadorStrategy_RealizarAposta_Valida()
    {
        // Arrange
        var strategy = new ComputadorStrategy();

        var jogador1 = new Humano("");
        SetPontuacao(jogador1, 80);
        SetVitorias(jogador1, 3);

        var jogador2 = new Humano("");
        SetPontuacao(jogador2, 80);
        SetVitorias(jogador2, 1);

        var jogador3 = new Humano("");
        SetPontuacao(jogador3, 50);
        SetVitorias(jogador3, 3);

        var jogador4 = new Humano("");
        SetPontuacao(jogador4, 30);
        SetVitorias(jogador4, 0);

        // Act
        var aposta1 = strategy.RealizarAposta(jogador1);
        var aposta2 = strategy.RealizarAposta(jogador2);
        var aposta3 = strategy.RealizarAposta(jogador3);
        var aposta4 = strategy.RealizarAposta(jogador4);

        // Assert
        Assert.InRange(aposta1, 20, 50);
        Assert.Equal(0, aposta1 % 10);

        Assert.InRange(aposta2, 10, 30);
        Assert.Equal(0, aposta2 % 10);

        Assert.InRange(aposta3, 10, 20);
        Assert.Equal(0, aposta3 % 10);

        Assert.Equal(30, aposta4);
    }

    [Fact]
    public void Jogador_DevePermitirAlterarEstrategia()
    {
        // Arrange
        var jogador = new Computador("CPU");
        var mockStrategy = new Mock<IJogadorStrategy>();
        mockStrategy.Setup(s => s.RealizarJogada(It.IsAny<Jogador>())).Returns(Enums.Acao.Manter);

        // Act
        jogador.SetStrategy(mockStrategy.Object);
        var acao = jogador.Jogar();

        // Assert
        Assert.Equal(Enums.Acao.Manter, acao);
    }

    [Fact]
    public void Jogar_DeveChamarRealizarJogadaNaStrategy()
    {
        // Arrange
        var mockStrategy = new Mock<IJogadorStrategy>();
        mockStrategy.Setup(s => s.RealizarJogada(It.IsAny<Jogador>())).Returns(Enums.Acao.Puxar);
        var jogador = new Computador("CPU");
        jogador.SetStrategy(mockStrategy.Object);

        // Act
        jogador.Jogar();

        // Assert
        mockStrategy.Verify(s => s.RealizarJogada(It.IsAny<Jogador>()), Times.Once);
    }

    [Fact]
    public void Dealer_DeveUsarDealerStrategy_PorPadrao()
    {
        // Arrange
        var dealer = new Dealer("Dealer");

        // Act
        var acao = dealer.Jogar();

        // Assert
        Assert.Equal(Enums.Acao.Puxar, acao);
    }
}