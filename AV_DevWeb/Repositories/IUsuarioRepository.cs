using System;
using AV_DevWeb.Models;

namespace AV_DevWeb.Repositories;

public interface IUsuarioRepository
{
    void Cadastrar(Usuario usuario);
    List<Usuario> Listar();
    Usuario? BuscarUsuarioPorEmailSenha(string email, string senha);
}

