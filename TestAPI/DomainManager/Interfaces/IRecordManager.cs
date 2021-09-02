using Lufuno.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lufuno.DomainManager.Interfaces
{
    public interface IRecordManager
    {
        List<FileRecord> GetFileRecords(int fileId);
        void UpdateRecord(FileRecord record);
    }
}