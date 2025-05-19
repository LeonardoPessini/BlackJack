
using BlackJack.Domain.Cartas;

namespace BlackJack.Console;

internal record CartaView
{
    public string PrimeiraLinha { get; private set; }
    public string SegundaLinha { get; private set; }
    public string TerceiraLinha { get; private set; }
    public string QuartaLinha { get; private set; }
    public string QuintaLinha { get; private set; }
    public string SextaLinha { get; private set; }
    public string SetimaLinha { get; private set; }
    public string OitavaLinha { get; private set; }
    public string NonaLinha { get; private set; }

    public CartaView(string? valor, string? naipe)
    {
        PrimeiraLinha =  "┌───────────┐";
        SegundaLinha = $"│ {valor,-2}        │";
        TerceiraLinha = $"│ {naipe,1}         │";
        QuartaLinha = $"│           │";
        QuintaLinha = $"│     {naipe,1}     │";
        SextaLinha = $"│           │";
        SetimaLinha = $"│         {naipe,1} │";
        OitavaLinha = $"│        {valor,2} │";
        NonaLinha = $"└───────────┘";
    }

    internal static CartaView CartaViradaParaBaixo()
    {
        return new CartaView(null, null);
    }
}


