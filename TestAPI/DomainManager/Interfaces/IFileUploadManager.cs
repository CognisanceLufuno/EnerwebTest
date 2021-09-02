using Lufuno.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lufuno.DomainManager.Interfaces
{
    public interface IFileUploadManager
    {
        void ProcessFile(FileToUpload theFile);

        List<FileToUpload> GetUploadedFiles();

        FileToUpload mapDOFileToController(DataAccess.FileUpload fileUpload);
    }
}
