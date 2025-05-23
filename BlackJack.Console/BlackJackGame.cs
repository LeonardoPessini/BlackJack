using BlackJack.Domain;
using System;

namespace BlackJack.Console;

internal class BlackJackGame
{
    private int _jogadorId;

    public BlackJackGame()
    {
        _jogadorId = Janela.SolicitarId();        
    }

    internal void StartGame()
    {
        var valorDaAposta = Janela.SolicitarValorDaAposta();

        var jogo = new Jogo(_jogadorId, valorDaAposta);
        var janela = new Janela(jogo);
        var input = new InputOpcao(jogo);

        while (!jogo.PartidaFinalizada){
            janela.Renderizar();

            while (input.Entry()) ;
        }

        var resultado = jogo.ObterResultado();

        janela.Renderizar();

        if (resultado.Vencedor == Vencedor.Jogador)
            janela.RedenrizarVitoria();

        else if( resultado.Vencedor == Vencedor.Mesa)
            janela.RedenrizarDerrota();

        else 
            janela.RedenrizarEmpate();
    }
}
