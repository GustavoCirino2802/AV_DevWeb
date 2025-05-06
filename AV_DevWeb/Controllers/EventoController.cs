using Microsoft.AspNetCore.Mvc;
using AV_DevWeb.Models;
using AV_DevWeb.Repositories;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authorization;

namespace AV_DevWeb.Controllers;

[ApiController]
[Route("api/eventos")]
public class EventoController : ControllerBase
{

    private readonly IEventoRepository _eventoRepository;
    private readonly IConfiguration _configuration;
    public EventoController(IEventoRepository eventoRepository,
        IConfiguration configuration)
    {
        _eventoRepository = eventoRepository;
        _configuration = configuration;
    }

[HttpPost("cadastrar")]
[Authorize]
public IActionResult CriarEvento([FromBody] Evento evento)
{
    var userIdClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
    if (userIdClaim == null)
    {
        return Unauthorized(new { mensagem = "Usuário não autenticado!" });
    }

    evento.UsuarioId = int.Parse(userIdClaim.Value); // Associa o evento ao usuário logado
    _eventoRepository.Cadastrar(evento); // Salva o evento no repositório
    return Created("", evento);
}

    [HttpGet("listar")]
    public IActionResult Listar()
    {
        return Ok(_eventoRepository.Listar());
    }

    [HttpGet("listarporusuario")]
    [Authorize]
    public IActionResult ListarMeusEventos()
    {
        var usuarioId = int.Parse(User.FindFirst("id").Value); // Obtém o ID do usuário logado
        var eventos = _eventoRepository.ListarPorUsuario(usuarioId);
        return Ok(eventos);
    }


}
