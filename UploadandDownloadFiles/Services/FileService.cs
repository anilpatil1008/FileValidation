using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace UploadandDownloadFiles.Services
{
    public class FileService :IFileService
    {
        #region Property
        private IHostingEnvironment _hostingEnvironment;
        #endregion

        #region Constructor
        [Obsolete]
        public FileService(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        #endregion

      

      
    

        #region file content validation
        public List<string> FileContentsValidation(IFormFile file)
        {
            List<string> result = new List<string>();

            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                int lineNumber = 0;
                bool success = false;
                while (reader.Peek() >= 0)
                {
                    lineNumber++;
                    string[] customerData = reader.ReadLine().Split(' ');
                    if (customerData.Length > 1)
                    {
                        bool validCustomerName = false;
                        bool validCustomerAccountNumber = false;
                        string errorString = "";
                        if (Regex.Match(customerData[0], "^[A-Z][a-z]*$").Success)
                        {
                            validCustomerName = true;
                        }

                        if (Regex.Match(customerData[1], "^[3-4][0-9]{6}$").Success)
                        {
                            validCustomerAccountNumber = true;
                        }

                        else if (Regex.Match(customerData[1], "^[3-4][0-9]{6}[p]$").Success)
                        {
                            validCustomerAccountNumber = true;
                        }

                        if (!(validCustomerName && validCustomerAccountNumber))
                        {
                            if (!validCustomerName && !validCustomerAccountNumber)
                                errorString = "Account name, account number -not valid for " + lineNumber + " line";
                            else if (!validCustomerName)
                                errorString = "Account name -not valid for " + lineNumber + " line";
                            else if (!validCustomerAccountNumber)
                                errorString = "Account number -not valid for " + lineNumber + " line";

                            result.Add(errorString + ' ' + customerData[0] + ' ' + customerData[1]);
                        }

                    }

                }
            }


            return result;
        }
        #endregion

    }
}
