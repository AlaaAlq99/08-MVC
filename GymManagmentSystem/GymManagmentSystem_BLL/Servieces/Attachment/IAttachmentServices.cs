using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentSystem_BLL.Servieces.Attachment
{
    public interface IAttachmentServices
    {

        Task<string> uploadAsync(Stream fileStream, string fileName, string FolerName, string webRootPath);

        bool deleteAsync(string fileName, string FolerName, string webRootPath);

        (Stream stream, string ContentType) getAsync(string fileName, string FolerName, string webRootPath);
    
    
    
    }
}
