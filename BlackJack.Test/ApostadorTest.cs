using BlackJack.Domain;
using BlackJack.Domain.Apostadores;
using BlackJack.Domain.Cartas;

namespace BlackJack.Test;

public class ApostadorTest

{
    private Apostador _apostador;
    public ApostadorTest()
    {
        _apostador = new Apostador();
    }


    [Fact]
    public void DeveCriarMaoSemCartas()
    {
        Assert.Empty(_apostador.Mao);
    }

    [Fact]
    public void DeveInserirCartaNaMao()
    {
        var carta = new Carta(ValorCarta.Dama, Naipe.Copa);

        _apostador.ReceberCarta(carta);

        Assert.Equal(carta, _apostador.Mao.First());
    }


    [Theory]
    [InlineData(ValorCarta.Valete, ValorCarta.Dama, null, 20)]
    [InlineData(ValorCarta.As, ValorCarta.As, null, 12)]
    [InlineData(ValorCarta.Oito, ValorCarta.Quatro, ValorCarta.Dois, 14)]
    [InlineData(ValorCarta.As, ValorCarta.Dama, null, 21)]
    [InlineData(ValorCarta.Rei, ValorCarta.As, ValorCarta.As, 12)]
    [InlineData(ValorCarta.Cinco, ValorCarta.As, null, 16)]
    [InlineData(ValorCarta.Quatro, ValorCarta.As, ValorCarta.As, 16)]
    [InlineData(ValorCarta.Valete, ValorCarta.Dama, ValorCarta.Rei, 30)]
    [InlineData(ValorCarta.As, ValorCarta.As, ValorCarta.As, 13)]
    [InlineData(ValorCarta.Dez, ValorCarta.Dez, ValorCarta.As, 21)]
    internal void DeveSomarMaoCorretamente(ValorCarta valor1, ValorCarta valor2, ValorCarta? valor3, int resultadoEsperado)
    {
        _apostador.ReceberCarta(new Carta(valor1, Naipe.Copa));
        _apostador.ReceberCarta(new Carta(valor2, Naipe.Paus));

        if(valor3 != null)
            _apostador.ReceberCarta(new Carta(valor3.Value, Naipe.Copa));

        Assert.Equal(resultadoEsperado, _apostador.SomaDosValoresDasCartas);
    }
}
