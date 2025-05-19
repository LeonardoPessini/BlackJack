using BlackJack.Domain.Cartas;

namespace BlackJack.Domain.Apostadores;

internal class Mesa : Jogador
{
    internal void ComprarDoBaralhoAteAtingirOValorLimite(IBaralho baralho)
    {
        do{
            base.ReceberCarta(baralho.Comprar());

        } while (base.SomaDosValoresDasCartas <= 17);
    }
}
