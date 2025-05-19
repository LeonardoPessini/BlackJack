using BlackJack.Domain.Apostadores;
using BlackJack.Domain.Cartas;

namespace BlackJack.Test;

public class MesaTest
{
    private Baralho _baralho;
    private Mesa _mesa;
    private IEnumerable<int> _cartasCompradas;

    public MesaTest()
    {
        _baralho = new Baralho();
        _mesa = new Mesa();
        _mesa.ComprarDoBaralhoAteAtingirOValorLimite(_baralho);
        _cartasCompradas = _mesa.Mao.Select(c => c.ValorParaBlackJack());
    }

    [Fact]
    public void DeveSacarAteSomaSerIgualOuMaiorQue17()
    {
        Assert.True(_cartasCompradas.Sum() >= 17);
    }

    [Fact]
    public void CasoResultadoEstejaEntre17e21NaoDeveMaisComprar()
    {
        if(_cartasCompradas.Sum() > 21)
        {
            var somaDosValoresAntesDeSacarUltimaCarta = _cartasCompradas.Take(_cartasCompradas.Count() - 1).Sum();
            Assert.True(somaDosValoresAntesDeSacarUltimaCarta < 17);
        }
    }
}
