using BlackJack.Domain.Cartas;
using System.Collections.Immutable;

namespace BlackJack.Domain.Apostadores;

internal class Apostador
{
    private readonly List<Carta> _cartas;

    internal ImmutableList<Carta> Mao { get => _cartas.ToImmutableList(); }


    internal int SomaDosValoresDasCartas
    {
        get
        {
            int somasemAses = 0;

            foreach (var carta in _cartas)
            {
                if (carta.Valor != ValorCarta.As)
                    somasemAses += carta.ValorParaBlackJack();
            }

            var somaComAses = AdicionarValorDosAsesASomaSeHouver(somasemAses);
            return somaComAses;
        }
    }


    private int AdicionarValorDosAsesASomaSeHouver(int somaSemAses)
    {
        var ases = _cartas.Where(c => c.Valor == ValorCarta.As);

        if (!ases.Any())
            return somaSemAses;

        const int asValendo1 = 1;
        var somaFinal = somaSemAses + ases.Count() * asValendo1;

        if (somaFinal > 21)
            return somaFinal;

        static int SomaComUmAsAlteradoDe1Para11(int valor)
            => valor - 1 + 11;

        if (SomaComUmAsAlteradoDe1Para11(somaFinal) > 21)
            return somaFinal;

        somaFinal = SomaComUmAsAlteradoDe1Para11(somaFinal);
        return somaFinal;
    }



    internal Apostador()
    {
        _cartas = [];
    }

    internal void ReceberCarta(Carta carta)
    {
        _cartas.Add(carta);
    }
}
