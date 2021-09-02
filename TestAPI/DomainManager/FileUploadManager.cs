using Lufuno.Utilities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lufuno.Domain;
using Lufuno.DomainManager.Interfaces;
using Lufuno.DataAccess;
using Lufuno.DataAccessLogic.Interfaces;

namespace Lufuno.DomainManager
{
    public class FileUploadManager : IFileUploadManager
    {
        /// <summary>
        /// The logging implementation.
        /// </summary>
        private readonly ILogger _logger = null;

        private readonly IUtil _util = null;

        private readonly IFileUploadLogic _fileUploadLogic = null;
        private readonly IRecordLogic _recordLogic = null;

        public FileUploadManager(ILogger logger, IUtil util, IFileUploadLogic fileUploadLogic, IRecordLogic recordlogic)
        {
            _logger = logger;
            _util = util;
            _fileUploadLogic = fileUploadLogic;
            _recordLogic = recordlogic;
        }

        public void ProcessFile(FileToUpload theFile)
        {
            if (theFile == null || theFile.FileAsBase64 == null)
                throw new ArgumentNullException();

            string fileText;
            try
            {
                fileText = _util.getASCIIFromBase64(theFile.FileAsBase64);
                _logger.LogInfo(theFile.FileName, "Uploaded file converted to plain text successfully");

                Lufuno.DataAccess.FileUpload fileUpload = mapControllerFileDO(theFile);

                fileUpload = _util.GetFileDetailsFromAscii(theFile.FileAsBase64, fileUpload);

                int fileSaveId = _fileUploadLogic.SaveFile(fileUpload);
                if (fileSaveId > 0)
                {
                    _logger.LogInfo(fileUpload.FileName, "File saved successfully");
                    foreach (var record in fileUpload.Records)
                    {
                        record.FileId = fileSaveId;
                    }
                    _recordLogic.SaveRecord(fileUpload.Records.ToList());
                }
            }
            catch (Exception ex)
            {
                _logger.LogCriticalError(ex, "Unable to convert file to plain text successfully");
                throw new Exception("There was an issue converting the file to plain text : " + ex);
            }
        }

        public List<FileToUpload> GetUploadedFiles()
        {
            List<FileToUpload> files = new List<FileToUpload>();

            var DataAccessFiles = _fileUploadLogic.GetFileUpoads();
            foreach (var file in DataAccessFiles)
                files.Add(mapDOFileToController(file));
            
            return files;
        }
        
        private DataAccess.FileUpload mapControllerFileDO(FileToUpload File)
        {
            return new DataAccess.FileUpload
            {
                Id = 0,
                Uid = Guid.NewGuid(),
                FileName = File.FileName,
                LoadDate = DateTime.UtcNow,
                UserId = string.Empty,
                base64 = File.FileAsBase64,
                NumberOfRecords = 0
            };
        }
        public  FileToUpload mapDOFileToController(DataAccess.FileUpload fileUpload)
        {
            return new FileToUpload
            {
                Id = fileUpload.Id,
                FileName = fileUpload.FileName,
                CreateDate = fileUpload.LoadDate.ToString("dd MMM yyyy HH:mm:ss"),
                FileAsBase64 = fileUpload.base64
            };
        }
    }
}
