using Domain.Entities;
using Domain.Resources;
using Xunit;
using Assert = Xunit.Assert;

namespace Tests.Domain;

public class BaralhoTests
{
    [Fact]
    public void Baralho_DeveConter_52_Cartas_AoIniciar()
    {
        // Arrange & Act
        var baralho = new Baralho();

        // Assert
        Assert.Equal(52, baralho.CriarIterator().Restantes);
    }

    [Fact]
    public void Baralho_DeveConter_TodosOsNaipesENumeros()
    {
        // Arrange
        var baralho = new Baralho();
        var iterator = baralho.CriarIterator();
        var cartas = new List<Carta>();

        // Act
        while (iterator.HasNext())
            cartas.Add(iterator.Next());

        // Assert
        foreach (var naipe in Enum.GetValues<Enums.Naipe>())
            foreach (var numero in Enum.GetValues<Enums.Numero>())
                Assert.Contains(cartas, c => c.Naipe == naipe && c.Numero == numero);
    }

    [Fact]
    public void Embaralhar_DeveMudarOrdem_DasCartas()
    {
        // Arrange
        var baralho = new Baralho();
        var copia = baralho.CriarIterator();

        // Act
        var novoBaralho = new Baralho();
        var novaOrdem = novoBaralho.CriarIterator();

        // Assert
        Assert.NotEqual(copia, novaOrdem);
    }

    [Fact]
    public void CriarIterator_DevePermitirIteracaoCompleta()
    {
        // Arrange
        var baralho = new Baralho();
        var iterator = baralho.CriarIterator();
        var contador = 0;

        // Act
        while (iterator.HasNext())
        {
            var carta = iterator.Next();
            Assert.NotNull(carta);
            contador++;
        }

        // Assert
        Assert.Equal(52, contador);
        Assert.False(iterator.HasNext());
    }

    [Fact]
    public void CriarIterator_AposFinal_RetornaMesmaInstancia()
    {
        // Arrange
        var baralho = new Baralho();
        var iterator = baralho.CriarIterator();

        // Act
        while (iterator.HasNext())
            iterator.Next();

        var restanteAntes = iterator.Restantes;

        // Assert
        Assert.Equal(0, restanteAntes);
        Assert.False(iterator.HasNext());
    }
}