using Lufuno.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lufuno.DataAccessLogic.Interfaces
{
    public interface IRecordLogic
    {
        int SaveRecord(List<Record> record);

        List<Record> GetUnApprovedRecords();

        List<DataAccess.Record> GetFileRecords(int fileId);
        void UpdateRecord(DataAccess.Record record);
    }
}
