using Lufuno.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lufuno.DataAccessLogic.Interfaces
{
    public interface IFileUploadLogic
    {
        List<DataAccess.FileUpload> GetFileUpoads();

        int SaveFile(FileUpload file);
    }
}
