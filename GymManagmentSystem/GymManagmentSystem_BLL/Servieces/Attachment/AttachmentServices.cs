using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace GymManagmentSystem_BLL.Servieces.Attachment
{
    public class AttachmentServices : IAttachmentServices
    {
        private readonly long _maxFileSize = 5 * 1024 * 1024;
        private readonly ILogger<AttachmentServices> _logger;
        private readonly string[] _allowedExtenstions = [".png", ".jpeg", ".jpg"];

        public AttachmentServices(ILogger<AttachmentServices> logger)
        { 
            _logger = logger;
        }

        public async Task<string?> uploadAsync(Stream fileStream, string fileName, string FolerName, string webRootPath)
        {
            //validate max file size  max filesize = 5 MB
            if (fileStream == null || !fileStream.CanRead) return null;
            if(fileStream.Length ==0) return null;
            if (fileStream.Length >  _maxFileSize)
            {
                _logger.LogError($"File Rejected: file Size {fileStream.Length} bytes too large");
                return null;
            }
            var extention = Path.GetExtension(fileName);
            if(string.IsNullOrEmpty(extention) || !_allowedExtenstions.Contains(extention))
            {
                _logger.LogError($"File Rejected: file extention {extention} Not Allowed");
                return null;
            }
            //get the path to upload
            var uploadedFolder = Path.Combine(webRootPath,FolerName);
            // if file not found crate it
            Directory.CreateDirectory(uploadedFolder);
            //file name will generate by us
            var stortedFileName = $"{Guid.NewGuid()}{fileName}";
            //full file path
            var filePath = Path.Combine(uploadedFolder, stortedFileName);
            //Upload Start
            try
            {
                using var fs = new FileStream(filePath, FileMode.Create, FileAccess.Write);
                await fileStream.CopyToAsync(fs);

                return stortedFileName;
            }catch(Exception ex)
            {
                _logger.LogError(ex, $"faild to upload file {fileName}");
                return null;
            }


        }


        public bool deleteAsync(string fileName, string FolerName, string webRootPath)
        {
            //get the path to upload
            try
            {
            var uploadedFolder = Path.Combine(webRootPath, FolerName, fileName);
                if(File.Exists(uploadedFolder))
                {
                    File.Delete(uploadedFolder);
                    return true;
                }
                else
                {
                    _logger.LogError($"File Not Found: file name {fileName}");
                    return false;
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to delete file {fileName}");
                return false;
            }
        }

        public (Stream stream, string ContentType) getAsync(string fileName, string FolerName, string webRootPath)
        {
            if(string.IsNullOrEmpty(fileName)|| string.IsNullOrEmpty(FolerName)|| string.IsNullOrEmpty(webRootPath)) return (null, null);

            var uploadedFolder = Path.Combine(webRootPath, FolerName, fileName);

            if(!File.Exists(uploadedFolder))
            {
                _logger.LogError($"File Not Found: file name {fileName}");
                return (null, null);
            }

            //open the file and return it as stream
            var stream = new FileStream(uploadedFolder, FileMode.Open, FileAccess.Read);
            var extention = Path.GetExtension(fileName);
            var contentType = extention switch
            {
                ".png" => "image/png",
                ".jpeg" => "image/jpeg",
                ".jpg" => "image/jpeg",
                _ => "application/octet-stream"//Binary file
            };
            return (stream, contentType);
        }

    }
}
