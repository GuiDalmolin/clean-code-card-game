using Domain.Entities;
using Domain.Resources;
using Xunit;

namespace Tests
{
    public class CartaTests
    {
        [Fact]
        public void GetValorTotal_DeveRetornar_ValorCorretoComAsComoOnze()
        {
            // Arrange
            var cartas = new List<Carta>
            {
                new Carta { Numero = Enums.Numero.As, Naipe = Enums.Naipe.Copas },
                new Carta { Numero = Enums.Numero.Oito, Naipe = Enums.Naipe.Ouro }
            };

            // Act
            var valorTotal = Carta.GetValorTotal(cartas);

            // Assert
            Assert.Equal(19, valorTotal);  // Ás como 11 + 8
        }

        [Fact]
        public void GetValorTotal_DeveRetornar_ValorCorretoComAsComoUm()
        {
            // Arrange
            var cartas = new List<Carta>
            {
                new Carta { Numero = Enums.Numero.As, Naipe = Enums.Naipe.Copas },
                new Carta { Numero = Enums.Numero.Dez, Naipe = Enums.Naipe.Ouro },
                new Carta { Numero = Enums.Numero.Valete, Naipe = Enums.Naipe.Espada }
            };

            // Act
            var valorTotal = Carta.GetValorTotal(cartas);

            // Assert
            Assert.Equal(21, valorTotal);  // Ás como 1 + 10 + 10
        }

        [Fact]
        public void GetNumero_DeveRetornar_SimboloCorreto()
        {
            // Arrange
            var carta = new Carta { Numero = Enums.Numero.As, Naipe = Enums.Naipe.Copas };

            // Act
            var numero = carta.GetNumero();

            // Assert
            Assert.Equal("As", numero);
        }

        [Fact]
        public void GetNaipe_DeveRetornar_SimboloCorreto()
        {
            // Arrange
            var carta = new Carta { Numero = Enums.Numero.Dez, Naipe = Enums.Naipe.Espada };

            // Act
            var naipe = carta.GetNaipe();

            // Assert
            Assert.Equal("♠️", naipe);
        }
    }
}