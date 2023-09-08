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
    public string Snack { get; set; } = string.Empty;
    [Column("Quantidade"), Required]
    public int Quantity { get; set; }
    [Column("CarrinhoId"), Required, MaxLength(250)]
    public string CartId { get; set; } = string.Empty;
    [Column("DataCad")]
    [Display(Name = "Data de Cadastro")]
    [Required]
    public DateTime RegDate { get; set; }

    // Constructors
    public CartItem(){}

    public CartItem(int id, int snackId, string snack, int quantity, 
        string cartId, DateTime regDate) {
        Id = id;
        SnackId = snackId;
        Snack = snack;
        Quantity = quantity;
        CartId = cartId;
        RegDate = regDate;
    }
}
