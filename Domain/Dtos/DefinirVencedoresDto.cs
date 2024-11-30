using Domain.Entities;

namespace Domain.Dtos
{
    public class DefinirVencedoresDto
    {
        public DefinirVencedoresDto(Jogador jogador, int pontuacaoJogador, int pontuacaoDealer)
        {
            Jogador = jogador;
            PontuacaoJogador = pontuacaoJogador;
            PontuacaoDealer = pontuacaoDealer;
        }

        public Jogador Jogador { get; set; }
        public int PontuacaoJogador { get; set; }
        public int PontuacaoDealer { get; set; }
    }
}