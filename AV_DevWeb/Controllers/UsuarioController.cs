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
[Route("api/usuarios")]
public class UsuarioController : ControllerBase
{

    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IConfiguration _configuration;
    public UsuarioController(IUsuarioRepository usuarioRepository,
        IConfiguration configuration)
    {
        _usuarioRepository = usuarioRepository;
        _configuration = configuration;
    }

        [HttpPost("cadastrar")]
    public IActionResult Cadastrar([FromBody] Usuario usuario)
    {
        _usuarioRepository.Cadastrar(usuario);
        return Created("", usuario);
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] Usuario usuario)
    {
        Usuario? usuarioExistente = _usuarioRepository
            .BuscarUsuarioPorEmailSenha(usuario.Email, usuario.Senha);

        if (usuarioExistente == null)
        {
            return Unauthorized(new { mensagem = "Usuário ou senha inválidos!" });
        }

        string token = GerarToken(usuarioExistente);
        return Ok(token);
    }

    [HttpGet("listar")]
    [Authorize]
    public IActionResult Listar()
    {
        return Ok(_usuarioRepository.Listar());
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    public string GerarToken(Usuario usuario)
{
    var claims = new[]
    {
        new Claim(ClaimTypes.Email, usuario.Email),
        new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()) // Adiciona o ID do usuário como claim
    };

    var chave = Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"]!);

    var assinatura = new SigningCredentials(
        new SymmetricSecurityKey(chave),
        SecurityAlgorithms.HmacSha256
    );

    var token = new JwtSecurityToken(
        claims: claims,
        expires: DateTime.Now.AddMinutes(2),
        signingCredentials: assinatura
    );

    return new JwtSecurityTokenHandler().WriteToken(token);
}
}

