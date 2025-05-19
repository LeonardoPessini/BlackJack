using BlackJack.Domain.Cartas;

namespace BlackJack.Test;

public class BaralhoTest
{
    private readonly Baralho _baralho;
    private readonly List<Carta> _cartasCompradas;

    public BaralhoTest()
    {
        _baralho = new Baralho();
        _cartasCompradas = [];
    }

    [Fact]
    public void Comprar_DeveComprarCartasAleatorias()
    {
        for (int i = 0; i < 15; i++)
            _cartasCompradas.Add(_baralho.Comprar());

        Assert.True(_cartasCompradas.Select(c => c.Naipe)
                                    .Distinct()
                                    .Count() > 1);
    }

    [Fact]
    public void Comprar_NaoDeveComprarCartaRepetida()
    {
        for (int i = 0; i < 50; i++)
            _cartasCompradas.Add(_baralho.Comprar());

        Assert.True(
            _cartasCompradas.Distinct().Count() ==
            _cartasCompradas.Count);
    }


    [Fact]
    public void Embaralhar_DeveColocarTodasAsCartasNovamenteNoBaralhoEmOrdemAleatoria()
    {
        bool cartasJaCompradasForamReinseridasNoBaralho;
        bool cartasForamColocadasEmOrdemDiferente;
        List<Carta> cartasCompradas2 = [];

        for (int i = 0; i < 50; i++)
            _cartasCompradas.Add(_baralho.Comprar());

        _baralho.Embaralhar();

        for (int i = 0; i < 50; i++)
            cartasCompradas2.Add(_baralho.Comprar());


        cartasForamColocadasEmOrdemDiferente = !_cartasCompradas.SequenceEqual(cartasCompradas2);
        cartasJaCompradasForamReinseridasNoBaralho = _cartasCompradas.Intersect(cartasCompradas2).Any();

        Assert.True(cartasForamColocadasEmOrdemDiferente);
        Assert.True(cartasJaCompradasForamReinseridasNoBaralho);
    }
}
