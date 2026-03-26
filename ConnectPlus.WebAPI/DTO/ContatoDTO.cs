namespace ConnectPlus.WebAPI.DTO;

public class ContatoDTO
{
    public string Nome { get; set; } = null!;
    public string FormaDeContato { get; set; } = null!;
    public IFormFile? Imagem { get; set; }
    public string TipoDeContato { get; set; } = null!;
    public Guid? IdTipoContato { get; set; }
}
