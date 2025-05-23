using BlackJack.Domain.Apostadores;

namespace BlackJack.Test;

public class JogadorTest
{
    Jogador _jogador;
    public JogadorTest()
    {
        _jogador = new Jogador(new Usuario(1, 100));
    }

    [Fact]
    public void NaoDeveRealizarApostaComSaldoInsuficiente()
    {
        var message = Assert.Throws<InvalidOperationException>(() => _jogador.Apostar(101)).Message;
        Assert.Equal("Saldo insulficiente", message);
    }


    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(0.9)]
    public void NaoDeveRealizarApostaMenorQue1(decimal valorInvalido)
    {
        var message = Assert.Throws<InvalidOperationException>(() => _jogador.Apostar(valorInvalido)).Message;
        Assert.Equal("Valor minimo para aposta: 1", message);
    }
}
