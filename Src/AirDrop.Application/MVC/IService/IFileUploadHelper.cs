using Microsoft.AspNetCore.Http;

namespace AirDrop.Application.MVC.IService;

public interface IFileUploadHelper
{
    string Upload(IFormFile file, string path);
    void Delete(string relativePath);
}