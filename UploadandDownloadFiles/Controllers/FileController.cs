using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UploadandDownloadFiles.Services;

namespace UploadandDownloadFiles.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        [HttpPost(nameof(UploadAsync))]
        public async Task<IActionResult> UploadAsync([Required] IFormFile file)
        {
            try
            {
                var extension = Path.GetExtension(file.FileName);
                if (extension == ".txt")
                {

                    List<string> result = _fileService.FileContentsValidation(file);


                    if (result.Count != 0)
                        return Ok(new { fileValid = false, invalidLines = result });
                    else
                        return Ok(new { fileValid = true });
                }
                
                return Ok(new { error = "Please select .txt file." });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

     
    }

}
