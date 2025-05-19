using BlackJack.Domain.Cartas;

namespace BlackJack.Test.Utils;

internal class BaralhoViciado : IBaralho
{
    internal List<Carta> Cartas;
    private int contador;
    internal BaralhoViciado()
    {
        Cartas = [];
        contador = 0;
    }

    public Carta Comprar()
    {
        return Cartas[contador++];
    }

    internal void AddCarta(ValorCarta valor)
    {
        Cartas.Add(new Carta(valor, Naipe.Copa));
    }

    internal void AddCartas(params ValorCarta[] cartas)
    {
        foreach (var carta in cartas)
            AddCarta(carta);
    }

    public void Embaralhar() { }
}