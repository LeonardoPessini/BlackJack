using BlackJack.Domain;
using BlackJack.Domain.Apostadores;
using BlackJack.Domain.Cartas;
using BlackJack.Test.Utils;

namespace BlackJack.Test;

public class JogoTest : IDisposable
{
    Jogo _jogo;

    public JogoTest()
    {
        var usuarioUsecase = new UsuarioUseCase();
        usuarioUsecase.Add(new Usuario(1, 200));
        _jogo = new Jogo(jogadorId: 1, aposta: 100);
    }

    public void Dispose()
    {
        var repository = new UsuarioUseCase();
        var user = repository.Get(1);
        repository.Delete(user!);
    }

    [Fact]
    public void DeveCriarJogoCorretamente()
    {
        Assert.NotNull(_jogo);
        Assert.False(_jogo.JogadorJaComprouCarta);
        Assert.False(_jogo.PartidaFinalizada);
        Assert.False(_jogo.JogadorParou);
        Assert.Equal(2, _jogo.CartasComApostador.Count);
        Assert.Single(_jogo.CartasComDealer);
        Assert.Equal(100, _jogo.Aposta);
        Assert.Equal(1, _jogo.JogadorId);
    }

    [Fact]
    public void NaoDeveCriarJogocasoApostadorNaoTenhaSaldoParaCobrirAposta()
    {
        var repository = new UsuarioUseCase();
        repository.Add(new Usuario(2, 50));

        Assert.Throws<InvalidOperationException>(() => new Jogo(2, 100));
    }

    [Fact]
    public void Hit_JogadorDeveReceber1carta()
    {
        var cartasAntesDaCompra = _jogo.CartasComApostador.Count;

        _jogo.Hit();

        var cartasAposACompra = _jogo.CartasComApostador.Count;

        Assert.Equal(3, cartasAposACompra);
        Assert.True(cartasAntesDaCompra < cartasAposACompra);
    }

    [Fact]
    public void Hit_NaoPodeComprarSejogadorParou()
    {
        _jogo.Stand();
        Assert.Throws<InvalidOperationException>(() => _jogo.Hit());
    }

    [Fact]
    public void Hit_NaoDeveComprarSeJogadorDobrouAAposta()
    {
        _jogo.DoubleDown();
        Assert.Throws<InvalidOperationException>(() => _jogo.Hit());
    }

    [Fact]
    public void Stand_DeveMarcarJogadorParouComoTrue()
    {
        _jogo.Stand();
        Assert.True(_jogo.JogadorParou);
    }

    [Fact]
    public void DoubleDown_NaoPodeDobrarApostaSejogadorParou()
    {
        _jogo.Stand();
        Assert.Throws<InvalidOperationException>(() => _jogo.DoubleDown());
    }

    [Fact]
    public void DoubleDown_DeveEntregarUmaCartaAoApostador()
    {
        _jogo.DoubleDown();
        Assert.Equal(3, _jogo.CartasComApostador.Count);
    }

    [Fact]
    public void DoubleDown_NaoPodeFazerDoisDoubleDownPorPartida()
    {
        _jogo.DoubleDown();
        Assert.Throws<InvalidOperationException>(() => _jogo.DoubleDown());
    }

    [Fact]
    public void DoubleDown_NaoPodeDobrarApostaSejogadorJaComprouUmaCarta()
    {
        _jogo.Hit();
        Assert.Throws<InvalidOperationException>(() => _jogo.DoubleDown());
    }

    [Fact]
    public void DoubleDown_DeveDobrarAAposta()
    {
        var valorOriginalDaAposta = _jogo.Aposta;
        _jogo.DoubleDown();
        Assert.Equal(valorOriginalDaAposta * 2, _jogo.Aposta);
    }


    [Theory]
    [ClassData(typeof(ConfiguradorDeJogo))]
    internal void ValidaFormasDeGanhar(Func<Jogo> func, Vencedor vencedor, MotivoDaVitoria motivoVitoria)
    {
        var jogo = func();
        jogo.FinalizarPartida();
        var resultado = jogo.ObterResultado();

        Assert.Equal(vencedor, resultado.Vencedor);
        Assert.Equal(motivoVitoria, resultado.MovitoDaVitoria);
    }

    [Fact]
    public void FinalizarPartida_DeveFinalizarJogo()
    {
        _jogo.Stand();
        _jogo.FinalizarPartida();

        Assert.True(_jogo.PartidaFinalizada);
    }

    [Fact]
    public void FinalizarPartida_DeveFinalizarAposJogadorEstourar()
    {
        var baralho = new BaralhoViciado();
        baralho.AddCartas([ValorCarta.Valete, ValorCarta.Dama, ValorCarta.Rei, ValorCarta.Dois]);
        var jogo = new Jogo(1, 100, baralho);

        jogo.Hit();

        Assert.True(jogo.PartidaFinalizada);
        Assert.Equal(Vencedor.Mesa, jogo.ObterResultado().Vencedor);
    }

    [Fact]
    public void FinalizarPartida_DeveFinalizarAposJogadorObterBlackJack()
    {
        var baralho = new BaralhoViciado();
        baralho.AddCartas([ValorCarta.Valete, ValorCarta.Dama, ValorCarta.Rei, ValorCarta.As, ValorCarta.Cinco, ValorCarta.Nove]);
        var jogo = new Jogo(1, 100, baralho);

        jogo.Hit();

        Assert.True(jogo.PartidaFinalizada);
        Assert.Equal(Vencedor.Apostador, jogo.ObterResultado().Vencedor);

    }


    [Theory]
    [ClassData(typeof(ConfiguradorDeJogo))]
    internal void DeveEfetuarMovimentacaoBancariaCorretamente(Func<Jogo> func, Vencedor vencedor, MotivoDaVitoria motivoVitoria)
    {
        var usuario = new UsuarioUseCase().Get(usuarioId: 1);
        var saldoAntesDaAposta = usuario!.Saldo;

        var jogo = func();
        jogo.FinalizarPartida();
        var resultado = jogo.ObterResultado();

        var saldoAposAposta = usuario.Saldo;

        if (vencedor == Vencedor.Empate)
            Assert.Equal(saldoAntesDaAposta, saldoAposAposta);

        else if (vencedor == Vencedor.Apostador)
        {
            if (motivoVitoria == MotivoDaVitoria.BlackJackNatural)
                Assert.Equal(saldoAntesDaAposta + (jogo.Aposta * 1.5m), saldoAposAposta);

            else
                Assert.Equal(saldoAntesDaAposta + jogo.Aposta, saldoAposAposta);
        }

        else if (vencedor == Vencedor.Mesa)
            Assert.Equal(saldoAntesDaAposta - jogo.Aposta, saldoAposAposta);
    }

    [Fact]
    internal void NaoDeveRealizarMovimentacao2Vezes()
    {
        _jogo.Stand();
        _jogo.FinalizarPartida();

        Assert.Throws<InvalidOperationException>(() => _jogo.RealizarMovimentacaoBancariaParaOVencedor());
    }

    [Fact]
    internal void NaoDeveRealizarMovimentacaoSeJogoNaoFinalizou()
    {
        _jogo.Stand();
        Assert.Throws<InvalidOperationException>(() => _jogo.RealizarMovimentacaoBancariaParaOVencedor());
    }
}
