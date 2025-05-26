using Presentation.Controllers;

namespace Presentation;

public static class Program
{
    public static void Main()
    {
        var controller = new MesaController();
        controller.IniciarJogo();
    }
}