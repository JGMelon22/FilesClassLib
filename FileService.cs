using Microsoft.AspNetCore.Http;

namespace FilesClassLibraryDemo;

public class FileService : IFileService
{
    public async Task<List<FileDetailDto>> GetFiles()
    {
        var filePath = Directory.GetFiles(Path.Combine(Environment.CurrentDirectory, "ReceivedFiles"));

        var sentFiles = new List<FileDetailDto>();

        foreach (var item in filePath)
            sentFiles.Add(new FileDetailDto
            {
                FileName = Path.GetFileName(item)
            });

        return await Task.FromResult(sentFiles.ToList());
    }

    public async Task<bool> UploadFile(IFormFile file)
    {
        var path = "";
        var allowedFileExtensions = ".csv"; // You can change to any file extension here

        try
        {
            if (file.Length > 0) // At least one file is selected
            {
                path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "ReceivedFile"));
                var ext = Path.GetExtension(file.FileName).ToLowerInvariant(); // Get file extension

                if (!Directory.Exists(path)) Directory.CreateDirectory(path); // Auto creates directory if not exist

                if (string.IsNullOrEmpty(ext) || allowedFileExtensions.Contains(ext)) // Verify if has correct extension
                {
                    var fileStream = new FileStream(Path.Combine(path, file.FileName), FileMode.Create);

                    await file.CopyToAsync(fileStream);

                    return true;
                }
            }

            return false;
        }
        catch (Exception ex)
        {
            throw new Exception("File Copy Failed", ex);
        }
    }

    public async Task<byte[]> DownloadFile(string fileName)
    {
        // Get File from it's Directory
        var path = Path.Combine(
            Path.Combine(Environment.CurrentDirectory, $"ReceivedFile{Path.DirectorySeparatorChar}" + fileName));

        var bytes = await File.ReadAllBytesAsync(path); // Read file

        return await Task.FromResult(bytes); // Download file
    }

    public Task DeleteFile(string fileName)
    {
        // Get File from it's Directory
        var path = Path.Combine(
            Path.Combine(Environment.CurrentDirectory, $"ReceivedFile{Path.DirectorySeparatorChar}" + fileName));

        // If exists, delete
        if (File.Exists(path))
            File.Delete(path);

        return Task.CompletedTask;
    }
}