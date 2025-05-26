using Domain.Dtos;

namespace Domain.Handlers
{
    public class DerrotaHandler : AbstractHandler
    {
        public override void Processar(DefinirVencedoresDto request)
        {
            Console.WriteLine($"{request.Jogador.Nome} perdeu.");
            request.Jogador.ReduzirVitorias();
        }
    }
}