using ConnectPlus.WebAPI.DTO;
using ConnectPlus.WebAPI.Interfaces;
using ConnectPlus.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;


namespace ConnectPlus.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TipoDeContatoController : ControllerBase
{
    private readonly ITipoDeContatoRepository _tipoDeContatoRepository;

    public TipoDeContatoController(ITipoDeContatoRepository tipoDeContatoRepository)
    {
        _tipoDeContatoRepository = tipoDeContatoRepository;
    }

    [HttpGet]
    public IActionResult Listar()
    {
        try
        {
            return Ok(_tipoDeContatoRepository.Listar());
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }
    [HttpGet("{id}")]

    public IActionResult BuscarPorId(Guid id)
    {
        try
        {
            return Ok(_tipoDeContatoRepository.BuscarPorId(id));
        }
        catch (Exception erro)
        {

            return BadRequest(erro.Message);
        }

    }

    [HttpPost]
    public IActionResult Cadastrar(TipoDeContatoDTO tipoDeContato)
    {
        try
        {
            var NovotipoDeContato = new TipoDeContato
            {
                Titulo = tipoDeContato.titulo!
            };

            _tipoDeContatoRepository.Cadastrar(NovotipoDeContato);
            return StatusCode(201, NovotipoDeContato);
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }


    }

    [HttpPut("{id}")]
    public IActionResult Atualizar(Guid id, TipoDeContatoDTO tipoDeContato)
    {
        try
        {
            var tipoDeContatoAtualizado = new TipoDeContato
            {
                Titulo = tipoDeContato.titulo!
            };

            _tipoDeContatoRepository.Atualizar(id, tipoDeContatoAtualizado);
            return StatusCode(204, tipoDeContato);
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }

    [HttpDelete("{id}")]
    public IActionResult Deletar(Guid id)
    {
        try
        {
            _tipoDeContatoRepository.Deletar(id);
            return NoContent();  // Retorna status code 204 para indicar que a operação foi bem-sucedida, mas não há conteúdo para retornar
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }
}
