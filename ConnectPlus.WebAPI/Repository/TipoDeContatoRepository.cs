using ConnectPlus.WebAPI.BdContextConnect;
using ConnectPlus.WebAPI.Interfaces;
using ConnectPlus.WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ConnectPlus.WebAPI.Repository;

public class TipoDeContatoRepository : ITipoDeContatoRepository
{
    private readonly ConnectContext _context;

    public TipoDeContatoRepository(ConnectContext context)
    {
        _context = context;
    }

    public void Atualizar(Guid id, TipoDeContato tipoDeContato)
    {
        var TipoDeContatoBuscado = _context.TipoDeContatos.Find(id);

        if (TipoDeContatoBuscado != null)
        {
            TipoDeContatoBuscado.Titulo = tipoDeContato.Titulo;


            //O SaveChanges() detecta as mudanças na propriedade "Titulo" automaticamente
            _context.SaveChanges();

        }
    }

    public TipoDeContato BuscarPorId(Guid id)
    {
        return _context.TipoDeContatos.Find(id)!;
    }


    public void Cadastrar(TipoDeContato NovoTipoDeContato)
    {
        _context.TipoDeContatos.Add(NovoTipoDeContato);
        _context.SaveChanges();
    }

    public void Deletar(Guid id)
    {
        var tipoDeContatoBuscado = _context.TipoDeContatos.Find(id);

        if (tipoDeContatoBuscado != null)
        {
            _context.TipoDeContatos.Remove(tipoDeContatoBuscado);
            _context.SaveChanges();
        }
    }

    public List<TipoDeContato> Listar()
    {
        return _context.TipoDeContatos.OrderBy(TipoDeContato => TipoDeContato.Titulo).ToList();
    }
}
