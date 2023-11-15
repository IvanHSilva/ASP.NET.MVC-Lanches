using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VendasLanches.Models;

[Table("Pedidos")]
public class Order {

    // Attributes
    [Key] 
    public int Id { get; set; }
    [Column("Cliente"), Required(ErrorMessage = "Nome é obrigatório!"), MaxLength(100)]
    public string Client { get; set; } = string.Empty;
    [Column("Telefone"), Required(ErrorMessage = "Telefone é obrigatório!"), MaxLength(15)] 
    public string Phone { get; set; } = string.Empty;
    [Column("EMail"), Required(ErrorMessage = "E-Mail é obrigatório!"), MaxLength(150)] 
    public string EMail { get; set; } = string.Empty;
    [Column("Total")]
    [DataType(DataType.Currency, ErrorMessage = "Total inválido!")]
    [Required(ErrorMessage = "Total obrigatório")]
    [Range(1, 199.99, ErrorMessage = "O preço deve estar ente R$ 1,00 e R$ 199,99")]
    public double TotalOrder { get; set; }
    [Column("Items"), Required] 
    public int Items { get; set; }
    [Column("DataCad")]
    [DataType(DataType.Date, ErrorMessage = "Data Inválida!")]
    [Required(ErrorMessage = "Data obrigatória")]
    [Display(Name = "Data de Cadastro")]
    public DateTime RegDate { get; set; }
    [Column("DataEnv")]
    [DataType(DataType.Date, ErrorMessage = "Data Inválida!")]
    [Required(ErrorMessage = "Data obrigatória")]
    [Display(Name = "Data de Envio")]
    public DateTime ShippingDate { get; set; }
    [Column("DataEnt")]
    [DataType(DataType.Date, ErrorMessage = "Data Inválida!")]
    [Required(ErrorMessage = "Data obrigatória")]
    [Display(Name = "Data de Entrega")]
    public DateTime DeliveryDate { get; set; }

    public List<OrderItem>? OrderItems { get; set; }

    // Constructors
    public Order() {}

    public Order(int id, string client, string phone, string eMail, 
        double totalOrder, int items, DateTime regDate, DateTime shippingDate, 
        DateTime deliveryDate) {
        Id = id;
        Client = client;
        Phone = phone;
        EMail = eMail;
        TotalOrder = totalOrder;
        Items = items;
        RegDate = regDate;
        ShippingDate = shippingDate;
        DeliveryDate = deliveryDate;
    }
}
