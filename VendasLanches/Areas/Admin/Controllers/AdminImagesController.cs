using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using VendasLanches.Models.Configurations;

namespace VendasLanches.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class AdminImagesController : Controller {

    private readonly ImagesConfiguration _configuration;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public AdminImagesController(IWebHostEnvironment webHostEnvironment,
        IOptions<ImagesConfiguration> configuration) {
        _configuration = configuration.Value;
        _webHostEnvironment = webHostEnvironment;
    }

    public IActionResult Index() {
        return View();
    }

    public async Task<IActionResult> UploadImages(List<IFormFile> files) {

        if (files == null || files.Count == 0) {
            ViewData["Error"] = "Erro! Arquivos não selecionados!";
            return View(ViewData);
        }

        if (files.Count > 10) {
            ViewData["Error"] = "Erro! Quantidade de arquivos não pode ultrapassar 10!";
            return View(ViewData);
        }

        long size = files.Sum(f => f.Length);

        List<string> filePathsName = new List<string>();

        string filePath = Path.Combine(_webHostEnvironment.WebRootPath,
            _configuration.SnacksImagesFolder);

        foreach (IFormFile file in files) {
            if (file.FileName.Contains(".jpg") || file.FileName.Contains(".png")) {

                string completeFileName = string.Concat(filePath, "\\", file.FileName);
                filePathsName.Add(completeFileName);

                using (FileStream stream = new FileStream(completeFileName, FileMode.Create)) {
                    await file.CopyToAsync(stream);
                }
            }
        }

        ViewData["Result"] = $"{files.Count} arquivos foram enviados ao servidor " +
                             $"(tamamnho total: {size} bytes";

        ViewBag.Files = filePathsName;

        return View(ViewData);
    }

    public IActionResult GetImages() {

        ImageFileManagerModel model = new ImageFileManagerModel();

        string imagesPath = Path.Combine(_webHostEnvironment.WebRootPath,
            _configuration.SnacksImagesFolder);

        DirectoryInfo dir = new DirectoryInfo(imagesPath);
        FileInfo[] files = dir.GetFiles();

        model.PathImages = _configuration.SnacksImagesFolder;

        if (files.Length == 0) {
            ViewData["Error"] = $"Nenhum arquivo encontrado na pasta {imagesPath}!";
        }

        model.Images = files;

        return View(model);
    }

    public IActionResult DeleteFile(string fileName) {

        string _imgDelete = Path.Combine(_webHostEnvironment.WebRootPath,
           _configuration.SnacksImagesFolder);

        if (System.IO.File.Exists(_imgDelete)) {
            System.IO.File.Delete(_imgDelete);
            ViewData["Excluido"] = $"Arquivos {_imgDelete} excluídos com sucesso!";
        }

        return View("Index");
    }
}
