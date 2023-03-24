using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FilesClassLibraryDemo;
public class Class1 : ControllerBase
{
    // File download demo
    public async Task<FileResult> DownloadFile(string fileName)
    {
        IFileService fileService = new FileService();

        var bytes = await fileService.DownloadFile(fileName);
        return File(bytes, "application/octet-stream", fileName);
    }

    // File delete demo
    public async Task<IActionResult> DeleteFile(string fileName)
    {
        IFileService fileService = new FileService();

        await fileService.DeleteFile(fileName);
        return Ok();
    }

    // File upload demo
    [HttpPost]
    public async Task<ActionResult> Create(IFormFile file)
    {
        IFileService fileService = new FileService();

        try
        {
            if (await fileService.UploadFile(file))
                return Ok("File successfully uploaded!");

            return BadRequest();
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
    }
}
