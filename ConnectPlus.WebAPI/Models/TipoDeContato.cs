using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace ConnectPlus.WebAPI.Models;

[Table("TipoDeContato")]
public partial class TipoDeContato
{
    [Key]
    [Column("TipoDeContatoID")]
    public Guid TipoDeContatoId { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string Titulo { get; set; } = null!;

    [InverseProperty("IdTipoContatoNavigation")]
    [JsonIgnore]
    public virtual ICollection<Contato> Contatos { get; set; } = new List<Contato>();
}
