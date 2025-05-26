using Domain.Entities;

namespace Presentation.Views;

public static class MesaView
{
    public static void MostrarCartas(Jogador jogador, bool ocultarSegunda = false)
    {
        var linhasCartas = new List<List<string>>();

        for (var i = 0; i < jogador.Cartas.Count; i++)
        {
            if (ocultarSegunda && i == 1)
                linhasCartas.Add(Carta.ObterLinhasCartaVirada());
            else
                linhasCartas.Add(jogador.Cartas[i].ObterLinhasCarta());
        }

        var fichas = jogador.Pontuacao + jogador.Aposta;
        var cabecalho = jogador.Nome.Equals("dealer", StringComparison.CurrentCultureIgnoreCase)
            ? $"Cartas do {jogador.Nome}:"
            : $"Cartas do {jogador.Nome} (fichas: {fichas}) (aposta: {jogador.Aposta}):";

        Console.WriteLine(cabecalho);

        for (var linha = 0; linha < 7; linha++)
        {
            var partes = linhasCartas.SelectMany(carta => carta[linha]);
            Console.WriteLine(string.Join(" ", partes));
        }

        Console.WriteLine();
    }

    public static void MostrarFimDeJogo(int rodadas)
    {
        Console.Clear();
        Console.WriteLine("Fim de jogo!");
        Console.WriteLine($"Rodadas jogadas: {rodadas}");
    }
}