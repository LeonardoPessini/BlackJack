namespace BlackJack.Domain.Apostadores;

internal class UsuarioUseCase
{
    private static List<Usuario> _usuarios;

    static UsuarioUseCase()
    {
        _usuarios = new List<Usuario>();
    }

    internal void Add(Usuario usuario)
    {
        _usuarios.Add(usuario);
    }

    internal Usuario? Get(int usuarioId)
    {
        return _usuarios.First(u => u.Id == usuarioId);
    }

    internal void Delete(Usuario usuario)
    {
        _usuarios.Remove(usuario);
    }
}
