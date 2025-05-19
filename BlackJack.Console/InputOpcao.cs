using BlackJack.Domain;

namespace BlackJack.Console;

internal class InputOpcao
{
    private Jogo _jogo;

    internal InputOpcao(Jogo jogo)
    {
        _jogo = jogo;
    }

    internal bool Entry()
    {
        var teclaClicada = System.Console.ReadKey();
        var entry = teclaClicada.Key;

        if (entry == Hit.Key)
        {
            _jogo.Hit();
            return true;
        }

        else if (entry == Stand.Key)
        {
            _jogo.Stand();
            _jogo.FinalizarPartida();
            return true;
        }

        else if (entry == DoubleDown.Key)
        {
            if (_jogo.JogadorJaComprouCarta)
                return false;
            
            _jogo.DoubleDown();
            _jogo.FinalizarPartida();
            return true;
        }

        else if (entry == Out.Key)
        {
            throw new ExitException();
        }

        else
        {
            return false;
        }
        
    }

    public static readonly KeyValuePair<System.ConsoleKey, string> Hit = new(System.ConsoleKey.Q, "Comprar");
    public static readonly KeyValuePair<System.ConsoleKey, string> Stand = new(System.ConsoleKey.W, "Parar");
    public static readonly KeyValuePair<System.ConsoleKey, string> DoubleDown = new(System.ConsoleKey.E, "Dobrar");
    public static readonly KeyValuePair<System.ConsoleKey, string> Out = new(System.ConsoleKey.Escape, "Sair");

}


internal class InvalidOptionException : Exception
{

}

internal class ExitException : Exception
{

}

