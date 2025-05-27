using Domain.Entities;
using Domain.Resources;
using Xunit;
using Assert = Xunit.Assert;

namespace Tests.Domain;

public class CartaTests
{
    [Fact]
    public void GetValorTotal_DeveRetornar_ValorCorretoComAsComoOnze()
    {
        // Arrange
        var cartas = new List<Carta>
        {
            new() { Numero = Enums.Numero.As, Naipe = Enums.Naipe.Copas },
            new() { Numero = Enums.Numero.Oito, Naipe = Enums.Naipe.Ouro }
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
            new() { Numero = Enums.Numero.As, Naipe = Enums.Naipe.Copas },
            new() { Numero = Enums.Numero.Dez, Naipe = Enums.Naipe.Ouro },
            new() { Numero = Enums.Numero.Valete, Naipe = Enums.Naipe.Espada }
        };

        // Act
        var valorTotal = Carta.GetValorTotal(cartas);

        // Assert
        Assert.Equal(21, valorTotal);  // Ás como 1 + 10 + 10
    }

    [Fact]
    public void ObterLinhasCarta_DeveRetornar_7LinhasComFormatacao()
    {
        // Arrange
        var carta = new Carta { Numero = Enums.Numero.Dama, Naipe = Enums.Naipe.Paus };

        // Act
        var linhas = carta.ObterLinhasCarta();

        // Assert
        Assert.Equal(7, linhas.Count);
        Assert.StartsWith("┌", linhas[0]);
        Assert.EndsWith("┘", linhas[^1]);
    }

    [Fact]
    public void ObterLinhasCartaVirada_DeveRetornar_7LinhasComPadraoX()
    {
        // Act
        var linhas = Carta.ObterLinhasCartaVirada();

        // Assert
        Assert.Equal(7, linhas.Count);
        Assert.Contains("X", linhas[1]);
    }
}