using System.Drawing;

namespace BlackJack.Domain.Apostadores;

internal class Usuario
{
    internal int Id { get; set; }
    internal decimal Saldo { get; private set; }

    internal Usuario(int id, decimal saldo)
    {
        if (id <= 0)
            throw new ArgumentException("Id invalido");

        if (saldo < 1)
            throw new ArgumentException("Saldo invalido");

        Id = id;
        Saldo = saldo;
    }

    internal void RetirarSaldo(decimal valor)
    {
        if(valor <= 0) 
            throw new ArgumentException("Valor invalido");

        if (valor > Saldo)
            throw new InvalidOperationException("Saldo insulficiente");

        Saldo -= valor;
    }

    internal void AdicionarSaldo(decimal valor)
    {
        if (valor <= 0)
            throw new ArgumentException("Valor invalido");

        Saldo += valor;
    }
}
