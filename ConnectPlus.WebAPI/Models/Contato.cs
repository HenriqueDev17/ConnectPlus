using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ConnectPlus.WebAPI.Models;

[Table("Contato")]
[Index("TipoDeContato", Name = "UQ__Contato__1BB80D302DB0B584", IsUnique = true)]
[Index("FormaDeContato", Name = "UQ__Contato__DCCB6F48C8D4EBC2", IsUnique = true)]
public partial class Contato
{
    [Key]
    public Guid IdContato { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string Nome { get; set; } = null!;

    [Column("formaDeContato")]
    [StringLength(256)]
    [Unicode(false)]
    public string FormaDeContato { get; set; } = null!;

    [StringLength(100)]
    [Unicode(false)]
    public string Imagem { get; set; } = null!;

    [Column("tipoDeContato")]
    [StringLength(256)]
    [Unicode(false)]
    public string TipoDeContato { get; set; } = null!;

    public Guid? IdTipoContato { get; set; }

    [ForeignKey("IdTipoContato")]
    [InverseProperty("Contatos")]
    public virtual TipoDeContato? IdTipoContatoNavigation { get; set; }
}
