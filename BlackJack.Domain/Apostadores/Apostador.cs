namespace BlackJack.Domain.Apostadores;

internal class Apostador : Jogador
{
    public Usuario Usuario { get; }

    internal Apostador(Usuario usuario)
    {
        Usuario = usuario;
    }

    internal void Apostar(decimal valor)
    {
        if (valor < 1)
            throw new InvalidOperationException("Valor minimo para aposta: 1");

        Usuario.RetirarSaldo(valor);
    }
}
