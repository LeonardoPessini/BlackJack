namespace BlackJack.Domain.Cartas;

internal class Baralho : IBaralho
{
    private List<Carta> _cartas;
    private int _contadorDeCompra;

    internal Baralho()
    {
        _cartas = [];

        AdicionarCartasDoNaipe(Naipe.Copa);
        AdicionarCartasDoNaipe(Naipe.Espadas);
        AdicionarCartasDoNaipe(Naipe.Ouro);
        AdicionarCartasDoNaipe(Naipe.Paus);

        Embaralhar();
    }

    private void AdicionarCartasDoNaipe(Naipe naipe)
    {
        _cartas.AddRange(
            [
                new(ValorCarta.Dois, naipe),
                new(ValorCarta.Tres, naipe),
                new(ValorCarta.Quatro, naipe),
                new(ValorCarta.Cinco, naipe),
                new(ValorCarta.Seis, naipe),
                new(ValorCarta.Sete, naipe),
                new(ValorCarta.Oito, naipe),
                new(ValorCarta.Nove, naipe),
                new(ValorCarta.Dez, naipe),
                new(ValorCarta.Valete, naipe),
                new(ValorCarta.Dama, naipe),
                new(ValorCarta.Rei, naipe),
                new(ValorCarta.As, naipe)

            ]);
    }


    public void Embaralhar()
    {
        var random = new Random();

        _cartas = _cartas.OrderBy(x => random.Next()).ToList();
        _contadorDeCompra = 0;
    }

    public Carta Comprar()
    {
        return _cartas[_contadorDeCompra++];
    }
}
