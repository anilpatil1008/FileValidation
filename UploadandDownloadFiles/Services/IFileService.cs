using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UploadandDownloadFiles.Services
{
   public interface IFileService
    {
       List<string> FileContentsValidation(IFormFile file);
    }
}
