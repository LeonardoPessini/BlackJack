using BlackJack.Domain.Apostadores;
using BlackJack.Domain.Cartas;
using System.Collections.Immutable;

namespace BlackJack.Domain;

public class Jogo 
{
    public Guid Id { get; }
    public decimal JogadorSaldo { get => Jogador.Usuario.Saldo; }
    public decimal Aposta { get; private set; }
    public bool JogadorJaComprouCarta { get; private set; }
    public bool PartidaFinalizada { get; private set; }
    public bool JogadorParou { get; private set; }
    public int JogadorId { get; }

    public ImmutableList<Carta> CartasComDealer { get => Mesa.Mao; }
    public ImmutableList<Carta> CartasComApostador { get => Jogador.Mao; }

    internal readonly Jogador Jogador;
    internal readonly Mesa Mesa;
    private readonly IBaralho _baralho;
    private bool _movimentacaoBancariaJaFoiRealizada;

    public Jogo(int jogadorId, decimal aposta)
    {
        new UsuarioUseCase().Add(new Usuario(1, 200)); // TEMPORARIO | ATE IMPLEMENTAR O SISTEMA DE ARMAZENAMENTO DE USUARIO

        Jogador = ObterApostadorPorId(jogadorId);
        AplicarDinheiroDoApostadorNaAposta(aposta);

        Id = Guid.NewGuid();
        JogadorId = Jogador.Usuario.Id;

        JogadorJaComprouCarta = false;
        JogadorParou = false;
        PartidaFinalizada = false;

        Mesa = new Mesa();
        _baralho = new Baralho();
        _movimentacaoBancariaJaFoiRealizada = false;

        Mesa.ReceberCarta(_baralho.Comprar());
        Jogador.ReceberCarta(_baralho.Comprar());
        Jogador.ReceberCarta(_baralho.Comprar());
    }


    internal Jogo(int jogadorId, decimal aposta, IBaralho baralho)
    {
        Jogador = ObterApostadorPorId(jogadorId);
        AplicarDinheiroDoApostadorNaAposta(aposta);

        Id = Guid.NewGuid();
        JogadorId = Jogador.Usuario.Id;

        JogadorJaComprouCarta = false;
        JogadorParou = false;
        PartidaFinalizada = false;

        Mesa = new Mesa();
        _baralho = baralho;
        _movimentacaoBancariaJaFoiRealizada = false;

        Mesa.ReceberCarta(_baralho.Comprar());
        Jogador.ReceberCarta(_baralho.Comprar());
        Jogador.ReceberCarta(_baralho.Comprar());
    }

    private void AplicarDinheiroDoApostadorNaAposta(decimal aposta)
    {
        Aposta = aposta;
        Jogador.Usuario.RetirarSaldo(aposta);
    }


    private Jogador ObterApostadorPorId(int id)
    {
        return new Jogador(new UsuarioUseCase().Get(id) 
            ?? throw new ArgumentException("Usuario nao existe na base"));
    }


    public void Hit()
    {
        if (PartidaFinalizada)
            throw new InvalidOperationException("A partida ja acabou, então essa operação não é possível");

        if (JogadorParou)
            throw new InvalidOperationException("O jogador ja parou, então essa operação não é possível");

        Jogador.ReceberCarta(_baralho.Comprar());
        JogadorJaComprouCarta = true;

        if (Jogador.SomaDosValoresDasCartas >= 21)
            FinalizarPartida();
    }


    public void Stand()
    {
        JogadorParou = true;
    }


    public void DoubleDown()
    {
        if (JogadorJaComprouCarta)
            throw new InvalidOperationException("Jogador ja comprou uma carta, então essa operação não é permitida");

        if (PartidaFinalizada || JogadorParou)
            throw new InvalidOperationException("O jogador ja parou, então essa operação não é possível");

        Jogador.ReceberCarta(_baralho.Comprar());
        Aposta *= 2;
        JogadorParou = true;
    }

    public void FinalizarPartida()
    {
        PartidaFinalizada = true;

        if(!_movimentacaoBancariaJaFoiRealizada)
            RealizarMovimentacaoBancariaParaOVencedor();
    }


    public Resultado ObterResultado()
    {
        if (!PartidaFinalizada)
            throw new InvalidOperationException("A partida ainda nao acabou, então não é possível obter um vencedor");

        var somaDasCartasDoJogador = Jogador.SomaDosValoresDasCartas;

        var jogadorEstourou = somaDasCartasDoJogador > 21;
        if (jogadorEstourou)
            return new Resultado(Vencedor.Mesa, MotivoDaVitoria.OponenteEstorou);

        if (Mesa.Mao.Count == 1) 
            Mesa.ComprarDoBaralhoAteAtingirOValorLimite(_baralho);
        
        var somaDasCartasDaMesa = Mesa.SomaDosValoresDasCartas;

        var mesaEstourou = somaDasCartasDaMesa > 21;
        if (mesaEstourou)
            return new Resultado(Vencedor.Jogador, MotivoDaVitoria.OponenteEstorou);
        

        var mesaTemBlackJack = somaDasCartasDaMesa == 21;
        var jogadorTemBlackJack = somaDasCartasDoJogador == 21;

        bool MesaComBlackJackNatural = mesaTemBlackJack && Mesa.Mao.Count == 2;
        bool jogadorComBlackJackNatural = jogadorTemBlackJack && Jogador.Mao.Count == 2;

        if(MesaComBlackJackNatural && jogadorComBlackJackNatural)
            return new Resultado(Vencedor.Empate, MotivoDaVitoria.Empate);

        if (MesaComBlackJackNatural)
            return new Resultado(Vencedor.Mesa, MotivoDaVitoria.BlackJackNatural);

        if (jogadorComBlackJackNatural)
            return new Resultado(Vencedor.Jogador, MotivoDaVitoria.BlackJackNatural);

        if(mesaTemBlackJack && jogadorTemBlackJack)
            return new Resultado(Vencedor.Empate, MotivoDaVitoria.Empate);

        if (mesaTemBlackJack)
            return new Resultado(Vencedor.Mesa, MotivoDaVitoria.BlackJack);

        if (jogadorTemBlackJack)
            return new Resultado(Vencedor.Jogador, MotivoDaVitoria.BlackJack);

        if(somaDasCartasDaMesa == somaDasCartasDoJogador)
            return new Resultado(Vencedor.Empate, MotivoDaVitoria.Empate);

        return somaDasCartasDaMesa > somaDasCartasDoJogador ?
            new Resultado(Vencedor.Mesa, MotivoDaVitoria.Normal) :
            new Resultado(Vencedor.Jogador, MotivoDaVitoria.Normal);
    }

    public void RealizarMovimentacaoBancariaParaOVencedor()
    {
        if(!PartidaFinalizada)
            throw new InvalidOperationException("A partida ainda não acabou");

        if (_movimentacaoBancariaJaFoiRealizada)
            throw new InvalidOperationException("As transferencias já foram efetuadas");

        var resultado = ObterResultado();

        if (resultado.Vencedor == Vencedor.Jogador)
        {
            if (resultado.MovitoDaVitoria == MotivoDaVitoria.BlackJackNatural)
                Jogador.Usuario.AdicionarSaldo(Aposta + (Aposta * 1.5m));
            
            else
                Jogador.Usuario.AdicionarSaldo(Aposta + Aposta);

        }
        else if (resultado.Vencedor == Vencedor.Empate)
        {
            Jogador.Usuario.AdicionarSaldo(Aposta);
        }

        _movimentacaoBancariaJaFoiRealizada = true;
    }
}
