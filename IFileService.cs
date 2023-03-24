using Microsoft.AspNetCore.Http;

namespace FilesClassLibraryDemo;

public interface IFileService
{
    Task<List<FileDetailDto>> GetFiles();
    Task<bool> UploadFile(IFormFile file);
    Task<byte[]> DownloadFile(string fileName);
    Task DeleteFile(string fileName);
}