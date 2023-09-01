namespace VendasLanches.Models; 
public class Category {
    
    // Attributes
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    // Constructors
    public Category(){}

    public Category(int id, string name, string description) {
        Id = id;
        Name = name;
        Description = description;
    }
}
