using System.Collections.Immutable;

namespace BlackJack.Domain.Cartas;

public record Carta(ValorCarta Valor, Naipe Naipe)
{
    private static readonly ImmutableDictionary<ValorCarta, int> _conversor =
        new Dictionary<ValorCarta, int>
    {
        { ValorCarta.As,      1 },
        { ValorCarta.Valete, 10 },
        { ValorCarta.Dama,   10 },
        { ValorCarta.Rei,    10 },
        { ValorCarta.Dez,    10 },
        { ValorCarta.Nove,    9 },
        { ValorCarta.Oito,    8 },
        { ValorCarta.Sete,    7 },
        { ValorCarta.Seis,    6 },
        { ValorCarta.Cinco,   5 },
        { ValorCarta.Quatro,  4 },
        { ValorCarta.Tres,    3 },
        { ValorCarta.Dois,    2 }

    }.ToImmutableDictionary();


    internal int ValorParaBlackJack()
    {
        return _conversor[Valor];
    }
}
