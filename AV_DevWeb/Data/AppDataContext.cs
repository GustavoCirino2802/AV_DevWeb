using AV_DevWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace AV_DevWeb.Data;

public class AppDataContext : DbContext
{
    public AppDataContext(DbContextOptions options) : 
        base(options) { }
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Evento> Eventos { get; set; }
}
