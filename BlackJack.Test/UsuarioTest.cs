using BlackJack.Domain.Apostadores;

namespace BlackJack.Test;

public class UsuarioTest
{
    private Usuario _usuario;
    public UsuarioTest()
    {
        _usuario = new Usuario(id: 1, saldo: 100);
    }

    [Fact]
    public void DeveCriarUsuario()
    {
        Assert.Equal(1, _usuario.Id);
        Assert.Equal(100, _usuario.Saldo);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void NaoDeveCriarUsuarioComIdInvalido(int idInvalido)
    {
        var message = Assert.Throws<ArgumentException>(() => new Usuario(id:idInvalido, saldo: 100)).Message;
        Assert.Equal("Id invalido", message);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void NaoDeveCriarUsuarioComSaldoInvalido(int saldoInvalido)
    {
        var message = Assert.Throws<ArgumentException>(() => new Usuario(id: 1, saldo: saldoInvalido)).Message;
        Assert.Equal("Saldo invalido", message);
    }


    [Theory]
    [InlineData(1, 101)]
    public void DeveAtualizarSaldoAposInserir(decimal saldoAInserir, decimal saldoFinal)
    {
        _usuario.AdicionarSaldo(saldoAInserir);
        Assert.Equal(saldoFinal, _usuario.Saldo);
    }

    [Theory]
    [InlineData(1, 99)]
    public void DeveAtualizarSaldoAposRemover(decimal saldoARemover, decimal saldoFinal)
    {
        _usuario.RetirarSaldo(saldoARemover);
        Assert.Equal(saldoFinal, _usuario.Saldo);
    }


    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void NaoDeveInserirZeroOuNegativo(decimal saldoAInserir)
    {
        Assert.Throws<ArgumentException>(() => _usuario.AdicionarSaldo(saldoAInserir));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void NaoDeveRemoverZeroOuNegativo(decimal saldoAInserir)
    {
        Assert.Throws<ArgumentException>(() => _usuario.RetirarSaldo(saldoAInserir));
    }


    [Fact]
    public void NaoDeveRetirarSaldoSeSaldoInsuficiente()
    {
        var message = Assert.Throws<InvalidOperationException>(() => _usuario.RetirarSaldo(101)).Message;
        Assert.Equal("Saldo insulficiente", message);
    }
}
