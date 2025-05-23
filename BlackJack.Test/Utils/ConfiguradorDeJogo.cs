using BlackJack.Domain.Cartas;
using BlackJack.Domain;
using System.Collections;

namespace BlackJack.Test.Utils;


internal class ConfiguradorDeJogo : IEnumerable<object[]>
{
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[] { new Func<Jogo>(BalckJackNaturalParaMesa), Vencedor.Mesa, MotivoDaVitoria.BlackJackNatural };
        yield return new object[] { new Func<Jogo>(BalckJackNaturalParaJogador), Vencedor.Jogador, MotivoDaVitoria.BlackJackNatural };
        yield return new object[] { new Func<Jogo>(BlcakJackNaturalParaAmbos), Vencedor.Empate, MotivoDaVitoria.Empate };
        yield return new object[] { new Func<Jogo>(BlcakJackNaturalParaMesaENormalParaJogador), Vencedor.Mesa, MotivoDaVitoria.BlackJackNatural };
        yield return new object[] { new Func<Jogo>(BlcakJackNaturalParaJogadorENormalParaMesa), Vencedor.Jogador, MotivoDaVitoria.BlackJackNatural };
        yield return new object[] { new Func<Jogo>(BlcakJackNormalParaAmbos), Vencedor.Empate, MotivoDaVitoria.Empate };
        yield return new object[] { new Func<Jogo>(BalckJackNaturalParaMesa), Vencedor.Mesa, MotivoDaVitoria.BlackJackNatural };
        yield return new object[] { new Func<Jogo>(VitoriaNormalJogador), Vencedor.Jogador, MotivoDaVitoria.Normal };
        yield return new object[] { new Func<Jogo>(VitoriaNormalMesa), Vencedor.Mesa, MotivoDaVitoria.Normal };
        yield return new object[] { new Func<Jogo>(MesaEstourando), Vencedor.Jogador, MotivoDaVitoria.OponenteEstorou };
        yield return new object[] { new Func<Jogo>(JogadorEstourando), Vencedor.Mesa, MotivoDaVitoria.OponenteEstorou };
    }


    const int jogadorId = 1;
    const decimal valorDaAposta = 100;

    private Func<ValorCarta[], Jogo> ConfiguracaoInicialDeJogo = (cartas) =>
    {
        var baralho = new BaralhoViciado();
        baralho.AddCartas(cartas);
        var jogo = new Jogo(jogadorId, valorDaAposta, baralho);
        return jogo;
    };

    internal Jogo BalckJackNaturalParaMesa()
    {
        var jogo = ConfiguracaoInicialDeJogo([ValorCarta.Rei, ValorCarta.Dama, ValorCarta.Oito, ValorCarta.As]);
        jogo.Stand();
        return jogo;
    }


    internal Jogo BalckJackNaturalParaJogador()
    {
        var jogo = ConfiguracaoInicialDeJogo([ValorCarta.Dama, ValorCarta.Rei, ValorCarta.As, ValorCarta.Valete]);
        jogo.Stand();
        return jogo;
    }

    internal Jogo BlcakJackNaturalParaAmbos()
    {
        var jogo = ConfiguracaoInicialDeJogo([ValorCarta.Dama, ValorCarta.Rei, ValorCarta.As, ValorCarta.As]);
        jogo.Stand();
        return jogo;
    }

    internal Jogo BlcakJackNaturalParaMesaENormalParaJogador()
    {
        var jogo = ConfiguracaoInicialDeJogo([ValorCarta.Dama, ValorCarta.Rei, ValorCarta.Dama, ValorCarta.As, ValorCarta.As]);
        jogo.Hit();
        jogo.Stand();
        return jogo;
    }

    internal Jogo BlcakJackNaturalParaJogadorENormalParaMesa()
    {
        var jogo = ConfiguracaoInicialDeJogo([ValorCarta.Dama, ValorCarta.Rei, ValorCarta.As, ValorCarta.Cinco, ValorCarta.Seis]);
        jogo.Stand();
        return jogo;
    }

    internal Jogo BlcakJackNormalParaAmbos()
    {
        var jogo = ConfiguracaoInicialDeJogo([ValorCarta.Dama, ValorCarta.Rei, ValorCarta.Cinco, ValorCarta.Seis, ValorCarta.Cinco, ValorCarta.Seis]);
        jogo.Hit();
        jogo.Stand();
        return jogo;
    }

    internal Jogo VitoriaNormalJogador()
    {
        var jogo = ConfiguracaoInicialDeJogo([ValorCarta.Dama, ValorCarta.Rei, ValorCarta.Nove, ValorCarta.Oito]);
        jogo.Stand();
        return jogo;
    }

    internal Jogo VitoriaNormalMesa()
    {
        var jogo = ConfiguracaoInicialDeJogo([ValorCarta.Dama, ValorCarta.Rei, ValorCarta.Oito, ValorCarta.Nove]);
        jogo.Stand();
        return jogo;
    }


    internal Jogo MesaEstourando()
    {
        var jogo = ConfiguracaoInicialDeJogo([ValorCarta.Dama, ValorCarta.Rei, ValorCarta.Nove, ValorCarta.Seis, ValorCarta.Nove]);
        jogo.Stand();
        return jogo;
    }

    internal Jogo JogadorEstourando()
    {
        var jogo = ConfiguracaoInicialDeJogo([ValorCarta.Dama, ValorCarta.Rei, ValorCarta.Nove, ValorCarta.Nove, ValorCarta.Oito]);
        jogo.Hit();
        jogo.Stand();
        return jogo;
    }
}