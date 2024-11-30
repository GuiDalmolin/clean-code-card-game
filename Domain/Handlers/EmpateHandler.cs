using Domain.Dtos;

namespace Domain.Handlers
{
    public class EmpateHandler : AbstractHandler
    {
        public override void Processar(DefinirVencedoresDto request)
        {
            if (request.PontuacaoJogador == request.PontuacaoDealer)
            {
                Console.WriteLine($"{request.Jogador.Nome} empatou e recuperou sua aposta.");
                request.Jogador.Pontuacao += request.Jogador.Aposta;
            }
            else
            {
                base.Processar(request);
            }
        }
    }
}