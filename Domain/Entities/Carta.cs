using Domain.Resources;

namespace Domain.Entities
{
    public class Carta
    {
        public required Enums.Numero Numero { get; set; }
        public required Enums.Naipe Naipe { get; set; }

        public static int GetValorTotal(List<Carta> cartas)
        {
            var soma = 0;

            var cartasOrdenadas = cartas.OrderByDescending(o => o.Numero);

            foreach (var carta in cartasOrdenadas)
            {
                soma += carta.GetValor(soma);
            }

            return soma;
        }

        private int GetValor(int total)
        {
            if (Numero == Enums.Numero.As)
            {
                return (total + 11 > 21) ? 1 : 11;
            }

            return Numero switch
            {
                Enums.Numero.Dois => 2,
                Enums.Numero.Tres => 3,
                Enums.Numero.Quatro => 4,
                Enums.Numero.Cinco => 5,
                Enums.Numero.Seis => 6,
                Enums.Numero.Sete => 7,
                Enums.Numero.Oito => 8,
                Enums.Numero.Nove => 9,
                Enums.Numero.Dez or Enums.Numero.Valete or Enums.Numero.Dama or Enums.Numero.Rei => 10,
                _ => throw new InvalidOperationException("Número da carta inválido.")
            };
        }

        public string GetNumero()
        {
            return Numero switch
            {
                Enums.Numero.As => "As",
                Enums.Numero.Dois => "2",
                Enums.Numero.Tres => "3",
                Enums.Numero.Quatro => "4",
                Enums.Numero.Cinco => "5",
                Enums.Numero.Seis => "6",
                Enums.Numero.Sete => "7",
                Enums.Numero.Oito => "8",
                Enums.Numero.Nove => "9",
                Enums.Numero.Dez => "10",
                Enums.Numero.Valete => "J",
                Enums.Numero.Dama => "Q",
                Enums.Numero.Rei => "K",
                _ => "  ",
            };
        }

        public string GetNaipe()
        {
            return Naipe switch
            {
                Enums.Naipe.Ouro => "♦️",
                Enums.Naipe.Espada => "♠️",
                Enums.Naipe.Copas => "♥️",
                Enums.Naipe.Paus => "♣️",
                _ => " ",
            };
        }

        public List<string> ObterLinhasCarta()
        {
            var numero = GetNumero();
            var naipe = GetNaipe();

            var linhas = new List<string>
            {
                "┌─────────┐",
                $"│{numero,2}       │",
                "│         │",
                $"│    {naipe}   │",
                "│         │",
                $"│       {numero,-2}│",
                "└─────────┘"
            };

            return linhas;
        }

        public List<string> ObterLinhasCartaVirada()
        {
            return
            [
                "┌─────────┐",
                "│X X X X X│",
                "│ X X X X │",
                "│X X X X X│",
                "│ X X X X │",
                "│X X X X X│",
                "└─────────┘"
            ];
        }
    }
}