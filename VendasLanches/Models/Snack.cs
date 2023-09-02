using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VendasLanches.Models;

[Table("Lanches")]
public class Snack {

    // Attributes
    [Key]
    public int Id { get; set; }
    [Column("Nome")][Required, MaxLength(50)] 
    public string Name { get; set; } = string.Empty;
    [Column("Descricao")]
    [Display(Name="Descrição")]
    [Required, MaxLength(100)]
    public string Description { get; set; } = string.Empty;
    [Column("Detalhes")][Required, MaxLength(250)]
    public string Details { get; set; } = string.Empty;
    [Column("Preco")][Required]
    [Display(Name = "Preço")]
    [Range(1, 199.99, ErrorMessage = "O preço deve estar ente R$ 1,00 e R$ 199,99")]
    public double Price { get; set; }
    [Column("Imagem")][MaxLength(150)] 
    public string Image { get; set; } = string.Empty;
    [Column("Miniatura")][MaxLength(150)] 
    public string Miniature { get; set; } = string.Empty;
    [Column("Preferido")][Required] 
    public bool Favorite { get; set; }
    [Column("Estoque")]
    [Display(Name = "Tem Estoque")]
    [Required]
    public bool HaveStock { get; set; }
    [Column("Categoria")][Required, MaxLength(50)] 
    public string Category { get; set; } = string.Empty;
    [Column("DataCad")]
    [Display(Name = "Data de Cadastro")]
    [Required]
    public DateTime RegDate { get; set; }

    // Constructors
    public Snack(){}

    public Snack(int id, string name, string description, string details, 
        double price, string image, string miniature, bool favorite, 
        bool haveStock, DateTime regDate) {
        Id = id;
        Name = name;
        Description = description;
        Details = details;
        Price = price;
        Image = image;
        Miniature = miniature;
        Favorite = favorite;
        HaveStock = haveStock;
        RegDate = regDate;
    }
}
