using Domain.Dtos;

namespace Domain.Handlers
{
    public class EstouroHandler : AbstractHandler
    {
        public override void Processar(DefinirVencedoresDto request)
        {
            if (request.PontuacaoJogador > 21)
            {
                Console.WriteLine($"{request.Jogador.Nome} perdeu (estourou).");
                request.Jogador.Vitorias--;
            }
            else
            {
                base.Processar(request);
            }
        }
    }
}