using BlackJack.Domain.Apostadores;

namespace BlackJack.Test;

public class ApostadorTest
{
    Apostador _apostador;
    public ApostadorTest()
    {
        _apostador = new Apostador(new Usuario(1, 100));
    }

    [Fact]
    public void NaoDeveRealizarApostaComSaldoInsuficiente()
    {
        var message = Assert.Throws<InvalidOperationException>(() => _apostador.Apostar(101)).Message;
        Assert.Equal("Saldo insulficiente", message);
    }


    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(0.9)]
    public void NaoDeveRealizarApostaMenorQue1(decimal valorInvalido)
    {
        var message = Assert.Throws<InvalidOperationException>(() => _apostador.Apostar(valorInvalido)).Message;
        Assert.Equal("Valor minimo para aposta: 1", message);
    }
}
