using Domain.Entities;
using Domain.Factories;
using Domain.Interfaces.Iterator;
using Domain.Resources;
using Domain.Strategies;
using System.Text;

namespace Presentation.Views
{
    public static class Mesa
    {
        public static void Main()
        {
            Configurar();

            var (baralho, iterator) = ArrangeMesa();
            var jogadores = ArrangeJogadores();

            jogadores.ForEach(j => j.Cartas.Distribuir(iterator, 2));

            JogarRodadas(jogadores, iterator);

            JogarRodadasDealer(jogadores, iterator);

            DefinirVencedores(CalcularPontuacoes(jogadores));
        }

        private static void Configurar()
        {
            Console.OutputEncoding = Encoding.UTF8;
        }

        private static (Baralho baralho, IBaralhoIterator iterator) ArrangeMesa()
        {
            var baralho = new Baralho();
            return (baralho, baralho.CriarIterator());
        }

        private static List<Jogador> ArrangeJogadores()
        {
            var factory = new JogadorFactory();
            return
            [
                factory.CriarJogador("dealer", Enums.TipoJogador.Dealer),
                factory.CriarJogador("computador 1", Enums.TipoJogador.Computador),
                factory.CriarJogador("computador 2", Enums.TipoJogador.Computador),
                factory.CriarJogador("jogador", Enums.TipoJogador.Pessoa),
            ];
        }

        private static void JogarRodadas(List<Jogador> jogadores, IBaralhoIterator iterator)
        {
            bool continua;

            do
            {
                Console.Clear();
                ExibirCartas(jogadores);

                continua = jogadores.Skip(1).Reverse().Any(j => !RealizarAcao(j, iterator));
            } while (continua);
        }

        private static void JogarRodadasDealer(List<Jogador> jogadores, IBaralhoIterator iterator)
        {
            var dealer = jogadores[0];
            bool continua;
            do
            {
                Console.Clear();
                RevelarCartas(jogadores);

                continua = !RealizarAcao(dealer, iterator);

                Thread.Sleep(1000);

            } while (continua);
        }

        private static void ExibirCartas(List<Jogador> jogadores)
        {
            ExibirCartasComFormato(jogadores[0], ocultarSegundaCarta: true);
            jogadores.Skip(1).ToList().ForEach(j => ExibirCartasComFormato(j));
        }

        private static void RevelarCartas(List<Jogador> jogadores)
        {
            jogadores.ForEach(j => ExibirCartasComFormato(j));
        }

        private static Dictionary<string, int> CalcularPontuacoes(List<Jogador> jogadores) =>
            jogadores.ToDictionary(j => j.Nome, j => Carta.GetValorTotal(j.Cartas));

        private static void DefinirVencedores(Dictionary<string, int> pontuacoes)
        {
            var maiorPontuacao = pontuacoes.Values.Where(p => p <= 21).DefaultIfEmpty(0).Max();
            var vencedores = pontuacoes.Where(p => p.Value == maiorPontuacao).Select(p => p.Key).ToList();

            Console.WriteLine(vencedores.Count == 1
                ? $"O vencedor é: {vencedores[0]}!!!"
                : $"Os vencedores são: {string.Join(", ", vencedores)}!!!");

            Console.WriteLine("------------------");
            foreach (var item in pontuacoes)
            {
                Console.WriteLine($"{item.Key} - {item.Value}");
            }
        }

        private static void ExibirCartasComFormato(Jogador jogador, bool ocultarSegundaCarta = false)
        {
            var linhas = ocultarSegundaCarta
                ?
                [
            jogador.Cartas[0].ObterLinhasCarta(),
            jogador.Cartas[0].ObterLinhasCartaVirada()
                ]
                : jogador.Cartas.Select(c => c.ObterLinhasCarta()).ToList();

            Console.WriteLine($"Cartas de {jogador.Nome}:");
            for (int i = 0; i < 7; i++)
                Console.WriteLine(string.Join(" ", linhas.Select(l => l[i])));
        }

        private static bool RealizarAcao(Jogador jogador, IBaralhoIterator iterator)
        {
            var acao = jogador.Jogar();
            if (acao == Enums.Acao.Puxar)
                jogador.Cartas.Add(PuxarCarta(iterator));
            if (acao == Enums.Acao.Manter)
                jogador.SetStrategy(new JogadorManterStrategy());

            return acao == Enums.Acao.Manter;
        }

        private static Carta PuxarCarta(IBaralhoIterator iterator)
        {
            return iterator.Next();
        }

        private static void Distribuir(this List<Carta> cartas, IBaralhoIterator iterator, int quantidade)
        {
            for (int i = 0; i < quantidade; i++)
                cartas.Add(iterator.Next());
        }
    }
}