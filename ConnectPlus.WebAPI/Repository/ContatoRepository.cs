using ConnectPlus.WebAPI.BdContextConnect;
using ConnectPlus.WebAPI.Interfaces;
using ConnectPlus.WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ConnectPlus.WebAPI.Repository;

public class ContatoRepository : IContatoRepository
{
    private readonly ConnectContext _context;

    public ContatoRepository(ConnectContext context)
    {
        _context = context;
    }

    public void Atualizar(Guid id, Contato contato)
    {
        var ContatoBuscado = _context.Contatos.Find(id);

        if (ContatoBuscado != null)
        {
            ContatoBuscado.Nome = contato.Nome;
            ContatoBuscado.FormaDeContato = contato.FormaDeContato;
            ContatoBuscado.Imagem = contato.Imagem;
            ContatoBuscado.TipoDeContato = contato.TipoDeContato;
            ContatoBuscado.IdTipoContato = contato.IdTipoContato;

            //O SaveChanges() detecta as mudanças na propriedade "Titulo" automaticamente
            _context.SaveChanges();
        }
    }

    public Contato BuscarPorId(Guid id)
    {
        return _context.Contatos.Include(Contato => Contato.IdTipoContatoNavigation)
            .FirstOrDefault(Contato => Contato.IdContato == id)!;
    }

    public void Cadastrar(Contato NovoContato)
    {
        _context.Contatos.Add(NovoContato);
        _context.SaveChanges();
    }

    public void Deletar(Guid id)
    {
        var ContatoBuscado = _context.Contatos.Find(id);

        if (ContatoBuscado != null) // Verifica se o tipo de evento existe antes de tentar deletar
        {
            _context.Contatos.Remove(ContatoBuscado); //remove o tipo de evento encontrado
            _context.SaveChanges(); // Salva as mudanças no banco de dados
        }
    }

    public List<Contato> Listar()
    {
        return _context.Contatos
            .Include(c => c.IdTipoContatoNavigation)
            .ToList();
    }

}