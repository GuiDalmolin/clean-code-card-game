using Domain.Entities;
using Domain.Interfaces.Strategy;
using Domain.Resources;
using Domain.Strategies;
using Moq;
using Xunit;

namespace Tests
{
    public class JogadorTests
    {
        [Fact]
        public void RealizarJogada_DeveRetornarPuxar_QuandoInputForS()
        {
            // Arrange: Simulamos a entrada "s"
            var strategy = new HumanoStrategy(() => "s");

            // Act
            var resultado = strategy.RealizarJogada(new Humano(""));

            // Assert
            Assert.Equal(Enums.Acao.Puxar, resultado);
        }

        [Fact]
        public void RealizarJogada_DeveRetornarManter_QuandoInputForN()
        {
            // Arrange: Simulamos a entrada "n"
            var strategy = new HumanoStrategy(() => "n");

            // Act
            var resultado = strategy.RealizarJogada(new Humano(""));

            // Assert
            Assert.Equal(Enums.Acao.Manter, resultado);
        }

        [Fact]
        public void RealizarJogada_DeveRetornarManter_QuandoInputForInvalido()
        {
            // Arrange: Simulamos uma entrada inválida
            var strategy = new HumanoStrategy(() => "x");

            // Act
            var resultado = strategy.RealizarJogada(new Humano(""));

            // Assert
            Assert.Equal(Enums.Acao.Manter, resultado);
        }

        [Fact]
        public void Dealer_DeveUsarDealerStrategy_PorPadrao()
        {
            // Arrange
            var dealer = new Dealer("Dealer");

            // Act
            var acao = dealer.Jogar();

            // Assert
            Assert.Equal(Enums.Acao.Puxar, acao); // Ação esperada do Dealer
        }

        [Fact]
        public void Jogador_DevePermitirAlterarEstrategia()
        {
            // Arrange
            var jogador = new Computador("CPU");
            var mockStrategy = new Mock<IJogadorStrategy>();
            mockStrategy.Setup(s => s.RealizarJogada(It.IsAny<Jogador>()))
                        .Returns(Enums.Acao.Manter);

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
            mockStrategy.Setup(s => s.RealizarJogada(It.IsAny<Jogador>()))
                        .Returns(Enums.Acao.Puxar);
            var jogador = new Computador("CPU");
            jogador.SetStrategy(mockStrategy.Object);

            // Act
            jogador.Jogar();

            // Assert
            mockStrategy.Verify(s => s.RealizarJogada(It.IsAny<Jogador>()), Times.Once);
        }

        [Fact]
        public void ComputadorStrategy_DeveRetornarPuxarOuManter()
        {
            // Arrange
            var strategy = new ComputadorStrategy();
            var resultados = new int[2]; // [0] para Puxar, [1] para Manter
            int numeroDeTestes = 1000; // Testar 1000 vezes

            // Act
            for (int i = 0; i < numeroDeTestes; i++)
            {
                var resultado = strategy.RealizarJogada(new Humano(""));
                if (resultado == Enums.Acao.Puxar) resultados[0]++;
                else resultados[1]++;
            }

            // Assert
            Assert.True(resultados[0] > 0 || resultados[1] > 0, "Deve retornar pelo menos uma ação.");
        }

        [Fact]
        public void DealerStrategy_DevePuxarQuandoValorTotalMenorQue17()
        {
            // Arrange
            var dealerStrategy = new DealerStrategy();
            var cartas = new List<Carta>
            {
                new Carta { Numero = Enums.Numero.Dois, Naipe = Enums.Naipe.Ouro },
                new Carta { Numero = Enums.Numero.As, Naipe = Enums.Naipe.Copas }
            };

            // Act
            var dealer = new Dealer("");
            dealer.Cartas = cartas;
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
                new Carta { Numero = Enums.Numero.Dez, Naipe = Enums.Naipe.Ouro },
                new Carta { Numero = Enums.Numero.Sete, Naipe = Enums.Naipe.Copas }
            };

            // Act
            var dealer = new Dealer("");
            dealer.Cartas = cartas;
            var acao = dealerStrategy.RealizarJogada(dealer);

            // Assert
            Assert.Equal(Enums.Acao.Manter, acao);
        }

        [Fact]
        public void JogadorManterStrategy_DeveSempreRetornarManter()
        {
            // Arrange
            var estrategia = new JogadorManterStrategy();

            // Act
            var resultado = estrategia.RealizarJogada(new Humano(""));

            // Assert
            Assert.Equal(Enums.Acao.Manter, resultado);
        }
    }
}