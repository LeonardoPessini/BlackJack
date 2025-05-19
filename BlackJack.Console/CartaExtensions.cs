using BlackJack.Domain.Cartas;

namespace BlackJack.Console;

internal static class CartaExtensions
{
    internal static CartaView ToConsole(this Carta carta)
    {
        var valor = carta.Valor switch
        {
            ValorCarta.As => Simbolo.As,
            ValorCarta.Dois => Simbolo.Dois,
            ValorCarta.Tres => Simbolo.Tres,
            ValorCarta.Quatro => Simbolo.Quatro,
            ValorCarta.Cinco => Simbolo.Cinco,
            ValorCarta.Seis => Simbolo.Seis,
            ValorCarta.Sete => Simbolo.Sete,
            ValorCarta.Oito => Simbolo.Oito,
            ValorCarta.Nove => Simbolo.Nove,
            ValorCarta.Dez => Simbolo.Dez,
            ValorCarta.Valete => Simbolo.Valete,
            ValorCarta.Dama => Simbolo.Dama,
            ValorCarta.Rei => Simbolo.Rei,
            _ => null
        };

        var naipe = carta.Naipe switch
        {
            Naipe.Paus => Simbolo.Paus,
            Naipe.Copa => Simbolo.Copa,
            Naipe.Ouro => Simbolo.Ouro,
            Naipe.Espadas => Simbolo.Espadas,
            _ => null
        };

        return new CartaView(valor, naipe);
    }

}
