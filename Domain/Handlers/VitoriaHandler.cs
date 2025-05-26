using Domain.Dtos;

namespace Domain.Handlers
{
    public class VitoriaHandler : AbstractHandler
    {
        public override void Processar(DefinirVencedoresDto request)
        {
            if (request.PontuacaoDealer > 21 || request.PontuacaoJogador > request.PontuacaoDealer)
            {
                request.Jogador.DobrarAposta();
                request.Jogador.AumentarPontosAposta();
                request.Jogador.AumentarVitorias();
                Console.WriteLine($"{request.Jogador.Nome} ganhou! Pontos: {request.Jogador.Pontuacao}");
            }
            else
            {
                base.Processar(request);
            }
        }
    }
}