using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VendasLanches.Models;

[Table("PedidosItem")]
public class OrderItem {

    // Attributes
    [Key]
    public int Id { get; set; }
    [Column("LancheId"), Required]
    public int SnackId { get; set; }
    [Column("Lanche"), Required, MaxLength(50)]
    public string SnackName { get; set; } = string.Empty;
    [Column("Quantidade"), Required]
    public int Quantity { get; set; }
    [Column("Preco"), Required]
    public double Price { get; set; }
    [Column("Total"), Required]
    public double Total { get; set; }
    [Column("CarrinhoId"), MaxLength(250)]
    public string CartId { get; set; } = string.Empty;
    [Column("PedidoId"), Required]
    public int OrderId { get; set; }
    [Column("DataCad"), Required]
    [Display(Name = "Data de Cadastro")]
    public DateTime RegDate { get; set; }
    public Snack Snack { get; set; } = null!;

    // Constructors
    public OrderItem() { }

    public OrderItem(int id, int snackId, string snackName, int quantity, 
        string cartId, int orderId, DateTime regDate) {
        Id = id;
        SnackId = snackId;
        SnackName = snackName;
        Quantity = quantity;
        CartId = cartId;
        OrderId = orderId;
        RegDate = regDate;
    }
}
