using Microsoft.AspNetCore.Http;

namespace Application.IService;

public interface IFileUploadHelper
{
    string Upload(IFormFile file, string path);
    void Delete(string relativePath);
}