using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using FinLib.Common.Exceptions.Business;

namespace FinLib.Common.Validators.FileUpload
{
    internal static class Shared
    {
        internal static string GetRandomFileName()
        {
            var tempPath = Path.GetTempPath();
            var randomFileName = Path.Combine(tempPath, Path.GetRandomFileName());

            return randomFileName;
        }

        internal static async Task<string> SaveInTempPathAsync(IFormFile theFile)
        {
            var fileNameToSave = GetRandomFileName();

            using var theFileStream = new FileStream(fileNameToSave, FileMode.Create);
            await theFile.CopyToAsync(theFileStream);

            return fileNameToSave;
        }

        internal static string GetFileExtension(IFormFile file)
        {
            string itsExtension;

            try
            {
                itsExtension = Path.GetExtension(file.FileName).ToUpperInvariant();
            }
            catch (Exception)
            {
                throw new FileUploadFailedException("پسوند فایل نامعتبر است");
            }

            if (itsExtension == null)
            {
                throw new FileUploadFailedException("پسوند فایل نامعتبر است!");
            }

            return itsExtension.TrimStart('.');
        }
    }
}
