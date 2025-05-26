using Domain.Dtos;

namespace Domain.Handlers
{
    public class EmpateHandler : AbstractHandler
    {
        public override void Processar(DefinirVencedoresDto request)
        {
            if (request.PontuacaoJogador == request.PontuacaoDealer)
            {
                request.Jogador.AumentarPontosAposta();
                Console.WriteLine($"{request.Jogador.Nome} empatou e recuperou sua aposta.");
            }
            else
            {
                base.Processar(request);
            }
        }
    }
}