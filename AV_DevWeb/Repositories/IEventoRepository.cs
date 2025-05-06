using System;
using AV_DevWeb.Models;

namespace AV_DevWeb.Repositories;

public interface IEventoRepository
{
    void Cadastrar(Evento evento);
    List<Evento> Listar();
    List<Evento> ListarPorUsuario(int usuarioId);
}
