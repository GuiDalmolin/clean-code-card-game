using Domain.Dtos;

namespace Domain.Handlers
{
    public class VitoriaHandler : AbstractHandler
    {
        public override void Processar(DefinirVencedoresDto request)
        {
            if (request.PontuacaoDealer > 21 || request.PontuacaoJogador > request.PontuacaoDealer)
            {
                Console.WriteLine($"{request.Jogador.Nome} ganhou!");
                request.Jogador.Pontuacao += request.Jogador.Aposta * 2;
                request.Jogador.Vitorias++;
            }
            else
            {
                base.Processar(request);
            }
        }
    }
}