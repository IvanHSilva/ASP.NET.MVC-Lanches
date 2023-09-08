using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VendasLanches.Models;

[Table("CarrinhoItem")]
public class CartItem {

    // Attributes
    [Key]
    public int Id { get; set; }
    [Column("LancheId"), Required]
    public int SnackId { get; set; }
    [Column("Lanche"), Required, MaxLength(50)]
    public string SnackName { get; set; } = string.Empty;
    [Column("Quantidade"), Required]
    public int Quantity { get; set; }
    [Column("CarrinhoId"), Required, MaxLength(250)]
    public string CartId { get; set; } = string.Empty;
    [Column("DataCad")]
    [Display(Name = "Data de Cadastro")]
    [Required]
    public DateTime RegDate { get; set; }
    public Snack Snack { get; set; } = null!;

    // Constructors
    public CartItem(){}

    public CartItem(int id, int snackId, string snackName, int quantity, 
        string cartId, DateTime regDate) {
        Id = id;
        SnackId = snackId;
        SnackName = snackName;
        Quantity = quantity;
        CartId = cartId;
        RegDate = regDate;
    }
}
