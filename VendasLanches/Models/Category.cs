using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VendasLanches.Models;

[Table("Categorias")]
public class Category {

    // Attributes
    [Key]
    public int Id { get; set; }
    [Column("Nome")][MaxLength(50)]
    [Required(ErrorMessage = "Nome obrigatório")]
    public string Name { get; set; } = string.Empty;
    [Column("Descricao")][MaxLength(150)]
    [Required(ErrorMessage = "Descrição obrigatória")]
    public string Description { get; set; } = string.Empty;
    [Column("DataCad")]
    [Display(Name = "Data de Cadastro")]
    [DisplayFormat(DataFormatString = "{dd/MM/yyyy}")]
    [Required(ErrorMessage = "Data de Cadastro Inválida!")]
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
