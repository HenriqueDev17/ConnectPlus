using ConnectPlus.WebAPI.Models;

namespace ConnectPlus.WebAPI.Interfaces;

public interface ITipoDeContatoRepository
{
    List<TipoDeContato> Listar();
    void Cadastrar(TipoDeContato NovoTipoDeContato);
    void Atualizar(Guid id, TipoDeContato tipoDeContato);
    void Deletar(Guid id);
    TipoDeContato BuscarPorId(Guid id);
}
