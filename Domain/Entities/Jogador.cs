using Domain.Interfaces.Strategy;
using Domain.Resources;

namespace Domain.Entities;

public abstract class Jogador(string nome, IJogadorStrategy jogadorStrategy)
{
    public string Nome { get; } = nome;
    public List<Carta> Cartas { get; private set; } = [];
    public int Pontuacao { get; private set; } = 100;
    public int Aposta { get; private set; }
    public int Vitorias { get; private set; }
    public int Turno { get; private set; }

    private IJogadorStrategy _jogadorStrategy = jogadorStrategy;

    public void SetStrategy(IJogadorStrategy strategy)
    {
        _jogadorStrategy = strategy;
    }

    public Enums.Acao Jogar()
    {
        return _jogadorStrategy.RealizarJogada(this);
    }

    public void Apostar()
    {
        Aposta = _jogadorStrategy.RealizarAposta(this);
    }

    public void DiminuirPontosAposta()
    {
        Pontuacao -= Aposta;
    }

    public void AumentarPontosAposta()
    {
        Pontuacao += Aposta;
    }

    public void DobrarAposta()
    {
        Aposta *= 2;
    }

    public void ProximoTurno()
    {
        Turno++;
    }

    public void ReduzirVitorias()
    {
        Vitorias--;
    }

    public void AumentarVitorias()
    {
        Vitorias++;
    }

    public void Resetar()
    {
        Turno = 0;
        Cartas.Clear();
    }
}