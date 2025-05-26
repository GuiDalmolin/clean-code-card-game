using Domain.Dtos;

namespace Domain.Handlers
{
    public class EstouroHandler : AbstractHandler
    {
        public override void Processar(DefinirVencedoresDto request)
        {
            if (request.PontuacaoJogador > 21)
            {
                request.Jogador.ReduzirVitorias();
                Console.WriteLine($"{request.Jogador.Nome} perdeu (estourou).");
            }
            else
            {
                base.Processar(request);
            }
        }
    }
}