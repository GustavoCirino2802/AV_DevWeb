using AV_DevWeb.Models;
using AV_DevWeb.Data;
using AV_DevWeb.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AV_DevWeb.Repositories;

public class EventoRepository : IEventoRepository
{
    private readonly AppDataContext _context;
    public EventoRepository(AppDataContext context)
    {
        _context = context;
    }

    public void Cadastrar(Evento evento)
    {
    _context.Eventos.Add(evento);
    _context.SaveChanges();
    }

    public List<Evento> Listar()
    {
        return _context.Eventos.ToList();
    }

    public List<Evento> ListarPorUsuario(int usuarioId)
    {
        // Filtra os eventos pelo UsuarioId
        return _context.Eventos
            .Where(e => e.UsuarioId == usuarioId)
            .Include(e => e.Usuario) // Inclui os dados do usuário, se necessário
            .ToList();
    }
}
