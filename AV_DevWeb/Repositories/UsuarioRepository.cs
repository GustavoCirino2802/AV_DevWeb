using AV_DevWeb.Models;
using AV_DevWeb.Data;
using AV_DevWeb.Repositories;

namespace AV_DevWeb.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly AppDataContext _context;
    public UsuarioRepository(AppDataContext context)
    {
        _context = context;
    }

    public void Cadastrar(Usuario usuario)
    {
        _context.Usuarios.Add(usuario);
        _context.SaveChanges();
    }

    public List<Usuario> Listar()
    {
        return _context.Usuarios.ToList();
    }

    public Usuario? BuscarUsuarioPorEmailSenha(string email, string senha)
    {
        Usuario? usuarioExistente = 
            _context.Usuarios.FirstOrDefault
            (x => x.Email == email && x.Senha == senha);
        return usuarioExistente;
    }

}
