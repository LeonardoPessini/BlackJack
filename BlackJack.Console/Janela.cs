using BlackJack.Domain;
using System;
using System.Text;

namespace BlackJack.Console;

internal class Janela
{
    private Jogo _jogo;

    public Janela(Jogo jogo)
    {
        System.Console.OutputEncoding = Encoding.UTF8;
        _jogo = jogo;
    }

    public void Renderizar()
    {
        System.Console.Clear();

        var cartasComDealer = _jogo.CartasComDealer.Select(c => c.ToConsole()).ToList();
        var cartasComJogador = _jogo.CartasComApostador.Select(c => c.ToConsole());

        var cartaViradaParaBaixo = CartaView.CartaViradaParaBaixo();

        while(cartasComDealer.Count < 5)
            cartasComDealer.Add(cartaViradaParaBaixo);

        RenderizarCartasUmaAoLadoDaOutra(cartasComDealer);
        RenderizarCartasUmaAoLadoDaOutra(cartasComJogador);

        RenderizarSaldoDoJogadorEValorDaAposta();

        RenderizarOpcoesDeJogada();
    }

    private void RenderizarCartasUmaAoLadoDaOutra(IEnumerable<CartaView> cartas)
    {
        ImprimirLinha( cartas.Select(c => c.PrimeiraLinha) );
        ImprimirLinha( cartas.Select(c => c.SegundaLinha) );
        ImprimirLinha( cartas.Select(c => c.TerceiraLinha) );
        ImprimirLinha( cartas.Select(c => c.QuartaLinha) );
        ImprimirLinha( cartas.Select(c => c.QuintaLinha) );
        ImprimirLinha( cartas.Select(c => c.SextaLinha) );
        ImprimirLinha( cartas.Select(c => c.SetimaLinha) );
        ImprimirLinha( cartas.Select(c => c.OitavaLinha) );
        ImprimirLinha( cartas.Select(c => c.NonaLinha) );
        System.Console.WriteLine();
    }

    private void ImprimirLinha(IEnumerable<string> linha)
    {
        foreach (var conteudo in linha)
            System.Console.Write(conteudo + " ");

        System.Console.WriteLine();
    }

    private void RenderizarOpcoesDeJogada()
    {
        System.Console.Write($" [ {InputOpcao.Hit.Key} ] {InputOpcao.Hit.Value}  ");
        System.Console.Write($" [ {InputOpcao.Stand.Key} ] {InputOpcao.Stand.Value}  ");

        if( !_jogo.JogadorJaComprouCarta )
            System.Console.Write($" [ {InputOpcao.DoubleDown.Key} ] {InputOpcao.DoubleDown.Value}  ");

        System.Console.Write($" [ {InputOpcao.Out.Key} ] {InputOpcao.Out.Value}  ");


        System.Console.WriteLine();
    }

    private void RenderizarSaldoDoJogadorEValorDaAposta()
    {
        System.Console.WriteLine();

        System.Console.WriteLine("......................................................");
        System.Console.WriteLine($"Saldo Total: {_jogo.JogadorSaldo,-8}");
        System.Console.WriteLine($"Aposta: {_jogo.Aposta,-8}");
        System.Console.WriteLine("......................................................");

        System.Console.WriteLine();
    }

    private static Action Cabecalho = () =>
    {
        System.Console.Clear();
        System.Console.WriteLine("............................");
        System.Console.WriteLine("       Black Jack           ");
        System.Console.WriteLine("............................");
        System.Console.WriteLine();
    };

    internal static int SolicitarId()
    {
        int id;
        bool idValido = false;
        do{
            Cabecalho();

            if(idValido)
                System.Console.WriteLine("ID invalido!!!");

            System.Console.Write("Entre com seu ID: ");
            idValido = int.TryParse(System.Console.ReadLine(), out id);

        } while (!idValido);

        return id;
    }

    internal static decimal SolicitarValorDaAposta()
    {
        decimal valor;
        bool valorValido = false;
        do{
            Cabecalho();

            if (valorValido)
                System.Console.WriteLine("Valor invalido!!!");

            System.Console.Write("Entre com o valor da aposta: ");
            valorValido = decimal.TryParse(System.Console.ReadLine(), out valor);
        } while (!valorValido);

        return valor;
    }

    internal void RedenrizarVitoria()
    {
        System.Console.WriteLine("Voce venceu!!!");
    }

    internal void RedenrizarDerrota()
    {
        System.Console.WriteLine("Voce perdeu!!!");
    }

    internal void RedenrizarEmpate()
    {
        System.Console.WriteLine("Empatou!!!");
    }

    internal static bool DesejaJogarNovamente()
    {
        System.Console.WriteLine("Deseja jogar novamente? [S - Sim / N - Nao] ");

        while(true)
        {
            var teclaClicada = System.Console.ReadKey();
            var opcao = teclaClicada.Key;

            if (opcao == ConsoleKey.S)
                return true;

            else if (opcao == ConsoleKey.N)
                return false;
        }
    }
}

