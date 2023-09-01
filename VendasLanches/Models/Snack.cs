namespace VendasLanches.Models; 

public class Snack {

    // Attributes
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Details { get; set; } = string.Empty;
    public double Price { get; set; }
    public string Image { get; set; } = string.Empty;
    public string Miniature { get; set; } = string.Empty;
    public bool Favorite { get; set; }
    public bool HaveStock { get; set; }
    public string Category { get; set; } = string.Empty;

    // Constructors
    public Snack(){}

    public Snack(int id, string name, string description, string details, double price, string image, string miniature, bool favorite, bool haveStock) {
        Id = id;
        Name = name;
        Description = description;
        Details = details;
        Price = price;
        Image = image;
        Miniature = miniature;
        Favorite = favorite;
        HaveStock = haveStock;
    }
}
