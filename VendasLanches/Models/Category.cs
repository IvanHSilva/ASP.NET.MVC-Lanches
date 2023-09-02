using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VendasLanches.Models;

[Table("Categorias")]
public class Category {

    // Attributes
    [Key]
    public int Id { get; set; }
    [Column("Nome")][Required, MaxLength(50)]
    public string Name { get; set; } = string.Empty;
    [Column("Descricao")][Required, MaxLength(150)]
    public string Description { get; set; } = string.Empty;
    [Column("DataCad")]
    [Display(Name = "Data de Cadastro")]
    [Required]
    public DateTime RegDate { get; set; }

    // Constructors
    public Category(){}

    public Category(int id, string name, string description, DateTime regDate) {
        Id = id;
        Name = name;
        Description = description;
        RegDate = regDate;
    }
}
