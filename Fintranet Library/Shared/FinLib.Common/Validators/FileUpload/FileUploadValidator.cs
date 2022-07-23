using FinLib.Common.Exceptions.Business;
using FinLib.Common.Extensions;
using FinLib.Common.Helpers;
using Microsoft.AspNetCore.Http;
using System.Drawing;
using System.Text.RegularExpressions;

namespace FinLib.Common.Validators.FileUpload
{
    public class FileUploadValidator
    {
        private static readonly HashSet<string> _generalImageExtensions = new() { "JPG", "JPEG", "PNG", "BMP" };

        private static readonly Dictionary<string, string[]> _dicExtensionToMagicNumbers = new()
        {
            { "PDF", new string [] { "25-50-44-46" } } ,
            { "JPG", new string[] { "FF-D8-FF-DB", "FF-D8-FF-E0", "FF-D8-FF-EE", "FF-D8-FF-E1" } },
            { "IMAGE/JPEG", new string[] { "FF-D8-FF-DB", "FF-D8-FF-E0", "FF-D8-FF-EE", "FF-D8-FF-E1" } },
            { "PNG", new string[] { "89-50-4E-47" } },

            { "RTF", new string [] { "7B-5C-72-74" }  } ,
            { "DOC", new string [] { "D0-CF-11-E0" }  } ,
            { "XLS", new string [] { "D0-CF-11-E0" }  } ,
            { "DOCX", new string[] { "50-4B-03-04" , "50-4B-05-06" , "50-4B-07-08"}   } ,
            { "XLSX", new string[] { "50-4B-03-04" , "50-4B-05-06" , "50-4B-07-08"}   } ,
            { "PPTX", new string[] { "50-4B-03-04" , "50-4B-05-06" , "50-4B-07-08"}   } ,

            { "TXT", new string [] { "EF-BB-BF" }  } ,

            { "ZIP", new string[] { "50-4B-03-04" , "50-4B-05-06" , "50-4B-07-08"}   } ,
            { "RAR", new string[] { "52-61-72-21" , "52-61-72-21"} }        
        };

        private static readonly Dictionary<string, string[]> _dicExtensionToMimeType = new()
        {
            { "PDF", new string[] {"APPLICATION/PDF" } },
            { "JPG", new string[] { "IMAGE/JPEG" } },
            { "JPEG", new string[] { "IMAGE/JPEG" } },
            { "PNG", new string[] { "IMAGE/PNG" } },

            { "RTF", new string[] {"APPLICATION/RTF" } },
            { "DOC", new string[] {"APPLICATION/MSWORD" } },
            { "DOCX", new string[] {"APPLICATION/VND.OPENXMLFORMATS-OFFICEDOCUMENT.WORDPROCESSINGML.DOCUMENT" } },
            { "XLS", new string[] {"APPLICATION/VND.MS-EXCEL" } },
            { "XLSX",new string[] { "APPLICATION/VND.OPENXMLFORMATS-OFFICEDOCUMENT.SPREADSHEETML.SHEET" } },
            { "PPTX",new string[] { "APPLICATION/VND.OPENXMLFORMATS-OFFICEDOCUMENT.PRESENTATIONML.PRESENTATION" } },
            
            { "ZIP", new string[] {"APPLICATION/ZIP" } },
            { "RAR", new string[] {"APPLICATION/X-RAR-COMPRESSED" , "APPLICATION/OCTET-STREAM" } },
        };

        private readonly FileUploaderValidationConfig _fileUploaderValidationConfig;

        public FileUploadValidator(FileUploaderValidationConfig fileUploaderValidationConfig)
        {
            _fileUploaderValidationConfig = fileUploaderValidationConfig;

            if (fileUploaderValidationConfig.ForbiddenWordsInContent.Count > 0)
            {
                FileContentValidator.PopulateForbiddenWords(fileUploaderValidationConfig.ForbiddenWordsInContent);
            }
        }

        /// <summary>
        /// validate input file uploading (before writing to disk)
        /// </summary>
        /// <param name="file"></param>
        /// <param name="config"></param>
        /// <param name="preparedFilePathAfterValidation">Prepared file in random (temp) path</param>
        public void ValidateFile(IFormFile file, out string preparedFilePathAfterValidation)
        {
            try
            {
                if (file == null)
                {
                    throw new FileUploadFailedException("فایل جهت آپلود، خالی می باشد");
                }

                validateNameLength(file, _fileUploaderValidationConfig.MinFileNameLength, _fileUploaderValidationConfig.MaxFileNameLength);

                validateName(file, _fileUploaderValidationConfig.RegexToValidateFileName);

                validateSize(file, _fileUploaderValidationConfig.MinFileSizeInBytes, _fileUploaderValidationConfig.MaxFileSizeInMB);

                validateExtension(file, _fileUploaderValidationConfig.AllowedFileExtensions, out string itsExtension);

                validateMimeType(file, itsExtension);

                validateMagicNumber(file, itsExtension);

                var tempFileName = Shared.SaveInTempPathAsync(file).Result;
                if (_fileUploaderValidationConfig.FakeResizeAndReplaceIfImage)
                {
                    tempFileName = resizeFileIfImage(tempFileName, itsExtension);
                }

                FileContentValidator.Validate(tempFileName, _fileUploaderValidationConfig.MaxAllowedForbiddenWordsInFileContent);

                scanByAntivirus(tempFileName, _fileUploaderValidationConfig.AntivirusCommandLineFileName, _fileUploaderValidationConfig.AntivirusCommandLineArguments);

                preparedFilePathAfterValidation = tempFileName;
            }
            catch (FileUploadFailedException vex)
            {
                if (_fileUploaderValidationConfig.ValidationFailedMessage.IsEmpty())
                {
                    throw;
                }
                else
                {
                    throw new FileUploadFailedException(_fileUploaderValidationConfig.ValidationFailedMessage, vex);
                }
            }
        }

