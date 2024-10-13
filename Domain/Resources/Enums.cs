namespace Domain.Resources
{
    public class Enums
    {
        public enum Naipe
        {
            Ouro,
            Espada,
            Copas,
            Paus
        }

        public enum Numero
        {
            As,
            Dois,
            Tres,
            Quatro,
            Cinco,
            Seis,
            Sete,
            Oito,
            Nove,
            Dez,
            Valete,
            Dama,
            Rei
        }

        public enum Acao
        {
            Manter,
            Puxar
        }

        public enum TipoJogador
        {
            Dealer,
            Pessoa,
            Computador,
        }
    }
}