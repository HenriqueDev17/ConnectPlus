using ConnectPlus.WebAPI.Models;

namespace ConnectPlus.WebAPI.Interfaces;

public interface IContatoRepository
{
    List<Contato> Listar();
    void Cadastrar(Contato contato);
    void Atualizar(Guid id, Contato contato);
    void Deletar(Guid id);
    Contato BuscarPorId(Guid id);
}
