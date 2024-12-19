using AirDrop.Application.MVC.IService;

namespace AirDrop.EndPoint.Models;

public class FileUploadHelper : IFileUploadHelper
{
    private readonly IWebHostEnvironment _webHostEnvironment;

    public FileUploadHelper(IWebHostEnvironment webHostEnvironment)
    {
        _webHostEnvironment = webHostEnvironment;
    }

    public string Upload(IFormFile file, string path)
    {
        if (file == null || file.Length == 0)
            return string.Empty;

        var directoryPath = Path.Combine(_webHostEnvironment.WebRootPath, "Uploads", path);
        if (!Directory.Exists(directoryPath))
            Directory.CreateDirectory(directoryPath);

        var uniqueFileName = $"{Guid.NewGuid()}_{file.FileName}";
        var filePath = Path.Combine(directoryPath, uniqueFileName);

        try
        {
            using var output = File.Create(filePath);
            file.CopyTo(output);
        }
        catch (Exception ex)
        {
            throw new IOException("An error occurred while uploading the file.", ex);
        }

        return Path.Combine(path, uniqueFileName).Replace("\\", "/");
    }

    public void Delete(string relativePath)
    {
        if (string.IsNullOrEmpty(relativePath))
            return;

        var fullPath = Path.Combine(_webHostEnvironment.WebRootPath, "Uploads", relativePath);

        try
        {
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
        }
        catch (Exception ex)
        {
            throw new IOException("An error occurred while deleting the file.", ex);
        }
    }
}
