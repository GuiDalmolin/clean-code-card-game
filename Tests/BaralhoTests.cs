using Domain.Entities;
using Xunit;

namespace Tests
{
    public class BaralhoTests
    {
        [Fact]
        public void Baralho_DeveConter_52_Cartas_AoIniciar()
        {
            // Arrange & Act
            var baralho = new Baralho();

            // Assert
            Assert.Equal(52, baralho.Cartas.Count);
        }

        [Fact]
        public void Embaralhar_DeveMudarOrdem_DasCartas()
        {
            // Arrange
            var baralho = new Baralho();
            var ordemInicial = baralho.Cartas.ToList();

            // Act
            baralho.GetType().GetMethod("Embaralhar", System.Reflection.BindingFlags.NonPublic |
                System.Reflection.BindingFlags.Instance)?.Invoke(baralho, null);

            // Assert
            Assert.NotEqual(ordemInicial, baralho.Cartas);
        }

        [Fact]
        public void CriarIterator_DeveRetornar_IteradorValido()
        {
            // Arrange
            var baralho = new Baralho();

            // Act
            var iterator = baralho.CriarIterator();

            // Assert
            Assert.NotNull(iterator);
        }
    }
}