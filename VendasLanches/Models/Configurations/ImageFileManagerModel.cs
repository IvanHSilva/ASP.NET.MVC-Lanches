namespace VendasLanches.Models.Configurations; 

public class ImageFileManagerModel {
    public FileInfo[] Images { get; set; } = null!;
    public IFormFile IFormFileImage { get; set; } = null!;
    public List<IFormFile> IFormFileImages { get; set; } = null!;
    public string PathImages { get; set; } = string.Empty;
}