        private static void validateExtension(IFormFile file, HashSet<string> allowedFileExtensions, out string extension)
        {
            var itsExtension = Shared.GetFileExtension(file).ToUpperInvariant();

            var foundExtensionInAllowedList = allowedFileExtensions.SingleOrDefault(x => x.ToUpperInvariant() == itsExtension);
            if (foundExtensionInAllowedList == null)
            {
                throw new FileUploadFailedException("فایل جهت بارگذاری نامعتبر می باشد. کد 1019");
            }

            extension = foundExtensionInAllowedList;
        }

        private static string resizeFileIfImage(string fileName, string fileExtension)
        {
            if (!_generalImageExtensions.Contains(fileExtension))
            {
                return fileName;
            }

            var tempFileToSave = Shared.GetRandomFileName();
            try
            {
                using (Image img = Image.FromFile(fileName))
                {
                    var resizedImage = ImageHelper.ResizeImage(fileName, img.Width, img.Height);
                    resizedImage.Save(tempFileToSave);
                }

                return tempFileToSave;
            }
            catch (Exception ex)
            {
                throw new FileUploadFailedException("فایل جهت بارگذاری نامعتبر می باشد. کد 1020", ex);                
            }
        }

        private static void validateName(IFormFile file, string regexToValidate)
        {
            if (!Regex.IsMatch(file.FileName, regexToValidate))
            {
                throw new FileUploadFailedException("فایل جهت بارگذاری نامعتبر می باشد. کد 1021");
            }
        }

        private static void scanByAntivirus(string fileName, string antivirusCommandLineFileName, string antivirusCommandLineArguments)
        {
            if (antivirusCommandLineFileName.IsEmpty())
            {
                return;
            }

            try
            {
                antivirusCommandLineArguments += $" {fileName}";

                System.Diagnostics.ProcessStartInfo procStartInfo = new System.Diagnostics.ProcessStartInfo(antivirusCommandLineFileName, antivirusCommandLineArguments)
                {
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                var proc = new System.Diagnostics.Process
                {
                    StartInfo = procStartInfo
                };

                proc.Start();
                proc.WaitForExit();

                // check if file not exists after scan (or deleted by AV)
                if (!File.Exists(fileName))
                {
                    throw new FileUploadFailedException("فایل جهت بارگذاری نامعتبر می باشد. کد 1023");
                }
            }
            catch (Exception ex)
            {
                throw new FileUploadFailedException("فایل جهت بارگذاری نامعتبر می باشد. کد 1022", ex);
            }
        }

        private static void validateMagicNumber(IFormFile file, string fileExtension)
        {
            string signatureAsHex = "";
            MemoryStream tempFileStream = new();

            file.OpenReadStream().CopyTo(tempFileStream);
            using (var reader = new BinaryReader(tempFileStream))
            {
                reader.BaseStream.Position = 0x0;               // set offset to first of the file
                byte[] data = reader.ReadBytes(0x10);           // 16 bytes 
                signatureAsHex = BitConverter.ToString(data);
            }

            // چک کن اکستنشن فایل، در وایت-لیست اکستنشن های ما اصلا هست یا نه
            if (!_dicExtensionToMagicNumbers.ContainsKey(fileExtension))
            {
                throw new FileUploadFailedException("فایل جهت بارگذاری نامعتبر می باشد. کد 1024");
            }

            // چک کن که مجیک-نامبر فایل، مطابق وایت-لیست ما، تطبیق داشته باشه
            // first 11 characters (4bytes)
            string theMagicNumber = signatureAsHex.Substring(0, 11);
            if (!_dicExtensionToMagicNumbers[fileExtension].Contains(theMagicNumber))
            {
                throw new FileUploadFailedException("فایل جهت بارگذاری نامعتبر می باشد. کد 1025");
            }
        }

        private static void validateMimeType(IFormFile file, string extension)
        {
            // validate its content-type
            if (file.ContentType.IsEmpty())
            {
                throw new FileUploadFailedException("فایل جهت بارگذاری نامعتبر می باشد. کد 1019");
            }

            // چک کن که مایم-تایپ فایل، توی وایت-لیست ما وجود داشته باشه
            if (!_dicExtensionToMimeType.ContainsKey(extension))
            {
                throw new FileUploadFailedException("فایل جهت بارگذاری نامعتبر می باشد. کد 1018");
            }

            // بررسی کن آیا مایم-تایپ با اکستنشن فایل تطبیق داره
            string itsMimeType = file.ContentType.ToUpperInvariant();
            if (!_dicExtensionToMimeType[extension].Contains(itsMimeType))
            {
                throw new FileUploadFailedException("فایل جهت بارگذاری نامعتبر می باشد. کد 1017");
            }
        }

        private static void validateSize(IFormFile file, int minFileSizeInBytes, int maxFileSizeInMB)
        {
            var maxFileSizeInBytes = maxFileSizeInMB * 1024 * 1024;
            if (file.Length < minFileSizeInBytes || file.Length > maxFileSizeInBytes)
            {
                throw new FileUploadFailedException("فایل جهت بارگذاری نامعتبر می باشد. کد 1016");
            }
        }

        private static void validateNameLength(IFormFile file, int minFileName, int maxFileName)
        {
            if (file.FileName.Length < minFileName || file.FileName.Length > maxFileName)
            {
                throw new FileUploadFailedException("فایل جهت بارگذاری نامعتبر می باشد. کد 1015");
            }
        }
    }
}
