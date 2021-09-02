using Lufuno.DataAccess;
using Lufuno.DataAccessLogic.Interfaces;
using Lufuno.Domain;
using Lufuno.DomainManager.Interfaces;
using Lufuno.Utilities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lufuno.DomainManager
{
    public class RecordManager : IRecordManager
    {
        private readonly IRecordLogic _recordLogic = null;
        private readonly ILogger _logger = null;
        private readonly IFileUploadManager _fileUploadManager = null;

        public RecordManager(ILogger logger, IRecordLogic recordlogic, IFileUploadManager fileUploadManager)
        {
            _logger = logger;
            _recordLogic = recordlogic;
            _fileUploadManager = fileUploadManager;
        }
        public List<FileRecord> GetFileRecords(int fileId)
        {
            List<FileRecord> fileRecords = new List<FileRecord>();

            var DataAccessFileRecords = _recordLogic.GetFileRecords(fileId);
            foreach (var record in DataAccessFileRecords)
                fileRecords.Add(mapDORecordToController(record));

            return fileRecords;
        }

        public void UpdateRecord(FileRecord record)
        {
            _recordLogic.UpdateRecord(mapDomainRecordToDORecord(record));
        }

        private FileRecord mapDORecordToController(Record record)
        {
            return new FileRecord
            {
                Id = record.Id,
                Uid = record.Uid.ToString(),
                FileId = record.FileId,
                OperatingDate = record.OperatingDate.ToString("dd MMM yyyy HH:mm:ss"),
                ServicePoint = record.ServicePoint,
                HourNumber = record.HourNumber,
                UserId = record.UserId,
                ImportEnergy = record.ImportEnergy,
                ExportEnergy = record.ExportEnergy,
                ImportLeadingReactive = record.ImportLeadingReactive,
                ExportLeadingReactive = record.ExportLeadingReactive,
                ImportLaggingReactive = record.ImportLaggingReactive,
                ExportLaggingReactive = record.ExportLaggingReactive,
                IsOfficial = record.IsOfficial,
                IsApproved = record.IsApproved,
                //Upload = _fileUploadManager.mapDOFileToController(record.Upload)
            };
        }

        private DataAccess.Record mapDomainRecordToDORecord(FileRecord record)
        {
            return new DataAccess.Record
            {
                Id = record.Id,
                Uid = Guid.Parse(record.Uid),
                FileId = record.FileId,
                OperatingDate = DateTime.Parse(record.OperatingDate),
                ServicePoint = record.ServicePoint,
                HourNumber = record.HourNumber,
                UserId = record.UserId,
                ImportEnergy = record.ImportEnergy,
                ExportEnergy = record.ExportEnergy,
                ImportLeadingReactive = record.ImportLeadingReactive,
                ExportLeadingReactive = record.ExportLeadingReactive,
                ImportLaggingReactive = record.ImportLaggingReactive,
                ExportLaggingReactive = record.ExportLaggingReactive,
                IsOfficial = record.IsOfficial,
                IsApproved = record.IsApproved,
                //Upload = _fileUploadManager.mapDOFileToController(record.Upload)
            };
        }

    }
}
