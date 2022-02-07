using System.ComponentModel.DataAnnotations;
using Demo.FileServices;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FileController : ControllerBase
{
    #region Property  
        private readonly IFileService _fileService;  
        #endregion  
 
        #region Constructor  
        public FileController(IFileService fileService)  
        {  
            _fileService = fileService;  
        }  
        #endregion  
 
        #region Upload  
        [HttpPost(nameof(Upload))]  
        public IActionResult Upload([Required] List<IFormFile> formFiles, [Required] string subDirectory)  
        {  
            try  
            {  
                _fileService.UploadFile(formFiles, subDirectory);  
  
                return Ok(new { formFiles.Count, Size = _fileService.SizeConverter(formFiles.Sum(f => f.Length)) });  
            }  
            catch (Exception ex)  
            {  
                return BadRequest(ex.Message);  
            }  
        }  
        #endregion  
 
        #region Download File  
        [HttpGet(nameof(Download))]  
        public IActionResult Download([Required]string subDirectory)  
        {  
  
            try  
            {  
                var (fileType, archiveData, archiveName) = _fileService.DownloadFiles(subDirectory);  
  
                return File(archiveData, fileType, archiveName);  
            }  
            catch (Exception ex)  
            {  
                return BadRequest(ex.Message);  
            }  
  
        }  
        #endregion  
}