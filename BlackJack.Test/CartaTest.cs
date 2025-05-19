using BlackJack.Domain.Cartas;

namespace BlackJack.Test;

public class CartaTest
{
    [Theory]
    [InlineData(ValorCarta.Dois,   2)]
    [InlineData(ValorCarta.Tres,   3)]
    [InlineData(ValorCarta.Quatro, 4)]
    [InlineData(ValorCarta.Cinco,  5)]
    [InlineData(ValorCarta.Seis,   6)]
    [InlineData(ValorCarta.Sete,   7)]
    [InlineData(ValorCarta.Oito,   8)]
    [InlineData(ValorCarta.Nove,   9)]
    [InlineData(ValorCarta.Dez,   10)]
    [InlineData(ValorCarta.Valete,10)]
    [InlineData(ValorCarta.Dama,  10)]
    [InlineData(ValorCarta.Rei,   10)]
    [InlineData(ValorCarta.As,     1)]
    internal void AlterarValorDaCartaParaBlackJack(ValorCarta valor, int valorEsperado)
    {
        var carta = new Carta(valor, Naipe.Ouro);

        Assert.Equal(valorEsperado, carta.ValorParaBlackJack());
    }
}
